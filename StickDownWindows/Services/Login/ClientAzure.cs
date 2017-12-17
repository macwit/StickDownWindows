using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Microsoft.WindowsAzure.MobileServices;
using StickDownWindows.Services.Data;
using StickDownWindows.Services.Providers;

namespace StickDownWindows.Services.Login
{
    public class ClientAzure
    {
        protected readonly IMobileServiceClient MobileServiceClient;
        private Services.Providers.AccountStoreProvider LocalAccountStoreProvider;
        protected readonly string MobileAppUrl;
        protected SignInProviderType ProviderType;

        public ClientAzure()
        {
            MobileAppUrl = Services.AppInfo.UriApp;
            MobileServiceClient = new MobileServiceClient(MobileAppUrl);          
            LocalAccountStoreProvider = new AccountStoreProvider();
        }


        public async Task<bool> Login(MobileServiceAuthenticationProvider provider, JObject token)
        {
            await MobileServiceClient.LoginAsync(provider, token);
            if (MobileServiceClient.CurrentUser == null) return false;
            
            if (provider == MobileServiceAuthenticationProvider.Google)
                ProviderType = SignInProviderType.Google;
            if (provider == MobileServiceAuthenticationProvider.Facebook)
                ProviderType = SignInProviderType.Facebook;

            LocalAccountStoreProvider.StoreUserAccountInSecureStore(MobileServiceClient.CurrentUser, ProviderType);

            return true;
        }

    }
}