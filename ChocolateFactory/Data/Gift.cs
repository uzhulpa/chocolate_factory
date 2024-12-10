using CommunityToolkit.Mvvm.ComponentModel;
using ChocolateFactory.Models;

namespace ChocolateFactory.Data
{
    [Serializable]
    public partial class Gift
    {
        private static int Count = 0;

        private int _id;
        private string _name = string.Empty;
        private List<GiftItem> _giftItems = new List<GiftItem>();
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

        public List<GiftItem> GiftItems
        {
            get => _giftItems;
            set
            {
                _giftItems = value;
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

        public int TotalWeight => GiftItems.Sum(x => x.Weight);
        public decimal TotalPrice => GiftItems.Sum(x => x.Price);

        public double TotalCalories => GiftItems.Sum(x => x.Calories);
        public double TotalProteins => GiftItems.Sum(x => x.Proteins);
        public double TotalFats => GiftItems.Sum(x => x.Fats);
        public double TotalCarbohydrates => GiftItems.Sum(x => x.Carbohydrates);

        public Gift()
        {
            Id = ++Count;
        }

        public Gift(string name, string imagePath) : this()
        {
            Name = name;
            ImagePath = imagePath;
        }
    }
}
