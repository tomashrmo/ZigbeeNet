using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.Price;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.Price
{
    /// <summary>
    /// Publish Cpp Event Command value object class.
    ///
    /// Cluster: Price. Command ID 0x0B is sent FROM the server.
    /// This command is a specific command used for the Price cluster.
    ///
    /// The PublishCPPEvent command is sent from an ESI to its Price clients to notify them of a
    /// Critical Peak Pricing (CPP) event. <br> When the PublishCPPEvent command is received,
    /// the IHD or Meter shall act in one of two ways: 1. It shall notify the consumer that there is a
    /// CPP event that requires acknowledgment. The acknowledgement shall be either to accept
    /// the CPPEvent or reject the CPPEvent (in which case it shall send the CPPEventResponse
    /// command, with the CPPAuth parameter set to Accepted or Rejected). It is recommended
    /// that the CPP event is ignored until a consumer either accepts or rejects the event. 2. The
    /// CPPAuth parameter is set to “Forced”, in which case the CPPEvent has been accepted.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class PublishCppEventCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0700;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x0B;

        /// <summary>
        /// Provider ID command message field.
        /// 
        /// An unsigned 32-bit field containing a unique identifier for the commodity
        /// provider. This field allows differentiation in deregulated markets where
        /// multiple commodity providers may be available.
        /// </summary>
        public uint ProviderId { get; set; }

        /// <summary>
        /// Issuer Event ID command message field.
        /// 
        /// Unique identifier generated by the commodity provider. When new information is
        /// provided that replaces older information for the same time period, this field
        /// allows devices to determine which information is newer. The value contained in
        /// this field is a unique number managed by upstream servers or a UTC based time stamp
        /// (UTCTime data type) identifying when the Publish command was issued. Thus, newer
        /// information will have a value in the Issuer Event ID field that is larger than older
        /// information. A start date/time of 0x00000000 shall indicate that the command
        /// should be executed immediately. A start date/time of 0xFFFFFFFF shall cause an
        /// existing PublishCPPEvent command with the same Provider ID and Issuer Event ID to
        /// be cancelled (note that, in markets where permanently active price information is
        /// required for billing purposes, it is recommended that a replacement/superseding
        /// PublishCPPEvent command is used in place of this cancellation mechanism).
        /// Duration in Minutes: Defines the duration of the CPP event.
        /// </summary>
        public uint IssuerEventId { get; set; }

        /// <summary>
        /// Start Time command message field.
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Duration In Minutes command message field.
        /// </summary>
        public ushort DurationInMinutes { get; set; }

        /// <summary>
        /// Tariff Type command message field.
        /// 
        /// An 8-bit bitmap identifying the type of tariff published in this command. The least
        /// significant nibble represents an enumeration of the tariff type (Generation
        /// Meters shall use the ‘Received’ Tariff). The most significant nibble is reserved.
        /// </summary>
        public byte TariffType { get; set; }

        /// <summary>
        /// Cpp Price Tier command message field.
        /// 
        /// An 8-bit enumeration identifying the price tier associated with this CPP event.
        /// The price(s) contained in the active price matrix for that price tier will override
        /// the normal pricing scheme. Prices ‘CPP1’ and ‘CPP2’ are reserved for this
        /// purposes.
        /// </summary>
        public byte CppPriceTier { get; set; }

        /// <summary>
        /// Cpp Auth command message field.
        /// 
        /// An 8-bit enumeration identifying the status of the CPP event:
        /// </summary>
        public byte CppAuth { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PublishCppEventCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(ProviderId, DataType.UNSIGNED_32_BIT_INTEGER);
            serializer.Serialize(IssuerEventId, DataType.UNSIGNED_32_BIT_INTEGER);
            serializer.Serialize(StartTime, DataType.UTCTIME);
            serializer.Serialize(DurationInMinutes, DataType.UNSIGNED_16_BIT_INTEGER);
            serializer.Serialize(TariffType, DataType.BITMAP_8_BIT);
            serializer.Serialize(CppPriceTier, DataType.ENUMERATION_8_BIT);
            serializer.Serialize(CppAuth, DataType.ENUMERATION_8_BIT);
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            ProviderId = deserializer.Deserialize<uint>(DataType.UNSIGNED_32_BIT_INTEGER);
            IssuerEventId = deserializer.Deserialize<uint>(DataType.UNSIGNED_32_BIT_INTEGER);
            StartTime = deserializer.Deserialize<DateTime>(DataType.UTCTIME);
            DurationInMinutes = deserializer.Deserialize<ushort>(DataType.UNSIGNED_16_BIT_INTEGER);
            TariffType = deserializer.Deserialize<byte>(DataType.BITMAP_8_BIT);
            CppPriceTier = deserializer.Deserialize<byte>(DataType.ENUMERATION_8_BIT);
            CppAuth = deserializer.Deserialize<byte>(DataType.ENUMERATION_8_BIT);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("PublishCppEventCommand [");
            builder.Append(base.ToString());
            builder.Append(", ProviderId=");
            builder.Append(ProviderId);
            builder.Append(", IssuerEventId=");
            builder.Append(IssuerEventId);
            builder.Append(", StartTime=");
            builder.Append(StartTime);
            builder.Append(", DurationInMinutes=");
            builder.Append(DurationInMinutes);
            builder.Append(", TariffType=");
            builder.Append(TariffType);
            builder.Append(", CppPriceTier=");
            builder.Append(CppPriceTier);
            builder.Append(", CppAuth=");
            builder.Append(CppAuth);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
