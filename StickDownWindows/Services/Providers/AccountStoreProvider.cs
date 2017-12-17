using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using StickDownWindows.Services.Data;

namespace StickDownWindows.Services.Providers
{
    public class AccountStoreProvider
    {
        private string PathAccountInfoFile;

        public AccountStoreProvider()
        {
            PathAccountInfoFile = Path.Combine(AppInfo.AppDataFolderPath, AppInfo.AccountInfoFileName);
            if (!File.Exists(PathAccountInfoFile))
                File.Create(PathAccountInfoFile);
        }


        protected readonly Encoding Encoding = new UTF8Encoding(true, true);

        public (MobileServiceUser user, SignInProviderType providerType)? RetrieveUserAccountFromSecureStore()
        {
            byte[] bytes;

            using (var stream = File.Open(PathAccountInfoFile, FileMode.Open, FileAccess.Read))
            {
                try
                {
                    bytes = ReadFully(stream);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            if (bytes.Length == 0)
                return null;

            var plaintext = ProtectedData.Unprotect(bytes, null, DataProtectionScope.CurrentUser);
            var json = Encoding.GetString(plaintext);
            var account = JsonConvert.DeserializeObject<JsonAccount>(json);
            return (new MobileServiceUser(account.UserId)
            {
                MobileServiceAuthenticationToken = account.MobileServiceAuthenticationToken
            }, account.ProviderType);
        }

        protected static byte[] ReadFully(Stream input)
        {
            var buffer = new byte[128 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public void StoreUserAccountInSecureStore(MobileServiceUser user, SignInProviderType providerType)
        {
            // Data to protect. Convert a string to a byte[] using Encoding.UTF8.GetBytes().
            var json = JsonConvert.SerializeObject(new JsonAccount()
            {
                UserId = user.UserId,
                MobileServiceAuthenticationToken = user.MobileServiceAuthenticationToken,
                ProviderType = providerType
            });
            var plaintext = Encoding.GetBytes(json);

            var ciphertext = ProtectedData.Protect(plaintext, null, DataProtectionScope.CurrentUser);

            try
            {
                File.Delete(PathAccountInfoFile);
                var stream = File.Open(PathAccountInfoFile, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                stream.Write(ciphertext, 0, ciphertext.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            

        }

        public void DeleteUserAccountsFromSecureStore()
        {
            try
            {
                File.Delete(PathAccountInfoFile);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
