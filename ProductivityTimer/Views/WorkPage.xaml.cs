using ProductivityTimer.ViewModels;

namespace ProductivityTimer.Views;

public partial class WorkPage : ContentPage
{
	public WorkPage(WorkPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	protected override async void OnDisappearing()
	{
		base.OnDisappearing();
		if (BindingContext is WorkPageViewModel viewModel)
		{
			await viewModel.StopWhenLeavingPageAsync();
		}
	}
}