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
using Microsoft.Phone.Tasks; 

namespace WSUASTIS
{
    public partial class CheckoutPage : PhoneApplicationPage
    {
        #region A simple discountType class so I can bind it to a drop down menu 
        public class discountType
        {
            public string type { get; set; }
        }
        #endregion 


        public List<discountType> discountTypes = new List<discountType>();

        #region Intialize Discount List
        public void initializeDiscountList()
        {
            discountTypes.Add(new discountType() { type = "Faculty"});
            discountTypes.Add(new discountType() { type = "Staff" });
            discountTypes.Add(new discountType() { type = "Student" });
            discountTypes.Add(new discountType() { type = "Bulk" }); 
        }
        #endregion 

        public double itemsTotal;
        public double taxes;
        public double netTotal;

        #region Checkout Page Constructor
        public CheckoutPage()
        {
            InitializeComponent();
            initializeDiscountList(); 
            defaultPicker.ItemsSource = discountTypes;
            itemsTotal = calculateItemizedTotal();
            itemsTotalTxtBlock.Text = string.Format("Items total: ${0:C}", itemsTotal.ToString()); 
            taxes = calculateTax(itemsTotal);
            taxTxtBlock.Text = string.Format("Tax total: ${0:2}", taxes.ToString()); 
            netTotal = itemsTotal + taxes;
            totalTxtBlock.Text = string.Format("Order total: ${0:2}", netTotal.ToString()); 
        }
        #endregion 

        #region Apply discounts click
        private void discountBtn_Click(object sender, RoutedEventArgs e)
        {

            if (!App.isManager)
            {
                MessageBoxResult mb = MessageBox.Show("Only managers can apply discounts.", "Please login as manager", MessageBoxButton.OKCancel);

                /* TODO: Figure out how to go to login page and if successfully logged in, come back to this page */
                if (mb == MessageBoxResult.OK)
                    NavigationService.Navigate(new Uri("/ManagerLoginPage.xaml", UriKind.Relative));
                else
                    return;
            }
            else
            {
                /* Switch on selected discount and subtract accordingly */
                string selectedDiscount = (defaultPicker.SelectedItem as discountType).type;
                double discount = 0.0; 
                switch (selectedDiscount)
                {
                    case "Bulk":
                        discount = App.discountsDictionary[selectedDiscount] * netTotal;
                        break; 
                    case "Faculty":
                        discount = App.discountsDictionary[selectedDiscount] * netTotal;
                        break;
                    case "Student":
                        discount = App.discountsDictionary[selectedDiscount] * netTotal;
                        break;
                    case "Staff":
                        discount = App.discountsDictionary[selectedDiscount] * netTotal;
                        break;
                    default: break; 
                }

                netTotal = (netTotal - discount); /* Update net total */
                totalTxtBlock.Text = string.Format("Order total: ${0:2}", netTotal.ToString());
            }
         

        }
        #endregion

        #region ListPicker selection changed event 
        private void defaultPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string discount = (defaultPicker.SelectedItem as discountType).type;
            App.discount = App.discountsDictionary[discount]; 
        }
        #endregion 

        #region Change discounts button click
        private void changeDiscountsBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!App.isManager)
            {
                MessageBoxResult mb = MessageBox.Show("Only managers can edit inventory information.", "Please login as manager", MessageBoxButton.OKCancel);

                /* TODO: Figure out how to go to login page and if successfully logged in, come back to this page */
                if (mb == MessageBoxResult.OK)
                    NavigationService.Navigate(new Uri("/ManagerLoginPage.xaml", UriKind.Relative));
                else
                    return;
            }
            NavigationService.Navigate(new Uri("/ChangeDiscountAmountsPage.xaml", UriKind.Relative));
        }
        #endregion 

        #region Calculate itemized total
        private double calculateItemizedTotal()
        {
            double returnTotal = 0.0; 
            foreach(Product product in App.Cart)
            {
                returnTotal += product.price * product.quantity; 
            }
            return returnTotal; 
        }
        #endregion 

        #region Calculate tax
        private double calculateTax(double itemizedTotal)
        {
            double returnTax = 0.0;

            returnTax = itemizedTotal * .08;    /* Sales tax of 8% */ 
            return returnTax;
        }
        #endregion 

        #region Print receipts click
        private void printReceiptBtn_Click(object sender, RoutedEventArgs e)
        {
            EmailComposeTask emailTask = new EmailComposeTask();

            string Order = "Your sale order has been processed.\n";
            Order += "Transaction ID: 13\n";    /* TODO: Unique transaction IDs */ 

            foreach (Product product in App.Cart)
            {
                Order += "\n" + product.title;
            }

            Order += "\nTotal: " + netTotal.ToString();

            emailTask.Subject = "Your WSUASTIS order has been processed.";
            emailTask.Body = Order;

            emailTask.Show();
        }
        #endregion 

        private void changeDiscountsBtnAppBar_Click(object sender, EventArgs e)
        {
            if (!App.isManager)
            {
                MessageBoxResult mb = MessageBox.Show("Only managers can edit inventory information.", "Please login as manager", MessageBoxButton.OKCancel);

                /* TODO: Figure out how to go to login page and if successfully logged in, come back to this page */
                if (mb == MessageBoxResult.OK)
                    NavigationService.Navigate(new Uri("/ManagerLoginPage.xaml", UriKind.Relative));
                else
                    return;
            }
            NavigationService.Navigate(new Uri("/ChangeDiscountAmountsPage.xaml", UriKind.Relative));
        }

    }
}