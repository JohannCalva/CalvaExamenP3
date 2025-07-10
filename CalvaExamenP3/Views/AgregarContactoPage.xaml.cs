using CalvaExamenP3.ViewModels;

namespace CalvaExamenP3.Views
{
    public partial class AgregarContactoPage : ContentPage
    {
        private AgregarContactoViewModel viewModel;

        public AgregarContactoPage()
        {
            InitializeComponent();
            viewModel = BindingContext as AgregarContactoViewModel;
        }

        private async void BtnGuardarContacto_Clicked(object sender, EventArgs e)
        {
            if (viewModel != null)
            {
                await viewModel.GuardarAsync();
            }
        }
    }
}
