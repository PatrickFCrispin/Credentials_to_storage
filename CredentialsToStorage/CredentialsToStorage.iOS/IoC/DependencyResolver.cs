using Common.IoC;
using CredentialsToStorage.Contracts;
using CredentialsToStorage.iOS.Providers;

namespace CredentialsToStorage.iOS.IoC
{
    internal static class DependencyResolver
    {
        internal static void BindEverything()
        {
            InstanceResolverFor<IUserPreferencesProvider>.BindSingletonOf<UserPreferencesProvider>();
        }
    }
}