using System;
using System.Collections.Generic;
using System.Text;

namespace EVA.Protocol.Messages
{
    public interface IMessageBase
    {
        public abstract ushort PacketId { get; }

        public abstract byte[] Serialize();

        protected abstract IMessageBase Deserialize();
    }

    public abstract class MessageBase<T> : IMessageBase where T : IMessageBase
    {
        public abstract ushort PacketId { get; }

        protected abstract T Deserialize();

        public abstract byte[] Serialize();

        IMessageBase IMessageBase.Deserialize()
            => Deserialize();
    }
}