using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocolateFactory.Data
{
    [Serializable]
    public class Candy : Item
    {
        public Candy() :base() { }

        public Candy(string name, int weight, decimal price, string imagePath, NutritionalInfo nutritionalInfo)
            : base(name, weight, price, imagePath, nutritionalInfo) { }
    }
}
