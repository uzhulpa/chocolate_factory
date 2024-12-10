using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ChocolateFactory.Data
{
    public class XmlDatabaseManager
    {
        private readonly string _basePath;
        private readonly string _itemsFileName = "items.xml";
        private readonly string _giftsFileName = "gifts.xml";

        public string BasePath => _basePath;

        public XmlDatabaseManager()
        {
            // Получаем базовый путь к текущему домену приложения
            string currentPath = AppDomain.CurrentDomain.BaseDirectory;

            // Поднимаемся на один уровень вверх, чтобы получить путь к корневой папке решения
            _basePath = Path.GetFullPath(Path.Combine(currentPath, "..\\"));
        }

        public void SerializeToXml<T>(List<T> objects, string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
            string fullPath = Path.Combine(_basePath, filePath);

            if (!File.Exists(fullPath))
            {
                using (File.Create(fullPath)) { }
            }

            using (StreamWriter writer = new StreamWriter(fullPath))
            {
                serializer.Serialize(writer, objects);
            }
        }

        public List<T> DeserializeFromXml<T>(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
            string fullPath = Path.Combine(_basePath, filePath);

            if (!File.Exists(fullPath) || new FileInfo(fullPath).Length == 0)
            {
                return new List<T>();
            }

            using (StreamReader reader = new StreamReader(fullPath))
            {
                object deserializedObject = serializer.Deserialize(reader);
                if (deserializedObject == null)
                {
                    return new List<T>(); // Возвращаем пустой список, если десериализация вернула null
                }
                return (List<T>)deserializedObject;
            }
        }

        public void AppendToXml<T>(T obj, string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
            string fullPath = Path.Combine(_basePath, filePath);

            List<T> existingObjects = new List<T>();

            // Десериализуем существующие данные, если файл существует
            if (File.Exists(fullPath))
            {
                using (StreamReader reader = new StreamReader(fullPath))
                {
                    existingObjects = (List<T>)serializer.Deserialize(reader);
                }
            }

            // Добавляем новый объект в список
            existingObjects.Add(obj);

            // Сериализуем обновленный список обратно в файл
            using (StreamWriter writer = new StreamWriter(fullPath))
            {
                serializer.Serialize(writer, existingObjects);
            }
        }

        public void AppendListToXml<T>(List<T> objects, string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
            string fullPath = Path.Combine(_basePath, filePath);

            List<T> existingObjects = new List<T>();

            // Десериализуем существующие данные, если файл существует
            if (File.Exists(fullPath))
            {
                using (StreamReader reader = new StreamReader(fullPath))
                {
                    existingObjects = (List<T>)serializer.Deserialize(reader);
                }
            }

            // Добавляем новые объекты в список
            existingObjects.AddRange(objects);

            // Сериализуем обновленный список обратно в файл
            using (StreamWriter writer = new StreamWriter(fullPath))
            {
                serializer.Serialize(writer, existingObjects);
            }
        }

        public void SaveItems(List<Item> items)
        {
            SerializeToXml(items, _itemsFileName);
        }

        public List<Item> LoadItems()
        {
            return DeserializeFromXml<Item>(_itemsFileName);
        }

        public void PlaceGift(GiftModel model)
        {
            var gift = new Gift
            {
                Name = model.Name,
                ImagePath = model.ImagePath
            };
            foreach (var item in model.Items)
            {

            }
        }

        public void SaveGifts(List<Gift> gifts)
        {
            SerializeToXml(gifts, _giftsFileName);
        }

        public List<Gift> LoadGifts()
        {
            return DeserializeFromXml<Gift>(_giftsFileName);
        }

        public void FillWithTestData()
        {
            string itemsFilePath = Path.Combine(_basePath, "items.xml");
            string giftsFilePath = Path.Combine(_basePath, "gifts.xml");

            bool itemsFileExistsAndNotEmpty = File.Exists(itemsFilePath) && new FileInfo(itemsFilePath).Length > 0;
            bool giftsFileExistsAndNotEmpty = File.Exists(giftsFilePath) && new FileInfo(giftsFilePath).Length > 0;

            if (!itemsFileExistsAndNotEmpty)
            {
                var items = SeedData.GetItems();
                SaveItems(items);
            }

            if (!giftsFileExistsAndNotEmpty)
            {
                var gifts = SeedData.GetGifts();
                SaveGifts(gifts);
            }
        }
    }
}
