using ChocolateFactory.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocolateFactory.Models
{
    public class GiftItem
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }

        public int Weight { get; set; }

        public NutritionalInfo NutritionalInfo { get; set; } = new NutritionalInfo();

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public GiftItem()
        {
            Name = string.Empty;
            ImagePath = string.Empty;
        }

        public GiftItem(Item item)
        {
            ItemId = item.Id;
            Name = item.Name;
            ImagePath = item.ImagePath;
            Weight = item.Weight;
            Price = item.Price;
            NutritionalInfo = item.NutritionalInfo;
            Quantity = 1;
        }

        public GiftItem(Item item, int quantity) : this(item)
        {
            Quantity = quantity;
        }

        public decimal Amount => decimal.Round(Price * Quantity, 2);

        public int TotalWeight => Weight * Quantity;

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
