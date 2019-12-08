using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Great.Configs
{
    public static class ConfigManager
    {

        /// <summary>
        /// Load configuration from an XML file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath">Filesystem path to load configuration file from</param>
        /// <returns>Deserialized configuration file, or default on exception</returns>
        public static T GetConfig<T>(string filePath = null, bool throwExceptions = false) where T : ConfigFile, new()
        {
            try
            {
                if (filePath == null)
                {               
                    // if no filePath is provided, we will just return the default T
                    return new T() {
                        IsLoaded = false
                    };
                }
                else
                {
                    // otherwise, try loading from the provided file path
                    using (var stream = File.OpenRead(filePath))
                    {
                        var ser = new XmlSerializer(typeof(T));
                        var conf = ser.Deserialize(stream) as T;
                        conf.IsLoaded = true;
                        return conf;
                    }
                }

            }
            catch (Exception ex)
            {
                if (!throwExceptions)
                {
                    return new T()
                    {
                        IsLoaded = false,
                        LoadException = ex
                    };
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Write configuration to an XML file
        /// </summary>
        /// <param name="filePath">Filesystem path to write configuration file to</param>
        public static void SaveConfig<T>(this T conf, string filePath) where T : ConfigFile
        {
            var serializer = new XmlSerializer(conf.GetType());
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            using (var stream = new StreamWriter(filePath))
            {
                using (var writer = XmlWriter.Create(stream, new XmlWriterSettings() { Indent = true }))
                {
                    serializer.Serialize(writer, conf, ns);
                }
            }
        }
    }
}
