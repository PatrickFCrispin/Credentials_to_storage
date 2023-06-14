using CredentialsToStorage.Views;
using Xamarin.Forms;

namespace CredentialsToStorage
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
        }

        public void Logout()
        {
            StartOver();
        }

        private void StartOver()
        {
            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}