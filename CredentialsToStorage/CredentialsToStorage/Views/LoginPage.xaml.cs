using Common.IoC;
using CredentialsToStorage.Contracts;
using CredentialsToStorage.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CredentialsToStorage.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private readonly LoginViewModel _viewModel;

        public LoginPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new LoginViewModel(
                InstanceResolverFor<IUserPreferencesProvider>.Instance);

            MessagingCenter.Subscribe<LoginViewModel>(this, LoginViewModel.Messages.LoggedOn, LoggedOn);
        }

        private void LoggedOn(LoginViewModel sender)
        {
            Application.Current.MainPage = new HomePage();
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.ClearErrorMessage();
        }
    }
}