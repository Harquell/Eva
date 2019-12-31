using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace EVA.Common.Types
{
    [Serializable]
    public abstract class ConfigurationBase
    {
        [XmlIgnore]
        private string _directoryPath;

        [XmlIgnore]
        public abstract string FileName { get; }

        [XmlIgnore]
        public string Path => _directoryPath + "\\" + FileName;

        protected ConfigurationBase(string dirPath)
        {
            _directoryPath = dirPath;
        }

        public static void Serialize<T>(ConfigurationBase c) where T : ConfigurationBase
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            Directory.CreateDirectory(c._directoryPath);
            using StreamWriter writer = File.CreateText(c.Path);
            serializer.Serialize(writer, c);
            writer.Flush();
            writer.Close();
        }

        public static T Deserialize<T>(string path) where T : ConfigurationBase
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                StreamReader reader = File.OpenText(path);
                T Config = (T)serializer.Deserialize(reader);
                reader.Close();
                return Config;
            }
            catch
            {
                return null;
            }
        }
    }
}