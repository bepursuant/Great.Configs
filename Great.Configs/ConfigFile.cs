using System;
using System.Xml.Serialization;

namespace Great.Configs
{
    public class ConfigFile
    {
        [XmlIgnore]
        public bool IsLoaded { get; set; }

        [XmlIgnore]
        public Exception LoadException { get; set; }

        public ConfigFile()
        {

        }

    }
}
