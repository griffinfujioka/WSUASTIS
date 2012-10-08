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
    public partial class ProductDetailsPage : PhoneApplicationPage
    {
        #region Product Details Page Constructor 
        public ProductDetailsPage()
        {
            InitializeComponent();
            DataContext = App.ViewModel;
            PageTitle.Text = App.ViewModel.SelectedProduct.title;
            qtyTxtBox.Text = App.ViewModel.SelectedProduct.quantity.ToString();
            priceTxtBox.Text = App.ViewModel.SelectedProduct.price.ToString(); 
        }
        #endregion 

        private void qtyTxtBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            
        }

        private void priceTxtBox_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        #region Save button click
        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
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
            else
            {
                App.ViewModel.SelectedProduct.quantity = Convert.ToInt32(qtyTxtBox.Text);
                MessageBox.Show("Inventory saved.");
                NavigationService.Navigate(new Uri("/SubcategoriesPage.xaml", UriKind.Relative));
            }

        }
        #endregion 

        #region Add item to cart 
        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {
            ProductEqualityComparer pc = new ProductEqualityComparer(); 
            var productToAdd = new Product(App.ViewModel.SelectedProduct);

            if (App.Cart.Contains(productToAdd, pc))
            {
                int index = 0; 
                foreach (Product product in App.Cart)
                {

                    if (productToAdd.title == App.Cart[index].title)
                    {
                        App.Cart[index].quantity += 1; 
                        break;
                    }

                    index++; 
                }
            }
            else
            {
                productToAdd.quantity = 1;
                App.Cart.Add(productToAdd);    /* Add item to cart */
            }

            
            MessageBox.Show("Added " + productToAdd.title + " to cart.");
            NavigationService.Navigate(new Uri("/SubcategoriesPage.xaml", UriKind.Relative));

        }
        #endregion 

        #region View Cart
        private void viewCartBtn_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Cart.xaml", UriKind.Relative));
        }
        #endregion 


    }
}