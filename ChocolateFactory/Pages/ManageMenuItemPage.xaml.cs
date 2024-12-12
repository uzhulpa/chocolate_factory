using ChocolateFactory.ViewModels;

namespace ChocolateFactory.Pages;

public partial class ManageMenuItemPage : ContentPage
{
    private readonly ManageMenuItemsViewModel _manageMenuItemsViewModel;

    public ManageMenuItemPage(ManageMenuItemsViewModel manageMenuItemsViewModel)
	{
		InitializeComponent();
        _manageMenuItemsViewModel = manageMenuItemsViewModel;
        BindingContext = _manageMenuItemsViewModel;
        Initialize();
    }

    private void Initialize()
    {
        _manageMenuItemsViewModel.Initialize();
    }

    private void SaveItemFormControl_OnCancel()
    {
        _manageMenuItemsViewModel.CancelCommand.Execute(null);
    }
}