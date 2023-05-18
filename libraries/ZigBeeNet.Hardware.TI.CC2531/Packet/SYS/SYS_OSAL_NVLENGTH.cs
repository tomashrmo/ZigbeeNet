using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Extensions;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.SYS
{
    public class SYS_OSAL_NVLENGTH : ZToolPacket
    {

        public SYS_OSAL_NVLENGTH(ushort id)
        {
            byte[] framedata = new byte[2];
            //Inversed because at first glance order wasn't valid
            //framedata[0] = this.PanID.GetLSB();
            //framedata[1] = this.PanID.GetMSB();
            //Do not inverse - as if it was initialized with different PANID reinitialization will create different network
            framedata[0] = id.GetLSB(); 
            framedata[1] = id.GetMSB();

            BuildPacket((ushort)ZToolCMD.SYS_OSAL_NVLENGTH, framedata);
        }
    }
}
