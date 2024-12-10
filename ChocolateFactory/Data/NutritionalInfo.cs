using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocolateFactory.Data
{
    [Serializable]
    public class NutritionalInfo
    {
        private double _proteinsPer100g = 0;
        private double _fatsPer100g = 0;
        private double _carbohydratesPer100g = 0;

        public double CaloriesPer100g
        {
            get
            {
                return double.Round(this.ProteinsPer100g * 4.1 + this.FatsPer100g * 9.3 + this.CarbohydratesPer100g * 4.1,2);
            }
        }

        public double ProteinsPer100g
        {
            get => _proteinsPer100g;
            set
            {
                if (value >= 0)
                {
                    this._proteinsPer100g = value;
                }
            }
        }

        public double FatsPer100g
        {
            get => _fatsPer100g;
            set
            {
                if (value >= 0)
                {
                    this._fatsPer100g = value;
                }
            }
        }

        public double CarbohydratesPer100g
        {
            get => _carbohydratesPer100g;
            set
            {
                if (value >= 0)
                {
                    this._carbohydratesPer100g = value;
                }
            }
        }

        public NutritionalInfo() { }
        public NutritionalInfo(double proteinsPer100g, double fatsPer100g, double carbohydratesPer100g)
        {
            ProteinsPer100g = proteinsPer100g;
            FatsPer100g = fatsPer100g;
            CarbohydratesPer100g = carbohydratesPer100g;
        }
    }
}
