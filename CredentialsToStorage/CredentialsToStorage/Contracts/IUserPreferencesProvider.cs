using CredentialsToStorage.Models;

namespace CredentialsToStorage.Contracts
{
    public interface IUserPreferencesProvider
    {
        Credentials Credentials { get; set; }
    }
}