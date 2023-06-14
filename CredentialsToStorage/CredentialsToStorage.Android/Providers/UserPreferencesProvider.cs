using CredentialsToStorage.Contracts;
using CredentialsToStorage.Models;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace CredentialsToStorage.Droid.Providers
{
    public class UserPreferencesProvider : IUserPreferencesProvider
    {
        private const string UsernameKey = "username";
        private const string PasswordKey = "password";

        public Credentials Credentials
        {
            get => GetCredentials();
            set => SetCredentials(value);
        }

        private Credentials GetCredentials()
        {
            using var task = Task.Run(async () =>
            {
                var username = await SecureStorage.GetAsync(UsernameKey);
                var password = await SecureStorage.GetAsync(PasswordKey);

                return new Credentials
                {
                    Username = username,
                    Password = password
                };
            });
            task.Wait();
            return task.Result;
        }

        private Task SetCredentials(Credentials credentials)
        {
            using var task = Task.Run(async () =>
            {
                await SecureStorage.SetAsync(UsernameKey, credentials.Username);
                await SecureStorage.SetAsync(PasswordKey, credentials.Password);
            });
            task.Wait();
            return Task.CompletedTask;
        }
    }
}