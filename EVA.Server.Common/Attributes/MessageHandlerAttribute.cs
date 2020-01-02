using System;

namespace EVA.Server.Common.Attributes
{
    internal class MessageHandlerAttribute : Attribute
    {
        public ushort PacketId { get; set; }

        public MessageHandlerAttribute(ushort id)
        {
            PacketId = id;
        }
    }
}