using CommunityToolkit.Mvvm.ComponentModel;
using ChocolateFactory.Models;

namespace ChocolateFactory.Data
{
    [Serializable]
    public partial class GiftModel : ObservableObject
    {
        private static int Count = 0;

        private int _id;
        private string _name = string.Empty;
        private List<Item> _items = new List<Item>();
        private string _imagePath = string.Empty;

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
            }
        }

        public List<Item> Items
        {
            get => _items;
            set
            {
                _items = value;
            }
        }

        public string ImagePath
        {
            get => _imagePath;
            set
            {
                _imagePath = value;
            }
        }
        public NutritionalInfo NutritionalInfo
        {
            get
            {
                return new NutritionalInfo(TotalProteins / (TotalWeight / 100), TotalFats / (TotalWeight / 100), TotalCarbohydrates / (TotalWeight / 100));
            }
        }

        public int TotalWeight => Items.Sum(x => x.Weight);
        public decimal TotalPrice => Items.Sum(x => x.Price);

        public double TotalCalories => Items.Sum(x => x.Calories);
        public double TotalProteins => Items.Sum(x => x.Proteins);
        public double TotalFats => Items.Sum(x => x.Fats);
        public double TotalCarbohydrates => Items.Sum(x => x.Carbohydrates);

        public GiftModel()
        {
            Id = ++Count;
        }

        public GiftModel(string name, string imagePath) : this()
        {
            Name = name;
            ImagePath = imagePath;
        }

        public void SetGiftInfo(string name, string imagePath)
        {
            Name = name;
            ImagePath = imagePath;
        }

        public void AddItem(Item item)
        {
            Items.Add(item);
        }

        public void RemoveItem(Item item)
        {
            Items.Remove(item);
        }
    }
}
