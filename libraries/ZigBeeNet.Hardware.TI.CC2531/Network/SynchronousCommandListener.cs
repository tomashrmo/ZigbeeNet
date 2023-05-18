using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Hardware.TI.CC2531.Packet;
using ZigBeeNet.Util;
//using Microsoft.Extensions.Logging;

namespace ZigBeeNet.Hardware.TI.CC2531.Network
{
    public class SynchronousCommandListener : ISynchronousCommandListener
    {
        //static private readonly ILogger _logger = LogManager.GetLog<SynchronousCommandListener>();
        public event EventHandler<ZToolPacket> OnResponseReceived;

        public void ReceivedCommandResponse(ZToolPacket packet)
        {
            //Console.WriteLine(" {0} received as synchronous command.", packet.GetType().Name);
            OnResponseReceived?.Invoke(this, packet);
        }
    }
}
