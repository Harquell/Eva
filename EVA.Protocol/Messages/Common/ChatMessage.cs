using EVA.Protocol.Interfaces;
using System;

namespace EVA.Protocol.Messages.Common
{
    public class ChatMessage : MessageBase
    {
        public new const ushort Id = 3;
        public string Auteur { get; set; }
        public string Message { get; set; }
        public DateTime SentAt { get; set; }

        protected override void SerializeProperties(IDataWriter writer)
        {
            writer.WriteString(Auteur);
            writer.WriteString(Message);
            writer.WriteLong(SentAt.Ticks);
        }

        protected override void DeserializeProperties(IDataReader reader)
        {
            Auteur = reader.ReadString();
            Message = reader.ReadString();
            SentAt = new DateTime(reader.ReadLong());
        }

        protected override ushort GetPacketId()
            => 3;
    }
}