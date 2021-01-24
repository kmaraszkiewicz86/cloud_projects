using PhotoGalery.Mobile.Services.Implementations;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace PhotoGalery.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            new HostingBuilder()
                .InitializeRequiredServices()
                .Build();
            
            MainPage = new NavigationPage(HostingBuilder.ServiceProvider.GetService<MainPage>());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}