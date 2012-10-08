﻿using System;
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
    public partial class Cart : PhoneApplicationPage
    {
        public Cart()
        {
            InitializeComponent();
            cartListBox.ItemsSource = App.Cart; 
        }

        private void checkoutButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/CheckoutPage.xaml", UriKind.Relative));
        }
    }
}