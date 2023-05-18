using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Extensions;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.SYS
{
    public class SYS_OSAL_NVREAD : ZToolPacket
    {

        public SYS_OSAL_NVREAD(ushort id, ushort offset = 0)
        {
            byte[] framedata = new byte[4];
            //Inversed because at first glance order wasn't valid
            //framedata[0] = this.PanID.GetLSB();
            //framedata[1] = this.PanID.GetMSB();
            //Do not inverse - as if it was initialized with different PANID reinitialization will create different network
            framedata[0] = id.GetLSB(); 
            framedata[1] = id.GetMSB();
            framedata[2] = offset.GetLSB();
            framedata[3] = offset.GetMSB(); 

            BuildPacket((ushort)ZToolCMD.SYS_OSAL_NVREAD, framedata);
        }
    }
}
