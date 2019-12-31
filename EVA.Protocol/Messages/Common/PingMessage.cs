using EVA.Protocol.Interfaces;
using System;

namespace EVA.Protocol.Messages.Common
{
    public class PingMessage : MessageBase
    {
        public new const ushort Id = 1;

        public DateTime PingTime { get; set; }

        protected override void SerializeProperties(IDataWriter writer)
        {
            writer.WriteLong(PingTime.Ticks);
        }

        protected override void DeserializeProperties(IDataReader reader)
        {
            PingTime = new DateTime(reader.ReadLong());
        }
    }
}