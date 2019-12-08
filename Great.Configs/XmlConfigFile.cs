using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Great.Configs
{
    public class XmlConfigFile
    {

        public XmlConfigFile()
        {

        }

        /// <summary>
        /// Load configuration from an XML file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath">Filesystem path to load configuration file from</param>
        /// <returns>Deserialized configuration file, or default on exception</returns>
        public static T Load<T>(string filePath) where T : new()
        {
            try
            {
                using (var stream = File.OpenRead(filePath))
                {
                    var ser = new XmlSerializer(typeof(T));
                    return (T)ser.Deserialize(stream);
                }
            }
            catch (Exception)
            {
                return new T();
            }
        }

        /// <summary>
        /// Write configuration to an XML file
        /// </summary>
        /// <param name="filePath">Filesystem path to write configuration file to</param>
        public void Write(string filePath)
        {
            var serializer = new XmlSerializer(GetType());
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            using (var stream = new StreamWriter(filePath))
            {
                using (var writer = XmlWriter.Create(stream, new XmlWriterSettings() { Indent = true }))
                {
                    serializer.Serialize(writer, this, ns);
                }
            }
        }
    }
}
