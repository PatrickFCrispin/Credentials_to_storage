using Common.IoC;
using CredentialsToStorage.Contracts;
using CredentialsToStorage.Droid.Providers;

namespace CredentialsToStorage.Droid.IoC
{
    internal static class DependencyResolver
    {
        internal static void BindEverything()
        {
            InstanceResolverFor<IUserPreferencesProvider>.BindSingletonOf<UserPreferencesProvider>();
        }
    }
}