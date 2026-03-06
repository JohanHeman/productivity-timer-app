using ProductivityTimer.ViewModels;

namespace ProductivityTimer.Views;

public partial class WorkPage : ContentPage
{
	public WorkPage(WorkPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}