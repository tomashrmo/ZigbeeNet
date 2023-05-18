using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.SYS
{
    /// <summary>
    /// This command is used to get a configuration property from non-volatile memory
    /// </summary>
    public class SYS_OSAL_NVREAD_RESPONSE : ZToolPacket
    {
        /// <summary>
        /// This field indicates either SUCCESS (0) or FAILURE (1)
        /// </summary>
        public PacketStatus Status { get; private set; }

        /// <summary>
        /// Specifies the size of the Value buffer in bytes
        /// </summary>
        public byte Len { get; private set; }

        /// <summary>
        /// Buffer to hold the configuration property
        /// </summary>
        public byte[] Value { get; private set; }

        public SYS_OSAL_NVREAD_RESPONSE(byte[] framedata)
        {
            Status = (PacketStatus)framedata[0];
            Len = framedata[1];
            Value = new byte[framedata.Length - 2];

            for (int i = 0; i < Value.Length; i++)
            {
                this.Value[i] = framedata[i + 2];
            }

            BuildPacket((ushort)ZToolCMD.SYS_OSAL_NVREAD_RESPONSE, framedata);
        }
    }
}
