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
    public partial class ReceiptPage : PhoneApplicationPage
    {
        public ReceiptPage()
        {
            InitializeComponent();
            itemsTotalTxtBlock.Text = string.Format("Items total: ${0:N2}", App.GlobalVars.itemsTotal);
            taxTxtBlock.Text = string.Format("Tax total: ${0:N2}", App.GlobalVars.taxes);
            totalTxtBlock.Text = string.Format("Order total: ${0:N2}", App.GlobalVars.netTotal);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            /* Find the product in the database and adjust it's quantity value */
            EmailComposeTask emailTask = new EmailComposeTask();

            string Order = "Transaction ID: " + App.transactionID;
            Order += "\nTransaction type: Sale " + "\n\n";
            App.transactionID++;
            Order += "Items:";
            foreach (Product product in App.Cart)
            {
                var foundProduct = App.ViewModel.InventoryDB.Inventory.FirstOrDefault(s => s.title == product.title);

                if (foundProduct != null)
                {
                    if (App.GlobalVars.salesTransaction)        /* If it's a sales transaction */
                    {
                        foundProduct.quantity -= product.quantity;  /* subtract from the quantity */
                        App.ViewModel.SaveChangesToDB();
                    }
                }
                Order += "\n" + product.title + "\nPrice: $" + string.Format("{0:N2}", product.price) + "\nQuantity: " + product.quantity + "\n";
            }

            /* Empty shopping cart */
            App.Cart.Clear(); 

            Order += "\n\nItems total: $" + string.Format("{0:N2}", App.GlobalVars.itemsTotal);
            Order += "\nTax: $" + string.Format("{0:N2}", App.GlobalVars.taxes);
            Order += "\nTotal: $" + string.Format("{0:N2}", App.GlobalVars.netTotal);

            if (App.GlobalVars.discountTitle != "")
                Order += "\n\nDiscounts applied: " + App.GlobalVars.discountTitle;
            
            Order += "\n\nThank you for your business with WSUASTIS!";

            emailTask.Subject = "Your WSUASTIS order has been processed.";
            emailTask.Body = Order;

            emailTask.Show();
        }

        private void mainPageBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
    }
}