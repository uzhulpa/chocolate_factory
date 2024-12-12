using ChocolateFactory.Models;
using CommunityToolkit.Mvvm.Input;

namespace ChocolateFactory.Controls;

public partial class SaveItemFormControl : ContentView
{
	public SaveItemFormControl()
	{
		InitializeComponent();
	}

	public static readonly BindableProperty ItemProperty =
		BindableProperty.Create(nameof(Item), typeof(ItemModel), typeof(SaveItemFormControl), new ItemModel(), propertyChanged: OnItemChanged);
	public ItemModel Item
	{
		get => (ItemModel)GetValue(ItemProperty);
		set => SetValue(ItemProperty, value);
	}

	private static void OnItemChanged(BindableObject bindable, object oldValue, object newValue)
	{
		if (newValue is ItemModel itemModel)
		{
			if (bindable is SaveItemFormControl thisControl)
			{
				if (itemModel.Id > 0)
				{
					thisControl.itemImagePath.Source = itemModel.ImagePath;
					thisControl.itemImagePath.HeightRequest = thisControl.itemImagePath.WidthRequest = 150;
				}
				else
				{
                    thisControl.itemImagePath.Source = "add_image.png";
                    thisControl.itemImagePath.HeightRequest = thisControl.itemImagePath.WidthRequest = 35;
                }
			}
		}
	}

	public event Action? OnCancel;

	[RelayCommand]
	private void Cancel() => OnCancel?.Invoke();
}