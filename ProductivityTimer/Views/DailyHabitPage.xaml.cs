using ProductivityTimer.ViewModels;

namespace ProductivityTimer.Views;

public partial class DailyHabitPage : ContentPage
{

	public DailyHabitPage(DailyHabitPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
	protected override async void OnAppearing()
	{
		base.OnAppearing();

		if (BindingContext is DailyHabitPageViewModel viewModel)
		{
			await viewModel.InitializeAsync(); // Loads the list 
		}
	}
}