using CredentialsToStorage.Contracts;
using CredentialsToStorage.Models;
using Foundation;
using Security;

namespace CredentialsToStorage.iOS.Providers
{
    public class UserPreferencesProvider : IUserPreferencesProvider
    {
        private readonly NSData _keychainSearchKey;

        public UserPreferencesProvider()
        {
            _keychainSearchKey = NSData.FromString("credentials");
        }

        public Credentials Credentials 
        { 
            get => GetCredentialsFromKeychain(); 
            set => SetCredentialsToKeychain(value); 
        }

        private Credentials GetCredentialsFromKeychain()
        {
            TryGetCredentialsFromKeychain(out var foundRecord);

            return new Credentials
            {
                Username = foundRecord?.Account,
                Password = foundRecord?.ValueData?.ToString()
            };
        }

        private bool TryGetCredentialsFromKeychain(out SecRecord record, SecRecord queryRecord = null)
        {
            queryRecord = new SecRecord(SecKind.GenericPassword) { Generic = _keychainSearchKey };
            record = SecKeyChain.QueryAsRecord(queryRecord, out var status);
            return status == SecStatusCode.Success;
        }

        private void SetCredentialsToKeychain(Credentials credentials)
        {
            // If there is already a record in the keychain, we must remove it prior to adding a new one, 
            // as the Update Api does not allow any field to be updated and this can cause an undesired behavior.
            SecKeyChain.Remove(new SecRecord(SecKind.GenericPassword) { Generic = _keychainSearchKey });

            SecKeyChain.Add(new SecRecord(SecKind.GenericPassword)
            {
                Service = "CredentialsToStorage.iOS",
                Label = "CredentialsToStorage.Credentials",
                Account = credentials.Username ?? string.Empty,
                ValueData = NSData.FromString(credentials.Password ?? string.Empty),
                Generic = _keychainSearchKey
            });
        }
    }
}