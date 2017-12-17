using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;

namespace StickDownWindows
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void LoginGoogleButton_OnClick(object sender, RoutedEventArgs e)
        {
            var token = await (new Services.Login.GoogleLoginHelper()).LoginWithGoogleByDefaultBrowser();

            bool isLogin =
                await new Services.Login.ClientAzure().Login(MobileServiceAuthenticationProvider.Google, JObject.FromObject(token));

            if (isLogin)
                MainTextBlock.Text = "Zalogowano";
            else
                MainTextBlock.Text = "Failed";


        }

        private void LoginFacebookButton_OnClick(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
