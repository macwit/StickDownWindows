namespace StickDownWindows.Services.Data
{
    public enum SignInProviderType
    {
        Google,
        Facebook,
        None
    }

    public static class ConversionExtensions
    {
        public static string ConvertToString(this SignInProviderType providerType)
        {
            switch (providerType)
            {
                case SignInProviderType.Google:
                    return "google";
                case SignInProviderType.Facebook:
                    return "facebook";
                default:
                    return "none";
            }
        }

        public static SignInProviderType ConvertToSignInProviderType(this string s)
        {
            switch (s)
            {
                case "google":
                    return SignInProviderType.Google;
                case "facebook":
                    return SignInProviderType.Facebook;
                default:
                    return SignInProviderType.None;
            }
        }
    }
}