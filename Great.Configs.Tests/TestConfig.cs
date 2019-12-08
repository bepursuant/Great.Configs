using System.Collections.Generic;

namespace Great.Configs.Tests
{
    public class TestConfig : ConfigFile
    {
        public string DbConnectionString { get; set; } = "Host=test.foo;User=Bar;";

        public int WorkerThreads { get; set; } = 10;

        public bool EnableTestMode { get; set; } = false;

        public List<string> TestValues { get; set; } = new List<string>() { "test", "asdf" };
    }
}
