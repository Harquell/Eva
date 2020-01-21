using System;

namespace EVA.Common.Attributes
{
    public class MessageHandlerAttribute : Attribute
    {
        public ushort PacketId { get; set; }

        public MessageHandlerAttribute(ushort id)
        {
            PacketId = id;
        }
    }
}