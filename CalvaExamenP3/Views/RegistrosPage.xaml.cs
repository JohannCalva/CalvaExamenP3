using CalvaExamenP3.ViewModels;

namespace CalvaExamenP3.Views
{
    public partial class RegistrosPage : ContentPage
    {
        private RegistrosViewModel viewModel;

        public RegistrosPage()
        {
            InitializeComponent();
            viewModel = new RegistrosViewModel();
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel?.CargarContactos();
        }
    }
}