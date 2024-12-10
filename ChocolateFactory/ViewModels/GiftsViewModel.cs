using ChocolateFactory.Data;
using ChocolateFactory.Models;
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
        public void PlaceGift(List<CurrentGiftItemModel> giftItemsModels, string name, string imagePath)
        {
            var giftItems = giftItemsModels.Select(x => new GiftItem
            {
                ItemId = x.ItemId,
                Name = name,
                ImagePath = imagePath,
                Price = x.Price,
                Weight = x.Weight,
                NutritionalInfo = x.NutritionalInfo
                
            });
            var giftModel = new GiftModel
            {
                Name = name,
                ImagePath = imagePath,
            };
        }
    }
}
