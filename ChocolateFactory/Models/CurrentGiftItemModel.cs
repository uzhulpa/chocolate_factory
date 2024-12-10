using ChocolateFactory.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocolateFactory.Models
{
    public partial class CurrentGiftItemModel : ObservableObject
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }

        public int Weight { get; set; }

        public NutritionalInfo NutritionalInfo { get; set; } = new NutritionalInfo();

        public decimal Price { get; set; }

        [ObservableProperty, NotifyPropertyChangedFor(nameof(Amount), nameof(TotalWeight), nameof(Calories), nameof(Proteins), nameof(Fats), nameof(Carbohydrates))]
        private int _quantity;

        public CurrentGiftItemModel()
        {
            Name = string.Empty;
            ImagePath = string.Empty;
        }

        public CurrentGiftItemModel(Item item)
        {
            ItemId = item.Id;
            Name = item.Name;
            ImagePath = item.ImagePath;
            Weight = item.Weight;
            Price = item.Price;
            NutritionalInfo = item.NutritionalInfo;
            _quantity = 1;
        }

        public decimal Amount => decimal.Round(Price * _quantity, 2);

        public int TotalWeight => Weight * _quantity;
        public decimal Calories
        {
            get
            {
                return decimal.Round(NutritionalInfo.CaloriesPer100g * (TotalWeight / 100.0m), 2);
            }
        }
        public decimal Proteins
        {
            get
            {
                return decimal.Round(NutritionalInfo.ProteinsPer100g * (TotalWeight / 100.0m), 2);
            }
        }
        public decimal Fats
        {
            get
            {
                return decimal.Round(NutritionalInfo.FatsPer100g * (TotalWeight / 100.0m), 2);
            }
        }
        public decimal Carbohydrates
        {
            get
            {
                return decimal.Round(NutritionalInfo.CarbohydratesPer100g * (TotalWeight / 100.0m), 2);
            }
        }
    }
}
