using ChocolateFactory.Data;
using ChocolateFactory.Messages;
using ChocolateFactory.Models;
using ChocolateFactory.Repository;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocolateFactory.ViewModels
{
    public partial class GiftsViewModel : ObservableObject
    {
        private readonly XmlDatabaseManager _xmlDatabaseManager;

        private bool _isInitialized = false;

        [ObservableProperty]
        private bool _isLoading;

        public ObservableCollection<Gift> Gifts { get; set; } = new();

        [ObservableProperty]
        private Gift _selectedGift = null;

        [ObservableProperty]
        private List<GiftItem> _selectedGiftItems = new List<GiftItem>();

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
                Name = "Новогодний подарок",
                ImagePath = "hrutka.jpg",
                GiftItems = giftItems
            };

            await Toast.Make("Набор успешно сохранён").Show();

            _xmlDatabaseManager.PlaceGift(giftModel);
        }

        public void Initialize()
        {
            if (_isInitialized) return;
            IsLoading = true;
            var gifts = _xmlDatabaseManager.LoadGifts();
            foreach (var gift in gifts)
            {
                Gifts.Add(gift);
            }

            WeakReferenceMessenger.Default.Register<GiftCreatedMessage>(this, (r, m) =>
            {
                Gifts.Add(m.NewGift);
            });

            _isInitialized = true;
            IsLoading = false;
        }

        [RelayCommand]
        private void SelectGift(Gift? gift)
        {
            if (gift == null)
            {
                SelectedGiftItems = [];
                return;
            }
            IsLoading = true;
            SelectedGift = gift;
            SelectedGiftItems = _xmlDatabaseManager.GetGiftItems(gift.Id);
            IsLoading = false;
        }
    }
}
