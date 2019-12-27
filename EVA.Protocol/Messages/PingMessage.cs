using System;
using System.Collections.Generic;
using System.Text;

namespace EVA.Protocol.Messages
{
    public class PingMessage : MessageBase<PingMessage>
    {
        public override ushort PacketId => throw new NotImplementedException();

        public override byte[] Serialize()
        {
            throw new NotImplementedException();
        }

        protected override PingMessage Deserialize()
        {
            throw new NotImplementedException();
        }
    }
}