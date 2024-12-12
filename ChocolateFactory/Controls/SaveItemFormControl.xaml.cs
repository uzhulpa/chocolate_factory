using ChocolateFactory.Models;
using CommunityToolkit.Mvvm.Input;

namespace ChocolateFactory.Controls;

public partial class SaveItemFormControl : ContentView
{

	private const string DefaultImage = "add_image.png";

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
					thisControl.SetImage(false, itemModel.ImagePath, thisControl);
					thisControl.ExistingImagePath = itemModel.ImagePath;
				}
				else
				{
                    thisControl.SetImage(true, null, thisControl);
                }
			}
		}
	}

	public string? ExistingImagePath { get; set; }

	public event Action? OnCancel;

	[RelayCommand]
	private void Cancel() => OnCancel?.Invoke();

	private async void PickImageButton_Clicked(object sender, EventArgs e)
	{
		var fileResult = await MediaPicker.PickPhotoAsync();
		if (fileResult != null)
		{
            var imageStream = await fileResult.OpenReadAsync();

            var localPath = Path.Combine(FileSystem.AppDataDirectory, fileResult.FileName);

			using var fs = new FileStream(localPath, FileMode.Create, FileAccess.Write);
			
			await imageStream.CopyToAsync(fs);

			SetImage(isDefault: false, localPath);

			Item.ImagePath = localPath;
        }
		else
		{
			if(ExistingImagePath != null)
			{
				SetImage(isDefault: false, ExistingImagePath);
			}
			else
			{
				SetImage(isDefault: false);
			}
        }
	}

	public void SetImage(bool isDefault, string? itemSource=null, SaveItemFormControl? control=null)
	{
		int size = 100;

		if (isDefault)
		{
			itemSource = "add_image.png";
			size = 35;

        }
		control = control ?? this;

        control.itemImagePath.Source = itemSource;
        control.itemImagePath.HeightRequest = control.itemImagePath.WidthRequest = size;
    }

	public event Action<ItemModel>? OnSaveItem;

	[RelayCommand]
	private async Task SaveItemAsync()
	{
		if (string.IsNullOrWhiteSpace(Item.Name))
		{
			await ErrorAlertAsync("Отсутствует информация о названии продукта.");
			return;
		}

		if (Item.ImagePath == DefaultImage)
		{
			await ErrorAlertAsync("Отсутствует изображение продукта.");
			return;
		}

        if (Item.Price <= 0)
        {
            await ErrorAlertAsync("Отсутствует информация о стоимости продукта.");
            return;
        }

        if (Item.Weight <= 0)
        {
            await ErrorAlertAsync("Отсутствует информация о весе продукта.");
            return;
        }

		if (Item.NutritionalInfo.CaloriesPer100g == 0)
		{
			await ErrorAlertAsync("Отсутствует информация о пищевой ценности продукта.");
			return;
		}

        OnSaveItem?.Invoke(Item);

		static async Task ErrorAlertAsync(string message) => await Shell.Current.DisplayAlert("Ошибка", message, "ОК");
    }
}