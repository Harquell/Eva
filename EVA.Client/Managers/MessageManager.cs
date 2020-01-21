using EVA.Common.Interfaces;
using EVA.Common.Utils;
using EVA.Protocol.Messages;
using EVA.Protocol.Utils;
using EVA.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EVA.Client.Network;

namespace EVA.Client.Managers
{
    public class MessageManager : IInitializable
    {
        public static MessageManager Instance => _instance ?? (_instance = new MessageManager());
        private static MessageManager _instance;

        public delegate void HandleMessageDelegate(MessageBase message);

        private Dictionary<ushort, Type> _messageTypes;
        private Dictionary<ushort, HandleMessageDelegate> _messageHandlers;

        private MessageManager()
        {
            _messageTypes = new Dictionary<ushort, Type>();
            _messageHandlers = new Dictionary<ushort, HandleMessageDelegate>();
        }

        public void Init()
        {
            try
            {
                Logger.Info("Init MessageManager");
                InitMessageTypes();
                InitMessageHandlers();
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void InitMessageTypes()
        {
            Logger.Info("Initializing message types");
            Assembly messageAssembly = typeof(MessageBase).Assembly;
            _messageTypes = messageAssembly
                .GetTypes()
                .Where(x => x.IsSubclassOf(typeof(MessageBase)))
                .ToDictionary(t => (ushort)t.GetField("Id").GetValue(null), t => t);
            Logger.Debug(string.Format("{0} messages founded", _messageTypes.Count));
        }

        private void InitMessageHandlers()
        {
            Logger.Info("Initializing message handlers");
            Assembly assembly = Assembly.GetEntryAssembly();

            _messageHandlers = assembly.GetTypes()
                .SelectMany(t => t.GetMethods())
                .Where(m => m.GetCustomAttributes<MessageHandlerAttribute>().Any())
                .ToDictionary(
                    m => m.GetCustomAttribute<MessageHandlerAttribute>().PacketId,
                    m => (HandleMessageDelegate)Delegate.CreateDelegate(typeof(HandleMessageDelegate), m)
                );
            Logger.Debug(string.Format("{0} handlers founded", _messageHandlers.Count));
        }

        public void HandleMessage(byte[] message, TcpClient client)
        {
            using BigEndianReader reader = new BigEndianReader(message);
            ushort packetId = reader.ReadUShort();
            if (!_messageTypes.ContainsKey(packetId))
            {
                Logger.Error(string.Format("Received PacketId({0}) from {1} doesn't correspond to a message type", packetId, client.EndPoint));
                return;
            }
            if (!_messageHandlers.ContainsKey(packetId))
            {
                Logger.Error(string.Format("Received PacketId({0}) from {1} not handled", packetId, client.EndPoint));
                return;
            }
            MessageBase msg = Activator.CreateInstance(_messageTypes[packetId]) as MessageBase;
            msg.Deserialize(reader);

            Logger.Debug(string.Format("new packet data [{0}]{1}", packetId, msg.GetType().Name));

            _messageHandlers[packetId](msg);
        }
    }
}