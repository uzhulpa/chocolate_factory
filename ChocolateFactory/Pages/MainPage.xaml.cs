using ChocolateFactory.ViewModels;

namespace ChocolateFactory.Pages;

public partial class MainPage : ContentPage
{
    private readonly HomeViewModel _homeViewModel;

    public MainPage(HomeViewModel homeViewModel)
    {
        InitializeComponent();
        _homeViewModel = homeViewModel;
        BindingContext = _homeViewModel;
        Initialize();
    }

    private void Initialize()
    {
        _homeViewModel.Initialize();
    }
}
