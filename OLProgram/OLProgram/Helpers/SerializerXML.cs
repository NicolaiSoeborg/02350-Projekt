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
        private static string USER_PATH = "users";
        private static string PRODUCT_PATH = "products";


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

        public Task<string> AsyncSerializeToString(Data data)
        {
            return Task.Run(() => SerializeToString(data));
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

        private string SerializeToString(Data data)
        {
            var stringBuilder = new StringBuilder();

            using (TextWriter stream = new StringWriter(stringBuilder))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Data));
                serializer.Serialize(stream, data);
            }

            return stringBuilder.ToString();
        }


        public async void ASyncSaveUser(User user)
        {
            await Task.Run(() => SaveDataToFile(user.Serialize(), USER_PATH, user.Name));
        }

        public async void ASyncSaveProduct(Product product)
        {
            await Task.Run(() => SaveDataToFile(product.Serialize(), PRODUCT_PATH, product.ProductName));
        }

        private void SaveDataToFile(MemoryStream xml, string path, string name)
        {
            string filename = String.Format("{0}/{1}.xml", path, name);

            if (File.Exists(filename))
                File.Delete(filename); // TODO: better handling? Move?

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            using (Stream file = File.Create(filename))
                xml.CopyTo(file);
        }

        public User LoadUser(string name)
        {
            MemoryStream xml = LoadDataFromFile(USER_PATH, name);
            return User.Deserialize(xml);
        }

        public Product LoadProduct(string name)
        {
            MemoryStream xml = LoadDataFromFile(PRODUCT_PATH, name);
            return Product.Deserialize(xml);
        }

        private MemoryStream LoadDataFromFile(string path, string name)
        {
            string filename = String.Format("{0}/{1}.xml", path, name);

            if (!File.Exists(filename))
                return null;

            MemoryStream data = new MemoryStream();
            using (Stream file = File.Open(filename, FileMode.Open))
                data.CopyTo(data);

            return data;
        }
    }
}