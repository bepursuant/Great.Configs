using System.IO;
using Xunit;

namespace Great.Configs.Tests
{

    public class ConfigManagerTests
    {

        [Fact]
        [Trait("Build", "Run")]
        public void GetConfig_ShouldReturnDefault_WithNoParameters()
        {
            var c = ConfigManager.GetConfig<TestConfig>();

            Assert.NotNull(c);
            Assert.IsType<TestConfig>(c);

            Assert.Equal("Host=test.foo;User=Bar;", c.DbConnectionString);
            Assert.Equal(10, c.WorkerThreads);
            Assert.False(c.EnableTestMode);
        }

        [Fact]
        [Trait("Build", "Run")]
        public void GetConfig_ShouldReturnDefaultAndException_WithInvalidPath()
        {
            var c = ConfigManager.GetConfig<TestConfig>("nonexistentfile.xml");

            Assert.NotNull(c);
            Assert.IsType<TestConfig>(c);

            Assert.Equal("Host=test.foo;User=Bar;", c.DbConnectionString);
            Assert.Equal(10, c.WorkerThreads);
            Assert.False(c.EnableTestMode);
            Assert.False(c.IsLoaded);
            Assert.IsType<FileNotFoundException>(c.LoadException);
        }

        [Fact]
        [Trait("Build", "Run")]
        public void GetConfig_ShouldThrowException_WithInvalidPathAndThrowExceptionsTrue()
        {
            Assert.ThrowsAny<FileNotFoundException>(() => ConfigManager.GetConfig<TestConfig>("nonexistentfile.xml", true));
        }

        [Fact]
        [Trait("Build", "Run")]
        public void SaveConfig_ShouldWork()
        {
            var c = ConfigManager.GetConfig<TestConfig>();
            c.EnableTestMode = true;
            c.WorkerThreads = 400;

            c.SaveConfig("TestConfigCFG.xml");

            Assert.True(File.Exists("TestConfigCFG.xml"));
            File.Delete("TestConfigCFG.xml");
        }

        [Fact]
        [Trait("Build", "Run")]
        public void LoadConfig_ShouldWork_WithValidPath()
        {
            var c = ConfigManager.GetConfig<TestConfig>();
            c.EnableTestMode = true;
            c.WorkerThreads = 400;

            c.SaveConfig("TestConfigCFG.xml");

            var d = ConfigManager.GetConfig<TestConfig>("TestConfigCFG.xml");
            Assert.NotNull(d);
            Assert.IsType<TestConfig>(d);

            Assert.Equal(400, d.WorkerThreads);
            Assert.True(d.EnableTestMode);
            File.Delete("TestConfigCFG.xml");
        }
    }
}
