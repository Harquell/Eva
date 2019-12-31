using EVA.Protocol.Interfaces;
using System;

namespace EVA.Protocol.Messages.Common
{
    public class PongMessage : MessageBase
    {
        public new const ushort Id = 2;

        public DateTime PongTime { get; set; }
        public Guid TargetedPing { get; set; }

        protected override void SerializeProperties(IDataWriter writer)
        {
            writer.WriteLong(PongTime.Ticks);
            writer.WriteBytes(TargetedPing.ToByteArray());
        }

        protected override void DeserializeProperties(IDataReader reader)
        {
            PongTime = new DateTime(reader.ReadLong());
            TargetedPing = new Guid(reader.ReadBytes());
        }
    }
}