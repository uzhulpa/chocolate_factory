using CommunityToolkit.Mvvm.ComponentModel;
using ChocolateFactory.Models;

namespace ChocolateFactory.Data
{
    [Serializable]
    public class Gift
    {
        private Guid _id;
        private string _name = string.Empty;
        private List<GiftItem> _giftItems = new List<GiftItem>();
        private string _imagePath = string.Empty;

        public Guid Id
        {
            get => _id;
            set
            {
                _id = value;
            }
        }

        public string stringId => Id.ToString().Substring(0, 8);

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
                if (TotalCalories == 0) return new NutritionalInfo();
                return new NutritionalInfo(TotalProteins / (TotalWeight / 100.0m), TotalFats / (TotalWeight / 100.0m), TotalCarbohydrates / (TotalWeight / 100.0m));
            }
        }

        public int TotalWeight => GiftItems.Sum(x => x.TotalWeight);
        public decimal TotalPrice => GiftItems.Sum(x => x.Price * x.Quantity);
        public int NumberOfElements => GiftItems.Sum(x => x.Quantity);

        public decimal TotalCalories => GiftItems.Sum(x => x.Calories);
        public decimal TotalProteins => GiftItems.Sum(x => x.Proteins);
        public decimal TotalFats => GiftItems.Sum(x => x.Fats);
        public decimal TotalCarbohydrates => GiftItems.Sum(x => x.Carbohydrates);

        public Gift()
        {
            _id = Guid.NewGuid();
        }

        public Gift(string name, string imagePath) : this()
        {
            Name = name;
            ImagePath = imagePath;
        }
    }
}
