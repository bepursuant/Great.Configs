using Great.Configs;
using Great.Configs.Tests;
using Xunit;

namespace Great.EmvTags.Tests
{

    public class XmlConfigFileTests
    {

        [Fact]
        [Trait("Build", "Run")]
        public void Constructor_ShouldReturn_Defaults()
        {
            var c = new TestXmlConfig();

            Assert.NotNull(c);
            Assert.IsType<TestXmlConfig>(c);
        }

        [Fact]
        [Trait("Build", "Run")]
        public void Write_ShouldCreate_XmlFile()
        {
            var c = new TestXmlConfig();
            c.DbConnectionString = "Host=test;User=asdf;";
            c.EnableTestMode = false;
            c.WorkerThreads = 64;
            c.Write("TestXmlFile.xml");
        }

        [Fact]
        [Trait("Build", "Run")]
        public void Load_ShouldReturn_ConfigObjectWithValues()
        {
            TestXmlConfig c = TestXmlConfig.Load<TestXmlConfig>("TestXmlFile.xml");

            

        }

    }
}
