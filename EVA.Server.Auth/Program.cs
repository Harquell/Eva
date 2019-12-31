using EVA.Common.Managers;
using EVA.Server.Auth.Config;
using EVA.Server.Common.Network;
using System;

namespace EVA.Server.Auth
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.Title = "[Auth] Eva Server (starting ...)";

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