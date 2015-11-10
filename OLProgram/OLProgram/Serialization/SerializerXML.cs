using OLModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

// Currently only for students
namespace OLProgram.Serialization
{   
    public class SerializerXML
    {
        public static SerializerXML Instance { get; } = new SerializerXML();

        private SerializerXML() { }

        // Asynchronical in order to avoid conflicts with main program
        public async void AsyncSerializeToFile(Student student, string path)
        {
            await Task.Run(() => SerializeToFile(student, path));
        }

        private void SerializeToFile(Student student, string path)
        {
            using (FileStream stream = File.Create(path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Student));
                serializer.Serialize(stream, student);
            }
        }

        public Task<Student> AsyncDeserializeFromFile(string path)
        {
            return Task.Run(() => DeserializeFromFile(path));
        }

        private Student DeserializeFromFile(string path)
        {
            using (FileStream stream = File.OpenRead(path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Student));
                Student student = serializer.Deserialize(stream) as Student;

                return student;
            }
        }

        public Task<string> AsyncSerializeToString(Student student)
        {
            return Task.Run(() => SerializeToString(student));
        }

        private string SerializeToString(Student student)
        {
            var stringBuilder = new StringBuilder();

            using (TextWriter stream = new StringWriter(stringBuilder))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Student));
                serializer.Serialize(stream, student);
            }

            return stringBuilder.ToString();
        }

        public Task<Student> AsyncDeserializeFromString(string xml)
        {
            return Task.Run(() => DeserializeFromString(xml));
        }

        private Student DeserializeFromString(string xml)
        {
            using (TextReader stream = new StringReader(xml))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Student));
                Student student = serializer.Deserialize(stream) as Student;

                return student;
            }
        }
    }
}
