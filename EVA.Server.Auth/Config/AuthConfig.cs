using EVA.Common.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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