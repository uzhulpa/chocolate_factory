using ChocolateFactory.Data;
using ChocolateFactory.Messages;
using ChocolateFactory.Models;
using ChocolateFactory.Repository;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace ChocolateFactory.ViewModels
{
    public partial class ManageMenuItemsViewModel : ObservableObject
    {
        private readonly XmlDatabaseManager _xmlDatabaseManager;

        public ManageMenuItemsViewModel(XmlDatabaseManager xmlDatabaseManager)
        {
            _xmlDatabaseManager = xmlDatabaseManager;
        }

        [ObservableProperty]
        private List<Item> _items = new List<Item>();

        private bool _isInitialized = false;

        [ObservableProperty]
        private bool _isLoading;

        [ObservableProperty]
        private ItemModel _item = new();

        public void Initialize()
        {
            if (_isInitialized) return;
            IsLoading = true;
            Items = _xmlDatabaseManager.LoadItems();
            _isInitialized = true;
            IsLoading = false;
        }

        [RelayCommand]
        private async Task EditItemAsync(Item item)
        {
            //await Shell.Current.DisplayAlert("Редактирование", "Изменить этот элемент?", "Да");
            var itemModel = new ItemModel
            {
                Name = item.Name,
                ImagePath = item.ImagePath,
                Price = item.Price,
                Weight = item.Weight,
                NutritionalInfo = item.NutritionalInfo,
                Id = item.Id
            };
            Item = itemModel;
        }

        [RelayCommand]
        private void Cancel()
        {
            Item = new();
        }

        [RelayCommand]
        private async Task SaveItemAsync(ItemModel itemModel)
        {

            if (await Shell.Current.DisplayAlert("Сохранить изменения?", "Вы действительно хотите сохранить все внесённые изменения?", "Да", "Нет"))
            {
                IsLoading = true;

                _xmlDatabaseManager.SaveItem(itemModel);

                await Toast.Make("Элемент успешно изменён").Show();

                HandleItemChanged(itemModel);

                WeakReferenceMessenger.Default.Send(new ItemChangedMessage(itemModel));

                Cancel();

                IsLoading = false;
            }
        }

        private void HandleItemChanged(ItemModel itemModel)
        {
            Items = _xmlDatabaseManager.LoadItems();
        }
    }
}
