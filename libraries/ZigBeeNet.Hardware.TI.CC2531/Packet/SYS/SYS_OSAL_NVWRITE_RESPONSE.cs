using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.SYS
{
    /// <summary>
    /// This command is used to set a configuration property to non-volatile memory
    /// </summary>
    public class SYS_OSAL_NVWRITE_RESPONSE : ZToolPacket
    {
        /// <summary>
        /// This field indicates either SUCCESS (0) or FAILURE (1)
        /// </summary>
        public PacketStatus Status { get; private set; }

        public SYS_OSAL_NVWRITE_RESPONSE(byte[] framedata)
        {
            Status = (PacketStatus)framedata[0];

            BuildPacket((ushort)ZToolCMD.SYS_OSAL_NVWRITE_RESPONSE, framedata);
        }
    }
}
