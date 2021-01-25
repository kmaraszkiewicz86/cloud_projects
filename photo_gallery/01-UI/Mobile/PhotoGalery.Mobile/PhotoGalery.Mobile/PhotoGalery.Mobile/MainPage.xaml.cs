using PhotoGalery.Mobile.ViewModels;
using Xamarin.Forms;

namespace PhotoGalery.Mobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel mainViewModel)
        {
            InitializeComponent();

            BindingContext = mainViewModel;
        }
    }
}