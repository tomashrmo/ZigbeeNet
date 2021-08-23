using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.ColorControl;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.ColorControl
{
    /// <summary>
    /// Move Hue Command value object class.
    ///
    /// Cluster: Color Control. Command ID 0x01 is sent TO the server.
    /// This command is a specific command used for the Color Control cluster.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class MoveHueCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0300;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x01;

        /// <summary>
        /// Move Mode command message field.
        /// </summary>
        public byte MoveMode { get; set; }

        /// <summary>
        /// Rate command message field.
        /// </summary>
        public byte Rate { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MoveHueCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(MoveMode, DataType.ENUMERATION_8_BIT);
            serializer.Serialize(Rate, DataType.UNSIGNED_8_BIT_INTEGER);
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            MoveMode = deserializer.Deserialize<byte>(DataType.ENUMERATION_8_BIT);
            Rate = deserializer.Deserialize<byte>(DataType.UNSIGNED_8_BIT_INTEGER);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("MoveHueCommand [");
            builder.Append(base.ToString());
            builder.Append(", MoveMode=");
            builder.Append(MoveMode);
            builder.Append(", Rate=");
            builder.Append(Rate);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
