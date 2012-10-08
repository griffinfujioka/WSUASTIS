using System;
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
    public partial class ChangeDiscountAmountsPage : PhoneApplicationPage
    {
        public ChangeDiscountAmountsPage()
        {
            InitializeComponent();
            bulkTxtBox.Text = App.discountsDictionary["Bulk"].ToString();
            facultyTxtBox.Text = App.discountsDictionary["Faculty"].ToString();
            staffTxtBox.Text = App.discountsDictionary["Staff"].ToString();
            studentTxtBox.Text = App.discountsDictionary["Student"].ToString(); 
        }

        private void saveDiscountsBtn_Click(object sender, EventArgs e)
        {
            if (!App.isManager)
            {
                MessageBoxResult mb = MessageBox.Show("Only managers can change discount amounts.", "Please login as manager", MessageBoxButton.OKCancel);

                /* TODO: Figure out how to go to login page and if successfully logged in, come back to this page */
                if (mb == MessageBoxResult.OK)
                    NavigationService.Navigate(new Uri("/ManagerLoginPage.xaml", UriKind.Relative));
                else
                    return;
            }
            else
            {
                App.discountsDictionary["Bulk"] = Convert.ToDouble(bulkTxtBox.Text);
                App.discountsDictionary["Faculty"] = Convert.ToDouble(facultyTxtBox.Text);
                App.discountsDictionary["Staff"] = Convert.ToDouble(staffTxtBox.Text);
                App.discountsDictionary["Student"] = Convert.ToDouble(studentTxtBox.Text);
                NavigationService.Navigate(new Uri("/CheckoutPage.xaml", UriKind.Relative));

            }
        }
    }
}