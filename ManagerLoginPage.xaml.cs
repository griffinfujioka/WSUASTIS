using System;
using System.Collections; 
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace WSUASTIS
{
    public partial class ManagerLoginPage : PhoneApplicationPage
    {
        public System.Windows.Navigation.JournalEntry navigatedFromPage;    /* Which page did we come from? */ 
        #region A dictionary of valid logins
        Dictionary<string, string> validLogins = new Dictionary<string, string>()
        {
               {"griffin", "yee"}, 
               {"andy", "422"}, 
               {"TA", "422"}
            
        };
        #endregion


        public ManagerLoginPage()
        {
            InitializeComponent();

        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTxtBox.Text;
            string password = passwordTxtBox.Password;

            /* Create a dictionary out of the user input */
            Dictionary<string, string> userEntry = new Dictionary<string, string>() { { username, password } };

            /* TODO: Figure out the real way to see if a dictionary contains an entry. 
             * Until then, this is considered a bug 
             */

            bool validUsername = validLogins.ContainsKey(username);
            Exception nre = null; 
            bool validLogin = false; 
            try
            {
                
                string passwordFound = (validLogins[username]);
                App.isManager = true; /* Let program now the user is now logged in as a manager */
                MessageBox.Show("Logged in as manager.");
                string fromPage = navigatedFromPage.Source.ToString(); 
                switch (fromPage)
                {
                    case "/MainPage.xaml":
                        NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                        break; 
                    case "/ProductDetailsPage.xaml":
                        NavigationService.Navigate(new Uri("/ProductDetailsPage.xaml", UriKind.Relative));
                        break; 
                    case "/CheckoutPage.xaml":
                        NavigationService.Navigate(new Uri("/CheckoutPage.xaml", UriKind.Relative));
                        break; 
                    default: break; 
                }
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid login.");
                usernameTxtBox.Text = "";
                passwordTxtBox.Password = ""; 
            }
            
            
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            navigatedFromPage = NavigationService.BackStack.FirstOrDefault();

            if (navigatedFromPage != null && navigatedFromPage.Source.ToString() == "/MainPage.xaml")
            {
                NavigationService.RemoveBackEntry();
            }
        }
    }
}