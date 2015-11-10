using OLProgram.OLModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

// Currently only for users
namespace OLProgram.Serialization
{   
    public class SerializerXML
    {
        public static SerializerXML Instance { get; } = new SerializerXML();

        private SerializerXML() { }

        // Asynchronical in order to avoid conflicts with main program
        public async void AsyncSerializeToFile(User user, string path)
        {
            await Task.Run(() => SerializeToFile(user, path));
        }

        private void SerializeToFile(User user, string path)
        {
            using (FileStream stream = File.Create(path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(User));
                serializer.Serialize(stream, user);
            }
        }

        public Task<User> AsyncDeserializeFromFile(string path)
        {
            return Task.Run(() => DeserializeFromFile(path));
        }

        private User DeserializeFromFile(string path)
        {
            using (FileStream stream = File.OpenRead(path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(User));
                User user = serializer.Deserialize(stream) as User;

                return user;
            }
        }

        public Task<string> AsyncSerializeToString(User user)
        {
            return Task.Run(() => SerializeToString(user));
        }

        private string SerializeToString(User user)
        {
            var stringBuilder = new StringBuilder();

            using (TextWriter stream = new StringWriter(stringBuilder))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(User));
                serializer.Serialize(stream, user);
            }

            return stringBuilder.ToString();
        }

        public Task<User> AsyncDeserializeFromString(string xml)
        {
            return Task.Run(() => DeserializeFromString(xml));
        }

        private User DeserializeFromString(string xml)
        {
            using (TextReader stream = new StringReader(xml))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(User));
                User user = serializer.Deserialize(stream) as User;

                return user;
            }
        }
    }
}
