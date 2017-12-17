namespace StickDownWindows.Services.Login.Tokens
{
    public class GoogleOfflineToken
    {
        public string id_token { get; set; }

        // required to access the refresh token
        public string authorization_code { get; set; }
    }
}
