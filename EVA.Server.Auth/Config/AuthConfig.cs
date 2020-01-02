using EVA.Common.Types;
using System.IO;

namespace EVA.Server.Auth.Config
{
    public class AuthConfig : ConfigurationBase
    {
        public override string FileName => "config.xml";
        public string IPAddress { get; set; }
        public int Port { get; set; }

        public AuthConfig() : base(Directory.GetCurrentDirectory())
        {
            IPAddress = "";
            Port = 0;
        }
    }
}