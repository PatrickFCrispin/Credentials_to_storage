using CredentialsToStorage.ViewModels;
using System;
using Xamarin.Forms;

namespace CredentialsToStorage.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();

            BindingContext = new HomeViewModel();
        }

        private void Logout_Clicked(object sender, EventArgs e)
        {
            (Application.Current as App).Logout();
        }
    }
}