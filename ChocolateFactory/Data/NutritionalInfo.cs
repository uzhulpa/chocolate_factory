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
        private decimal _proteinsPer100g = 0m;
        private decimal _fatsPer100g = 0m;
        private decimal _carbohydratesPer100g = 0m;

        public decimal CaloriesPer100g
        {
            get
            {
                return decimal.Round(this.ProteinsPer100g * 4.1m + this.FatsPer100g * 9.3m + this.CarbohydratesPer100g * 4.1m, 2);
            }
        }

        public decimal ProteinsPer100g
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

        public decimal FatsPer100g
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

        public decimal CarbohydratesPer100g
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
        public NutritionalInfo(decimal proteinsPer100g, decimal fatsPer100g, decimal carbohydratesPer100g)
        {
            ProteinsPer100g = proteinsPer100g;
            FatsPer100g = fatsPer100g;
            CarbohydratesPer100g = carbohydratesPer100g;
        }
    }
}
