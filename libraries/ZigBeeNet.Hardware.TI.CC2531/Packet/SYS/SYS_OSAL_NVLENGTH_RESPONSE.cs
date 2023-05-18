using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.SYS
{
    /// <summary>
    /// This command is used to get a configuration property from non-volatile memory
    /// </summary>
    public class SYS_OSAL_NVLENGTH_RESPONSE : ZToolPacket
    {
        /// <summary>
        /// Specifies the size of the Value buffer in bytes
        /// </summary>
        public ushort Len { get; private set; }

        /// <summary>
        /// Buffer to hold the configuration property
        /// </summary>
        public byte[] Value { get; private set; }

        public SYS_OSAL_NVLENGTH_RESPONSE(byte[] framedata)
        {
            Len = (ushort)(framedata[0] + (framedata[1] << 8));
            
            BuildPacket((ushort)ZToolCMD.SYS_OSAL_NVLENGTH_RESPONSE, framedata);
        }
    }
}
