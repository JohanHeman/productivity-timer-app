using ProductivityTimer.ViewModels;

namespace ProductivityTimer.Views;

public partial class HistoryPage : ContentPage
{
	public HistoryPage(HistoryPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}