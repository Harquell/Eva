using EVA.Protocol.Interfaces;

namespace EVA.Protocol.Messages.Common
{
    public class ProtocolInfosMessage : MessageBase
    {
        public new const ushort Id = 99;

        public ushort ProtocolRequired { get; set; }
        public ushort ProtocolVersion { get; set; }

        protected override void SerializeProperties(IDataWriter writer)
        {
            writer.WriteUShort(ProtocolRequired);
            writer.WriteUShort(ProtocolVersion);
        }

        protected override void DeserializeProperties(IDataReader reader)
        {
            ProtocolRequired = reader.ReadUShort();
            ProtocolVersion = reader.ReadUShort();
        }

        protected override ushort GetPacketId()
            => 99;
    }
}