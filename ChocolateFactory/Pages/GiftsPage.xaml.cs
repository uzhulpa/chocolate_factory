using ChocolateFactory.ViewModels;

namespace ChocolateFactory.Pages;

public partial class GiftsPage : ContentPage
{
	private readonly GiftsViewModel _giftsViewModel;

	public GiftsPage(GiftsViewModel giftsViewModel)
	{
		InitializeComponent();
		_giftsViewModel = giftsViewModel;
		BindingContext = _giftsViewModel;
		InitializeViewModel();
	}

	private void InitializeViewModel() => _giftsViewModel.Initialize();
}