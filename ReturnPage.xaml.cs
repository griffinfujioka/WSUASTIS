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
    public partial class ReturnPage : PhoneApplicationPage
    {
        public int quantity;

        #region Page constructor 
        public ReturnPage()
        {
            InitializeComponent();
        }
        #endregion 

        #region quantityTxtBox_GotFocus
        private void quantityTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            quantityTxtBox.Text = "";   /* Clear the textbox */ 
        }
        #endregion 

        #region Submit return button
        private void submitReturnBtn_Click(object sender, RoutedEventArgs e)
        {
            EmailComposeTask emailTask = new EmailComposeTask();
            int index = 0;
            string Output = "Transaction ID: " + App.transactionID;
            App.transactionID++; 
            Output += "\nTransaction type: Return " + "\n\n";

            Output += "Item returned: "; 
            var returnedProduct =  App.ViewModel.InventoryDB.Inventory.FirstOrDefault(s => s.title == App.ViewModel.SelectedProduct.title);
            foreach (Product product in App.Cart)
            {
                if (product.title == returnedProduct.title)
                    break; 

                index++; 
            }
            quantity = Convert.ToInt32(quantityTxtBox.Text); 
            if (returnedProduct != null)
            {
                Output += returnedProduct.title + "\n\tPrice refunded: $" + String.Format("{0:N2}", returnedProduct.price.ToString()) + "\n\tQuantity: " + quantity; 
                returnedProduct.quantity += quantity;
                App.ViewModel.SaveChangesToDB(); 
            }


            Output += "\n\nThank you for your business with WSUASTIS!"; 
            emailTask.Subject = "Your WSUASTIS return transaction has been processed.";
            emailTask.Body = Output;
            emailTask.Show();


        }
        #endregion 
    }
}