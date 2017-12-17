using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using StickDownWindows.Services.Login.Tokens;


namespace StickDownWindows.Services.Login
{
    public class GoogleLoginHelper
    {
        public const string RedirectUri = "http://localhost:2012/";
        public const string AuthorizationEndpoint = "https://accounts.google.com/o/oauth2/auth";
        public const string ClientId = "853051828885-f9m5auu8bbthlftq77hkdjb10shindbo.apps.googleusercontent.com";
        public const string SsDomain = RedirectUri;

        public async Task<GoogleOfflineToken> LoginWithGoogleByDefaultBrowser()
        {
            try
            {
                var authorizationRequest =
                    $"{AuthorizationEndpoint}?redirect_uri={RedirectUri}&response_type=permission%20id_token&scope=email%20profile%20openid&openid.realm=&client_id={ClientId}&ss_domain={SsDomain}&prompt=select_account&fetch_basic_profile=true&gsiwebsdk=2";

                // Creates an HttpListener to listen for requests on that redirect URI.
                var http = new HttpListener();
                http.Prefixes.Add(RedirectUri);
                http.Start();

                // Opens request in the browser.
                System.Diagnostics.Process.Start(authorizationRequest);

                // Waits for the OAuth authorization response.
                var context = await http.GetContextAsync();

                var response = context.Response;
                var responseString = "<html><head><script src=\"https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js\"></script></head><body>Please return to the app: <script>var fragment = location.hash; $.get(\'http://localhost:2012\' + \'?\' + fragment.replace(/^.*#/, \'\'));</script></body></html>";

                var buffer = Encoding.UTF8.GetBytes(responseString);
                response.ContentLength64 = buffer.Length;
                var responseOutput = response.OutputStream;
                await responseOutput.WriteAsync(buffer, 0, buffer.Length).ContinueWith((task) =>
                {
                    responseOutput.Close();
                });

                context = await http.GetContextAsync();
                var queryStringValues = context.Request.QueryString.GetValues(0);
                if (queryStringValues == null) return null;

                var token = queryStringValues[0];
                http.Stop();
                return new GoogleOfflineToken()
                {
                    id_token = token
                };
            }
            catch (Exception exception)
            {
                return null;
            }
        }
    }
}
