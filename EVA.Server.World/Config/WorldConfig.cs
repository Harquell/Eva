using EVA.Common.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EVA.Server.World.Config
{
    class WorldConfig : ConfigurationBase
    {
        public override string FileName => "config.xml";
        public string NomServeur { get; set; }

        public WorldConfig() : base(Directory.GetCurrentDirectory())
        {

        }
    }
}
