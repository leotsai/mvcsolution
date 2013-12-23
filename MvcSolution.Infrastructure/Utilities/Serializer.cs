using System.IO;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace MvcSolution.Infrastructure.Utilities
{
    public static class Serializer
    {
        public static string ToJson(object entity)
        {
            return new JavaScriptSerializer().Serialize(entity);
        }

        public static T FromJson<T>(string json)
        {
            return new JavaScriptSerializer().Deserialize<T>(json);
        }

        public static string ToXml<T>(T obj)
        {
            var serializer = new XmlSerializer(typeof (T));
            using (var stream = new MemoryStream())
            {
                serializer.Serialize(stream, obj);
                stream.Position = 0;
                var reader = new StreamReader(stream);
                var xml = reader.ReadToEnd();
                reader.Close();
                return xml;
            }
        }

        public static T FromXml<T>(string xml)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var stream = new MemoryStream())
            {
                var writer = new StreamWriter(stream);
                writer.Write(xml);
                writer.Flush();
                stream.Position = 0;
                T instance = (T)serializer.Deserialize(stream);
                writer.Close();
                return instance;
            }
        }

    }
}
