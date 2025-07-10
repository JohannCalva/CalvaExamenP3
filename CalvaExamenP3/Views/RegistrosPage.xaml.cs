using CalvaExamenP3.ViewModels;

namespace CalvaExamenP3.Views
{
    public partial class RegistrosPage : ContentPage
    {
        public RegistrosPage()
        {
            InitializeComponent();
            BindingContext = new RegistrosViewModel();
        }
    }
}
