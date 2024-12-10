using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ChocolateFactory.Data
{
    [Serializable]
    public class Item
    {
        public static int Count=0;

        private int _id;
        private String _name = "item name";
        private int _weight = 100;
        private decimal _price = 10;
        private String _imagePath = "candy_bar.png";
        private NutritionalInfo _nutritionalInfo = new NutritionalInfo();

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
        public int Weight
        {
            get => _weight;
            set
            {
                _weight = value;
            }
        }
        public decimal Price
        {
            get => _price;
            set
            {
                _price = value;
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
            get => _nutritionalInfo;
            set
            {
                _nutritionalInfo = value;
            }
        }

        public double Calories
        {
            get
            {
                return double.Round(NutritionalInfo.CaloriesPer100g * (Weight / 100.0), 2);
            }
        }
        public double Proteins
        {
            get
            {
                return double.Round(NutritionalInfo.ProteinsPer100g * (Weight / 100.0), 2);
            }
        }
        public double Fats
        {
            get
            {
                return double.Round(NutritionalInfo.FatsPer100g * (Weight / 100.0), 2);
            }
        }
        public double Carbohydrates
        {
            get
            {
                return double.Round(NutritionalInfo.CarbohydratesPer100g * (Weight / 100.0), 2);
            }
        }

        public Item()
        {
            Id = ++Count;
        }
        public Item(string name, int weight, decimal price, string imagePath, NutritionalInfo nutritionalInfo) : this()
        {
            Name = name;
            Weight = weight;
            Price = price;
            ImagePath = imagePath;
            NutritionalInfo = nutritionalInfo;
        }
    }
}
