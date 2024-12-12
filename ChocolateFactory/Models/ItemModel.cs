using ChocolateFactory.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocolateFactory.Models
{
    public partial class ItemModel : ObservableObject
    {
        public int Id { get; set; }

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private string _imagePath;

        [ObservableProperty]
        private int _weight;

        [ObservableProperty]
        private decimal _price;

        [ObservableProperty]
        private NutritionalInfo _nutritionalInfo = new NutritionalInfo();

    }
}
