using ProductivityTimer.ViewModels;

namespace ProductivityTimer.Views
{
    public partial class MainPage : ContentPage
    {

        public MainPage(MainPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

    }
}
