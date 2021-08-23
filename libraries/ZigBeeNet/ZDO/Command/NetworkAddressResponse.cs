using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZDO.Field;


namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
    /// Network Address Response value object class.
    ///
    ///
    /// The NWK_addr_rsp is generated by a Remote Device in response to a NWK_addr_req command
    /// inquiring as to the NWK address of the Remote Device or the NWK address of an address held
    /// in a local discovery cache. The destination addressing on this command is unicast.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class NetworkAddressResponse : ZdoResponse
    {
        /// <summary>
        /// The ZDO cluster ID.
        /// </summary>
        public const ushort CLUSTER_ID = 0x8000;

        /// <summary>
        /// IEEE Addr Remote Dev command message field.
        /// </summary>
        public IeeeAddress IeeeAddrRemoteDev { get; set; }

        /// <summary>
        /// NWK Addr Remote Dev command message field.
        /// </summary>
        public ushort NwkAddrRemoteDev { get; set; }

        /// <summary>
        /// Start Index command message field.
        /// </summary>
        public byte StartIndex { get; set; }

        /// <summary>
        /// NWK Addr Assoc Dev List command message field.
        /// </summary>
        public List<ushort> NwkAddrAssocDevList { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public NetworkAddressResponse()
        {
            ClusterId = CLUSTER_ID;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            base.Serialize(serializer);

            serializer.Serialize(Status, DataType.ZDO_STATUS);
            serializer.Serialize(IeeeAddrRemoteDev, DataType.IEEE_ADDRESS);
            serializer.Serialize(NwkAddrRemoteDev, DataType.NWK_ADDRESS);
            serializer.Serialize(NwkAddrAssocDevList.Count, DataType.UNSIGNED_8_BIT_INTEGER);
            serializer.Serialize(StartIndex, DataType.UNSIGNED_8_BIT_INTEGER);
            for (int cnt = 0; cnt < NwkAddrAssocDevList.Count; cnt++)
            {
                serializer.Serialize(NwkAddrAssocDevList[cnt], DataType.NWK_ADDRESS);
            }
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            base.Deserialize(deserializer);

            // Create lists
            NwkAddrAssocDevList = new List<ushort>();

            Status = deserializer.Deserialize<ZdoStatus>(DataType.ZDO_STATUS);
            if (Status != ZdoStatus.SUCCESS)
            {
                // Don't read the full response if we have an error
                return;
            }
            IeeeAddrRemoteDev = deserializer.Deserialize<IeeeAddress>(DataType.IEEE_ADDRESS);
            NwkAddrRemoteDev = deserializer.Deserialize<ushort>(DataType.NWK_ADDRESS);
            byte? numAssocDev = (byte?) deserializer.Deserialize(DataType.UNSIGNED_8_BIT_INTEGER);
            StartIndex = deserializer.Deserialize<byte>(DataType.UNSIGNED_8_BIT_INTEGER);
            if (numAssocDev != null)
            {
                for (int cnt = 0; cnt < numAssocDev; cnt++)
                {
                    NwkAddrAssocDevList.Add((ushort) deserializer.Deserialize(DataType.NWK_ADDRESS));
                }
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("NetworkAddressResponse [");
            builder.Append(base.ToString());
            builder.Append(", Status=");
            builder.Append(Status);
            builder.Append(", IeeeAddrRemoteDev=");
            builder.Append(IeeeAddrRemoteDev);
            builder.Append(", NwkAddrRemoteDev=");
            builder.Append(NwkAddrRemoteDev);
            builder.Append(", StartIndex=");
            builder.Append(StartIndex);
            builder.Append(", NwkAddrAssocDevList=");
            builder.Append(NwkAddrAssocDevList == null? "" : string.Join(", ", NwkAddrAssocDevList));
            builder.Append(']');

            return builder.ToString();
        }
    }
}
