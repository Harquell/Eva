using EVA.Common.Managers;
using EVA.Common.Utils;
using EVA.Protocol.Messages.Common;
using EVA.Server.Auth.Config;
using EVA.Server.Auth.Types;
using EVA.Server.Common.Network;
using EVA.Common.Attributes;
using System;
using EVA.Protocol.Messages;
using EVA.Server.Common.Interfaces;
using EVA.Common.Attributes;
using System.Linq;

namespace EVA.Server.Auth
{
    internal static class Program
    {
        static TcpServer _server;
        private static void Main(string[] args)
        {
            Console.Title = "[Auth] Eva Server (starting ...)";
            Logger.Instance.LoggerLevel = Logger.LoggerType.DEBUG;

            var authCfgMngr = ConfigurationManager<AuthConfig>.Instance; // Parfait
            authCfgMngr.Load();

            Console.WriteLine("Server address => {0}:{1}", authCfgMngr.Config.IPAddress, authCfgMngr.Config.Port);

            using TcpServer server = new TcpServer(authCfgMngr.Config.IPAddress, authCfgMngr.Config.Port);
            try
            {
                _server = server;
                server.Init();
                server.Start();
            }
            catch
            {
                authCfgMngr.Save();
            }

            Console.Title = string.Format("[Auth] Eva Server ({0}:{1})", authCfgMngr.Config.IPAddress, authCfgMngr.Config.Port);

            if (server.IsRunning)
            {
                Console.WriteLine("Server running");
            }
            else
            {
                Console.WriteLine("Server not running !");
            }

            Console.Read();
            authCfgMngr.Save();
        }

        [MessageHandler(1)]
        public static void PingHandler(MessageBase message, IClientData clientData)
        {
            var msg = message as PingMessage;
            Console.WriteLine("Wallah j'ai un ping : " + msg.PingTime);
        }

        [MessageHandler(3)]
        public static void ChatHandler(MessageBase message, IClientData clientData)
        {
            var msg = message as ChatMessage;
            foreach(var client in _server.Clients.Where(x => x.ClientData != clientData))
            {
                client.SendMessage(msg);
            }
        }
    }
}