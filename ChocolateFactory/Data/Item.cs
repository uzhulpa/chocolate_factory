using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ChocolateFactory.Data
{
    [XmlInclude(typeof(Candy))]
    [Serializable]
    public abstract class Item :IComparable<Item>
    {
        public static int Count=0;

        private int _id;
        private string _name;
        private int _weight;
        private decimal _price;
        private string _imagePath;
        private NutritionalInfo _nutritionalInfo = new NutritionalInfo();

        public Item()
        {
            Id = ++Count;
            _name = "item name";
            _weight = 100;
            _price = 10;
            _imagePath = "candy_bar_png";
        }
        public Item(string name, int weight, decimal price, string imagePath, NutritionalInfo nutritionalInfo) : this()
        {
            Name = name;
            Weight = weight;
            Price = price;
            ImagePath = imagePath;
            NutritionalInfo = nutritionalInfo;
        }

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

        public decimal Calories
        {
            get
            {
                return decimal.Round(NutritionalInfo.CaloriesPer100g * (Weight / 100.0m), 2);
            }
        }
        public decimal Proteins
        {
            get
            {
                return decimal.Round(NutritionalInfo.ProteinsPer100g * (Weight / 100.0m), 2);
            }
        }
        public decimal Fats
        {
            get
            {
                return decimal.Round(NutritionalInfo.FatsPer100g * (Weight / 100.0m), 2);
            }
        }
        public decimal Carbohydrates
        {
            get
            {
                return decimal.Round(NutritionalInfo.CarbohydratesPer100g * (Weight / 100.0m), 2);
            }
        }

        public int CompareTo(Item? obj)
        {
            return (this.Weight-obj.Weight);
        }
    }
}
