using ChocolateFactory.Data;
using ChocolateFactory.Models;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocolateFactory.ViewModels
{
    public partial class GiftsViewModel : ObservableObject
    {
        private readonly XmlDatabaseManager _xmlDatabaseManager;
        public GiftsViewModel(XmlDatabaseManager xmlDatabaseManager)
        {
            this._xmlDatabaseManager = xmlDatabaseManager;
        }

        public async Task PlaceGiftAsync(List<CurrentGiftItemModel> currentGiftItems)
        {
            var giftItems = currentGiftItems.Select(x => new GiftItem
            {
                ItemId = x.ItemId,
                Name = x.Name,
                ImagePath = x.ImagePath,
                Weight = x.Weight,
                NutritionalInfo = x.NutritionalInfo,
                Price = x.Price,
                Quantity = x.Quantity
            }).ToList();
            var giftModel = new GiftModel
            {
                GiftItems = giftItems
            };

            await Toast.Make("Набор успешно сохранён").Show();
        }
    }
}
