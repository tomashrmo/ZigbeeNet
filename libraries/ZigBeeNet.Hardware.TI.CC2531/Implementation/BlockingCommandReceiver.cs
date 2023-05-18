using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using ZigBeeNet.Hardware.TI.CC2531.Network;
using ZigBeeNet.Hardware.TI.CC2531.Packet;
using ZigBeeNet.Util;
//using Microsoft.Extensions.Logging;

namespace ZigBeeNet.Hardware.TI.CC2531.Implementation
{
    /// <summary>
    /// Blocking receiver for asynchronous commands.
    /// </summary>
    public class BlockingCommandReceiver : IAsynchronousCommandListener
    {
        //static private readonly ILogger _logger = LogManager.GetLog<BlockingCommandReceiver>();

        private object _getCommandLockObject;

        private static ManualResetEvent _commandSync = new ManualResetEvent(true);

        /// <summary>
        /// The command interface.
        /// </summary>
        private ICommandInterface _commandInterface;
        /// <summary>
        /// The command ID to wait for.
        /// </summary>
        private ZToolCMD _commandId;
        /// <summary>
        /// The command packet.
        /// </summary>
        private ZToolPacket _commandPacket = null;

        /// <summary>
        /// The constructor for setting expected command ID and command interface.
        /// Sets self as listener for command in command interface.
        ///
        /// <param name="commandId">the command ID</param>  
        /// <param name="commandInterface">the command interface</param>  
        /// </summary>
        public BlockingCommandReceiver(ZToolCMD commandId, ICommandInterface commandInterface)
        {
            _getCommandLockObject = new object();

            _commandId = commandId;
            _commandInterface = commandInterface;
            Console.WriteLine("Waiting for asynchronous response message {0}.", commandId);
            _commandInterface.AddAsynchronousCommandListener(this);
        }

        /// <summary>
        /// Gets command packet and blocks until the command packet is available or timeoutMillis occurs.
        /// 
        /// <param name="timeoutMillis">the timeout in milliseconds</param>
        /// <returns>the command packet or null if time out occurs.</returns>
        /// </summary>
        public ZToolPacket GetCommand(long timeoutMillis)
        {
            lock (_getCommandLockObject)
            {
                long wakeUpTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + timeoutMillis;
                while (_commandPacket == null && wakeUpTime > DateTimeOffset.UtcNow.ToUnixTimeMilliseconds())
                {
                    try
                    {
                        _commandSync.WaitOne(TimeSpan.FromMilliseconds(wakeUpTime - DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Blocking command receive timed out. {0}", e);
                    }
                }
            }
            if (_commandPacket == null)
            {
                Console.WriteLine("Timeout {0} expired and no packet with {1} received", timeoutMillis, _commandId);
            }
            Cleanup();
            return _commandPacket;
        }

        /// <summary>
        /// Clean up asynchronous command listener from command interface.
        /// </summary>
        public void Cleanup()
        {
            lock (_getCommandLockObject)
            {
                _commandInterface.RemoveAsynchronousCommandListener(this);
                _commandSync.Reset();
            }
        }

        public void ReceivedAsynchronousCommand(ZToolPacket packet)
        {
            //Console.WriteLine("Received a packet {0} and waiting for {1}", packet.CMD, _commandId);
            //Console.WriteLine("received {0} {1}", packet.GetType(), packet.ToString());
            //if (packet.isError())
            //{
            //    return;
            //}
            if ((ZToolCMD)packet.CMD != _commandId)
            {
                Console.WriteLine("Received unexpected packet: {0}" + packet.GetType().Name);
                return;
            }
            lock (typeof(BlockingCommandReceiver))
            {
                _commandPacket = packet;
                Console.WriteLine("Received expected response: {0}", packet.GetType().Name);
                Cleanup();
            }
        }

        public void ReceivedUnclaimedSynchronousCommandResponse(ZToolPacket packet)
        {
            // Response handler not required
        }
    }
}
