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
        public string discountTitle; 

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
        public double initialNetTotal;
        public bool salesTransaction = true; 

        #region Checkout Page Constructor
        public CheckoutPage()
        {
            InitializeComponent();
            initializeDiscountList(); 
            defaultPicker.ItemsSource = discountTypes;
            itemsTotal = calculateItemizedTotal();
            itemsTotalTxtBlock.Text = string.Format("Items total: ${0:N2}", itemsTotal); 
            taxes = calculateTax(itemsTotal);
            taxTxtBlock.Text = string.Format("Tax total: ${0:N2}", taxes); 
            initialNetTotal = itemsTotal + taxes;
            netTotal = initialNetTotal;
            totalTxtBlock.Text = string.Format("Order total: ${0:N2}", netTotal);
            discountTitle = ""; 
        }
        #endregion 

        #region Apply discounts click
        private void discountBtn_Click(object sender, RoutedEventArgs e)
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
            discountTitle = (defaultPicker.SelectedItem as discountType).type; 
            netTotal = (initialNetTotal - discount); /* Update net total */
            totalTxtBlock.Text = string.Format("Order total: ${0:2}", netTotal.ToString());



        }
        #endregion

        #region ListPicker selection changed event 
        private void defaultPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            discountTitle = (defaultPicker.SelectedItem as discountType).type;
            App.discount = App.discountsDictionary[discountTitle]; 
        }
        #endregion 

        #region Change discounts button click
        private void changeDiscountsBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!App.isManager)
            {
                MessageBoxResult mb = MessageBox.Show("Only managers can edit inventory information.", "Please login as manager", MessageBoxButton.OKCancel);

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
            /* Find the product in the database and adjust it's quantity value */ 
            EmailComposeTask emailTask = new EmailComposeTask();

            string Order = "Transaction ID: " + App.transactionID ;
            Order += "\nTransaction type: Sale " + "\n\n";
            App.transactionID++;
            Order += "Items:"; 
            foreach (Product product in App.Cart)
            {
                var foundProduct = App.ViewModel.InventoryDB.Inventory.FirstOrDefault(s => s.title == product.title);

                if (foundProduct != null)
                {
                    if (salesTransaction)        /* If it's a sales transaction */
                    {
                        foundProduct.quantity -= product.quantity;  /* subtract from the quantity */
                        App.ViewModel.SaveChangesToDB(); 
                    }
                }
                Order += "\n" + product.title + "\nPrice: $" + string.Format("{0:N2}", product.price) + "\nQuantity: " + product.quantity + "\n";
            }

            Order += "\n\nItems total: $" + string.Format("{0:N2}", itemsTotal);
            Order += "\nTax: $" + string.Format("{0:N2}", taxes);
            Order += "\nTotal: $" + string.Format("{0:N2}",netTotal);

            if(discountTitle != "")
                Order += "\n\nDiscounts applied: " + discountTitle;
            /* TODO: Discounts applied */
            Order += "\n\nThank you for your business with WSUASTIS!"; 

            emailTask.Subject = "Your WSUASTIS order has been processed.";
            emailTask.Body = Order;

            emailTask.Show();
        }
        #endregion 

        #region Change discount percentages 
        private void changeDiscountsBtnAppBar_Click(object sender, EventArgs e)
        {
            if (!App.isManager)
            {
                MessageBoxResult mb = MessageBox.Show("Only managers can edit inventory information.", "Please login as manager", MessageBoxButton.OKCancel);

                if (mb == MessageBoxResult.OK)
                    NavigationService.Navigate(new Uri("/ManagerLoginPage.xaml", UriKind.Relative));
                else
                    return;
            }
            NavigationService.Navigate(new Uri("/ChangeDiscountAmountsPage.xaml", UriKind.Relative));
        }
        #endregion 

        private void mainPageAppBarBtn_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
    }
}