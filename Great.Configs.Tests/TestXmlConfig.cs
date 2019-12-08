using System.Collections.Generic;

namespace Great.Configs.Tests
{
    public class TestXmlConfig : XmlConfigFile
    {
        public string DbConnectionString { get; set; }

        public int WorkerThreads { get; set; }

        public bool EnableTestMode { get; set; }
    }
}
