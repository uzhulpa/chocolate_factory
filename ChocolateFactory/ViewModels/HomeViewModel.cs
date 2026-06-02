using ChocolateFactory.Data;
using ChocolateFactory.Messages;
using ChocolateFactory.Models;
using ChocolateFactory.Repository;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;

namespace ChocolateFactory.ViewModels
{
    public partial class HomeViewModel : ObservableObject, IRecipient<ItemChangedMessage>
    {
        private readonly XmlDatabaseManager _xmlDatabaseManager;
        private readonly GiftsViewModel _giftsViewModel;

        private bool _isInitialized = false;

        [ObservableProperty]
        private bool _isLoading;

        [ObservableProperty]
        private List<Item> _items = new List<Item>();

        public ObservableCollection<CurrentGiftItemModel> CurrentGiftItems { get; set; } = new();

        [ObservableProperty]
        private decimal _currentGift_TotalPrice;

        [ObservableProperty]
        private int _currentGift_TotalWeight;

        [ObservableProperty]
        private NutritionalInfo _currentGift_NutritionalInfo = new();

        public HomeViewModel(XmlDatabaseManager xmlDatabaseManager, GiftsViewModel giftsViewModel)
        {
            this._xmlDatabaseManager = xmlDatabaseManager;
            this._giftsViewModel = giftsViewModel;  
            CurrentGiftItems.CollectionChanged += CurrentGiftItems_CollectionChanged;

            WeakReferenceMessenger.Default.Register<ItemChangedMessage>(this);
        }

        private void CurrentGiftItems_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            RecalculateCurrentGiftTotalPrice();
            RecalculateCurrentGiftTotalWeight();
            RecalculateCurrentGiftNutritionalInfo();
        }

        public void Initialize()
        {
            if (_isInitialized) return;
            IsLoading = true;
            Items = _xmlDatabaseManager.LoadItems();
            Items.Sort();
            _isInitialized = true;
            IsLoading = false;
        }

        [RelayCommand]
        private void AddToGift(Item item)
        {
            var GiftItem = CurrentGiftItems.FirstOrDefault(x => x.ItemId == item.Id);
            if (GiftItem == null)
            {
                GiftItem = new CurrentGiftItemModel
                {
                    ItemId = item.Id,
                    Name = item.Name,
                    ImagePath = item.ImagePath,
                    Weight = item.Weight,
                    NutritionalInfo = item.NutritionalInfo,
                    Price = item.Price,
                    Quantity = 1
                };
                CurrentGiftItems.Add(GiftItem);
            }
            else
            {
                GiftItem.Quantity++;
            }
            RecalculateCurrentGiftTotalPrice();
            RecalculateCurrentGiftTotalWeight();
            RecalculateCurrentGiftNutritionalInfo();
        }

        [RelayCommand]
        private void IncreaseQuantity(CurrentGiftItemModel giftItemModel)
        {
            giftItemModel.Quantity++;
            RecalculateCurrentGiftTotalPrice();
            RecalculateCurrentGiftTotalWeight();
            RecalculateCurrentGiftNutritionalInfo();
        }

        [RelayCommand]
        private void DecreaseQuantity(CurrentGiftItemModel giftItemModel)
        {
            giftItemModel.Quantity--;
            if (giftItemModel.Quantity == 0)
            {
                RemoveItemFromCurrentGift(giftItemModel);
            }
            RecalculateCurrentGiftTotalPrice();
            RecalculateCurrentGiftTotalWeight();
            RecalculateCurrentGiftNutritionalInfo();
        }

        [RelayCommand]
        private void RemoveItemFromCurrentGift(CurrentGiftItemModel giftItemModel)
        {
            CurrentGiftItems.Remove(giftItemModel);
            RecalculateCurrentGiftTotalPrice();
            RecalculateCurrentGiftTotalWeight();
            RecalculateCurrentGiftNutritionalInfo();
        }

        [RelayCommand]
        private async Task ClearCurrentGiftAsync()
        {
            if (await Shell.Current.DisplayAlert("Удалить собранный набор?", "Вы действительно хотите удалить все элементы набора?", "Да", "Нет"))
            {
                CurrentGiftItems.Clear();
            }
        }

        private void RecalculateCurrentGiftTotalPrice()
        {
            CurrentGift_TotalPrice = CurrentGiftItems.Sum(x => x.Amount);
        }

        private void RecalculateCurrentGiftTotalWeight()
        {
            CurrentGift_TotalWeight = CurrentGiftItems.Sum(x => x.TotalWeight);
        }

        private void RecalculateCurrentGiftNutritionalInfo()
        {

            if (CurrentGift_TotalWeight == 0)
            {
                CurrentGift_NutritionalInfo = new NutritionalInfo(0.0m, 0.0m, 0.0m);
                return;
            }

            decimal totalProteins = CurrentGiftItems.Sum(x => x.Proteins);
            decimal totalFats = CurrentGiftItems.Sum(x => x.Fats);
            decimal totalCarbohydrates = CurrentGiftItems.Sum(x => x.Carbohydrates);

            CurrentGift_NutritionalInfo = new NutritionalInfo(decimal.Round(totalProteins / (CurrentGift_TotalWeight / 100.0m), 2),
                decimal.Round(totalFats / (CurrentGift_TotalWeight / 100.0m), 2),
                decimal.Round(totalCarbohydrates / (CurrentGift_TotalWeight / 100.0m), 2));
        }

        [RelayCommand]
        private async Task PlaceGiftAsync()
        {
            IsLoading = true;
            await _giftsViewModel.PlaceGiftAsync(CurrentGiftItems.ToList());
            IsLoading = false;
            CurrentGiftItems.Clear();
        }

        public void Receive(ItemChangedMessage message)
        {
            Items = _xmlDatabaseManager.LoadItems();

            var itemModel = message.Value;

            var currentGiftItem = CurrentGiftItems.FirstOrDefault(x => x.ItemId == itemModel.Id);

            if (currentGiftItem == null) return;

            currentGiftItem.Price = itemModel.Price;
            currentGiftItem.Name = itemModel.Name;
            currentGiftItem.ImagePath = itemModel.ImagePath;
            currentGiftItem.Weight = itemModel.Weight;
            currentGiftItem.NutritionalInfo = itemModel.NutritionalInfo;
            currentGiftItem.Quantity = currentGiftItem.Quantity;

            // триггерит отслеживаемую коллекцию
            var currentGiftItemIndex = CurrentGiftItems.IndexOf(currentGiftItem);
            CurrentGiftItems[currentGiftItemIndex] = currentGiftItem;

        }
    }
}
