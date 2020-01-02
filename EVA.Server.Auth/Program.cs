using EVA.Common.Managers;
using EVA.Common.Utils;
using EVA.Server.Auth.Config;
using EVA.Server.Common.Network;
using System;

namespace EVA.Server.Auth
{
    internal static class Program
    {
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
    }
}