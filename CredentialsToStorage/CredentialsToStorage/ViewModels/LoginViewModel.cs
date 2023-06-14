using CredentialsToStorage.Contracts;
using CredentialsToStorage.Models;
using System;
using Xamarin.Forms;

namespace CredentialsToStorage.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IUserPreferencesProvider _userPreferencesProvider;
        private Credentials _credentials;
        private string errorMessage = string.Empty;

        public Credentials Credentials
        {
            get => _credentials;
            private set { SetProperty(ref _credentials, value); }
        }

        public Command LoginCommand { get; }

        public string ErrorMessage
        {
            get => errorMessage;
            private set { SetProperty(ref errorMessage, value); }
        }

        public LoginViewModel(IUserPreferencesProvider userPreferencesProvider)
        {
            Title = "Login";

            _userPreferencesProvider = userPreferencesProvider;
            var savedCredentials = _userPreferencesProvider.Credentials;
            Credentials = new Credentials
            {
                Username = savedCredentials.Username,
                Password = savedCredentials.Password,
            };

            LoginCommand = new Command(AttemptLogin);
        }

        private void AttemptLogin()
        {
            try
            {
                if (IsBusy) { return; }

                IsBusy = true;

                ClearErrorMessage();

                if (string.IsNullOrWhiteSpace(Credentials.Username) || string.IsNullOrWhiteSpace(Credentials.Password))
                {
                    ErrorMessage = "Credenciais inválidas.";
                    IsBusy = false;
                    return;
                }

                _userPreferencesProvider.Credentials = Credentials;
                MessagingCenter.Send(this, Messages.LoggedOn);
                IsBusy = false;
            }
            catch (Exception) { throw; }
        }

        public void ClearErrorMessage()
        {
            if (ErrorMessage == string.Empty) { return; }

            ErrorMessage = string.Empty;
        }

        public static class Messages
        {
            public const string LoggedOn = nameof(LoggedOn);
        }
    }
}