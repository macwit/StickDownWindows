
namespace StickDownWindows.Services.Data
{
    public class JsonAccount
    {
        public string UserId { get; set; }
        public string MobileServiceAuthenticationToken { get; set; }
        public SignInProviderType ProviderType { get; set; }
    }
}
