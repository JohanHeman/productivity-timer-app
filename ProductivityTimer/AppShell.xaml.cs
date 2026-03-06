namespace ProductivityTimer
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            // maps the routes to the pages
            Routing.RegisterRoute(nameof(Views.WorkPage), typeof(Views.WorkPage));
            Routing.RegisterRoute(nameof(Views.HistoryPage), typeof(Views.HistoryPage));
        }
    }
}
