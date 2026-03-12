using ProductivityTimer.ViewModels;

namespace ProductivityTimer.Views;

public partial class DailyHabitPage : ContentPage
{

	public DailyHabitPage(DailyHabitPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}