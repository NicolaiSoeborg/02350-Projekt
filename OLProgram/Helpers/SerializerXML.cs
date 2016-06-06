using OLModel;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace OLProgram.Helpers
{
    public class SerializerXML
    {
        public static SerializerXML Instance { get; } = new SerializerXML();

        private SerializerXML() { }

        public Task<Data> AsyncDeserializeFromFile(string path)
        {
            return Task.Run(() => DeserializeFromFile(path));
        }

        private Data DeserializeFromFile(string path)
        {
            using (FileStream stream = File.OpenRead(path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Data));
                Data data = serializer.Deserialize(stream) as Data;

                return data;
            }
        }

        public async void AsyncSerializeToFile(Data data, string path)
        {
            await Task.Run(() => SerializeToFile(data, path));
        }

        private void SerializeToFile(Data data, string path)
        {
            using (FileStream stream = File.Create(path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Data));
                serializer.Serialize(stream, data);
            }
        }
    }
}