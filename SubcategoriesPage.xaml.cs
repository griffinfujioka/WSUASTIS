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
using System.Collections.ObjectModel; 

namespace WSUASTIS
{
    public partial class SubcategoriesPage : PhoneApplicationPage
    {
        #region Page Constructor
        public SubcategoriesPage()
        {
            InitializeComponent();

            PageTitle.Text = App.ViewModel.SelectedCategory.Title; /* get category of that send you (i.e., Mens, Womens, etc.) */
            string category = App.ViewModel.SelectedCategory.Title; 

            /* Load all of the categories associated with that Title */ 
            load_products_from_Category(category);
        }
        #endregion 

        /* -------- TODO --------/ 
         * Show available products based on which category
         * 
         * Mens: T-shirt, Jersey, Hat, Shorts, Polos etc. 
         * Womens: V-neck
         * Sweatshirts: Find some Cougar sweatshirts
         * Performance Apparel
         * Hats
         */

        #region Load all products in a given category 
        private void load_products_from_Category(string category)
        {
            List<Product> ProductsToPrint = new List<Product>(); 
            switch (category)
            {
                case "Mens": 
                    ProductsToPrint = LoadMensProducts();
                    break; 
                case "Women":
                    ProductsToPrint = LoadWomensProducts();
                    break; 
               default: break; 
            }


            ProductsListBox.ItemsSource = ProductsToPrint; 
        }
        #endregion 

        #region LoadMensProducts()
        public List<Product> LoadMensProducts()
        {

            // Specify the query for men's products in inventory. Subcategory will be marked as Men 
            var MensInDB = from Product product in App.ViewModel.InventoryDB.Inventory where product.subcategory == "Men" select product;

            
            List<Product> MensProducts = new List<Product>(MensInDB);

            List<Product> returnList = new List<Product>(); 

            returnList.Add(MensProducts[0]); 
            returnList.Add(MensProducts[1]); 
            returnList.Add(MensProducts[2]); 
            /* HACK: Only return the first 3 products */
            return returnList; 

           
        }
        #endregion 
        
        #region LoadWomensProducts()
        public List<Product> LoadWomensProducts()
        {

            // Specify the query for men's products in inventory. Subcategory will be marked as Men 
            var WomensInDB = from Product product in App.ViewModel.InventoryDB.Inventory where product.subcategory == "Women" select product;

            List<Product> WomensProducts = new List<Product>(WomensInDB); 

            List<Product> returnList = new List<Product>();

            returnList.Add(WomensProducts[0]);
            returnList.Add(WomensProducts[1]);
            returnList.Add(WomensProducts[2]);
            returnList.Add(WomensProducts[3]);
            returnList.Add(WomensProducts[4]);
            returnList.Add(WomensProducts[5]); 

            return returnList;
        }
        #endregion 

        #region LoadPerformanceApparel()
        public List<Product> LoadPerformanceApparel()
        {
            List<Product> returnList = new List<Product>();

            return returnList;
        }
        #endregion 

        #region LoadHats()
        public List<Product> LoadHats()
        {
            List<Product> returnList = new List<Product>();

            return returnList;
        }
        #endregion 

        #region LoadSweatshirts()
        public List<Product> LoadSweatshirts()
        {
            List<Product> returnList = new List<Product>();

            return returnList;
        }
        #endregion 







    }
}