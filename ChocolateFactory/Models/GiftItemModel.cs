using ChocolateFactory.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocolateFactory.Models
{
    public partial class GiftItemModel : ObservableObject
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }

        public int Weight { get; set; }

        public NutritionalInfo NutritionalInfo { get; set; } = new NutritionalInfo();

        public decimal Price { get; set; }

        [ObservableProperty, NotifyPropertyChangedFor(nameof(Amount), nameof(TotalWeight), nameof(Calories), nameof(Proteins), nameof(Fats), nameof(Carbohydrates))]
        private int _quantity;

        public GiftItemModel()
        {
            Name = string.Empty;
            ImagePath = string.Empty;
        }

        public GiftItemModel(Item item)
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

        public double Calories
        {
            get
            {
                return double.Round(NutritionalInfo.CaloriesPer100g * (TotalWeight / 100.0), 2);
            }
        }
        public double Proteins
        {
            get
            {
                return double.Round(NutritionalInfo.ProteinsPer100g * (TotalWeight / 100.0), 2);
            }
        }
        public double Fats
        {
            get
            {
                return double.Round(NutritionalInfo.FatsPer100g * (TotalWeight / 100.0), 2);
            }
        }
        public double Carbohydrates
        {
            get
            {
                return double.Round(NutritionalInfo.CarbohydratesPer100g * (TotalWeight / 100.0), 2);
            }
        }
    }
}
