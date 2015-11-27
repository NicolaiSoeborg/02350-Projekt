using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

// TODO: Currently only for users
namespace OLProgram.OLModel
{   
    public class SerializerXML
    {
        public static SerializerXML Instance { get; } = new SerializerXML();

        private SerializerXML() { }
        private static string USER_PATH = "users";
        private static string PRODUCT_PATH = "products";

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
