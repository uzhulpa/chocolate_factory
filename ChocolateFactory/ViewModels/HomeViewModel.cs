using ChocolateFactory.Data;
using ChocolateFactory.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace ChocolateFactory.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly XmlDatabaseManager _xmlDatabaseManager;
        private bool _isInitialized=false;

        [ObservableProperty]
        private bool _isLoading;

        [ObservableProperty]
        private List<Item> _items = new List<Item>();

        public ObservableCollection<GiftItemModel> GiftItems { get; set; } = new();

        [ObservableProperty]
        private string _currentGiftName = string.Empty;

        [ObservableProperty]
        private string _currentGiftImagePath = string.Empty;

        public HomeViewModel(XmlDatabaseManager xmlDatabaseManager)
        {
            this._xmlDatabaseManager = xmlDatabaseManager;
        }

        public void Initialize()
        {
            if (_isInitialized) return; // Уже инициализирован
            IsLoading = true;
            Items = _xmlDatabaseManager.LoadItems();
            _isInitialized = true;
            IsLoading = false;
        }

        [RelayCommand]
        private void AddToGift(Item item)
        {
            var GiftItem = GiftItems.FirstOrDefault(x => x.ItemId == item.Id);
            if (GiftItem == null) // Изделия ещё нет в наборе, добавить в набор
            {
                GiftItem = new GiftItemModel
                {
                    ItemId = item.Id,
                    Name = item.Name,
                    ImagePath = item.ImagePath,
                    Weight = item.Weight,
                    NutritionalInfo = item.NutritionalInfo,
                    Price = item.Price,
                    Quantity = 1
                };
                GiftItems.Add(GiftItem);
            }
            else // Изделие уже есть в наборе, увеличить количество на 1
            {
                GiftItem.Quantity++;
            }
        }

        [RelayCommand]
        private void IncreaseQuantity(GiftItemModel giftItemModel) => giftItemModel.Quantity++;

        [RelayCommand]
        private void DecreaseQuantity(GiftItemModel giftItemModel)
        {
            giftItemModel.Quantity--;
            if (giftItemModel.Quantity == 0)
            {
                RemoveItemFromCurrentGift(giftItemModel);
            }
        }

        [RelayCommand]
        private void RemoveItemFromCurrentGift(GiftItemModel giftItemModel) => GiftItems.Remove(giftItemModel);
    }
}
