using ChocolateFactory.Data;
using ChocolateFactory.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

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
    }
}
