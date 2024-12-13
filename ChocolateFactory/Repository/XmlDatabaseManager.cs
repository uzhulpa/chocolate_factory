using ChocolateFactory.Data;
using ChocolateFactory.Messages;
using ChocolateFactory.Models;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ChocolateFactory.Repository
{
    public class XmlDatabaseManager
    {
        private readonly string _basePath;
        private readonly string _itemsFileName = "items.xml";
        private readonly string _giftsFileName = "gifts.xml";

        public string BasePath => _basePath;

        public XmlDatabaseManager()
        {
            string currentPath = AppDomain.CurrentDomain.BaseDirectory;
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
                    return new List<T>();
                }
                return (List<T>)deserializedObject;
            }
        }

        public void AppendToXml<T>(T obj, string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
            string fullPath = Path.Combine(_basePath, filePath);

            List<T> existingObjects = new List<T>();

            if (File.Exists(fullPath))
            {
                using (StreamReader reader = new StreamReader(fullPath))
                {
                    existingObjects = (List<T>)serializer.Deserialize(reader);
                }
            }

            existingObjects.Add(obj);

            using (StreamWriter writer = new StreamWriter(fullPath))
            {
                serializer.Serialize(writer, existingObjects);
            }
        }

        public void AppendItemToXml(Item obj, string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Item>));
            string fullPath = Path.Combine(_basePath, filePath);

            List<Item> existingObjects = new List<Item>();

            if (File.Exists(fullPath))
            {
                using (StreamReader reader = new StreamReader(fullPath))
                {
                    existingObjects = (List<Item>)serializer.Deserialize(reader);
                }
            }

            existingObjects.Add(obj);

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

            if (File.Exists(fullPath))
            {
                using (StreamReader reader = new StreamReader(fullPath))
                {
                    existingObjects = (List<T>)serializer.Deserialize(reader);
                }
            }

            existingObjects.AddRange(objects);

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

        public void SaveGifts(List<Gift> gifts)
        {
            SerializeToXml(gifts, _giftsFileName);
        }

        public List<Gift> LoadGifts()
        {
            return DeserializeFromXml<Gift>(_giftsFileName);
        }

        public List<GiftItem> GetGiftItems(Guid giftId)
        {
            List<Gift> gifts = LoadGifts();
            Gift? gift = gifts.FirstOrDefault(g => g.Id == giftId);

            if (gift != null)
            {
                return gift.GiftItems;
            }

            return new List<GiftItem>();
        }

        public void PlaceGift(GiftModel giftModel)
        {
            var gift = new Gift(giftModel.Name, giftModel.ImagePath);
            foreach (var giftItem in giftModel.GiftItems)
            {
                gift.GiftItems.Add(giftItem);
            }

            AppendToXml(gift, _giftsFileName);

            WeakReferenceMessenger.Default.Send(new GiftCreatedMessage(gift));
        }

        public void SaveItem(ItemModel itemModel)
        {
            if (itemModel.Id == 0)
            {
                // создание нового элемента
                Candy newItem = new()
                {
                    Name = itemModel.Name,
                    Price = itemModel.Price,
                    Weight = itemModel.Weight,
                    ImagePath = itemModel.ImagePath,
                    NutritionalInfo = itemModel.NutritionalInfo,
                };

                AppendItemToXml(newItem, _itemsFileName);
            }
            else
            {
                // обновление существующего элемента
                var items = LoadItems();
                var oldItem = items.Find(x => x.Id == itemModel.Id);

                if (oldItem == null) return;

                int index = items.IndexOf(oldItem);

                items[index].Name = itemModel.Name;
                items[index].Price = itemModel.Price;
                items[index].Weight = itemModel.Weight;
                items[index].ImagePath = itemModel.ImagePath;
                items[index].NutritionalInfo = itemModel.NutritionalInfo;

                SaveItems(items);
            }
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
