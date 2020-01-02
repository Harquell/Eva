using EVA.Protocol.Interfaces;
using System;

namespace EVA.Protocol.Messages
{
    public abstract class MessageBase
    {
        public const ushort Id = 0;
        public ushort PacketId => Id;

        public Guid PacketUId { get; set; }

        protected MessageBase()
        {
            PacketUId = Guid.NewGuid();
        }

        protected abstract void SerializeProperties(IDataWriter writer);

        protected abstract void DeserializeProperties(IDataReader reader);

        public void Serialize(IDataWriter writer)
        {
            writer.WriteUShort(PacketId);
            writer.WriteBytes(PacketUId.ToByteArray());
            SerializeProperties(writer);
        }

        public void Deserialize(IDataReader reader)
        {
            PacketUId = new Guid(reader.ReadBytes());
            DeserializeProperties(reader);
        }
    }
}