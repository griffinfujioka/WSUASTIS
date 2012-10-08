using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Generic; 
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Linq; 

namespace WSUASTIS
{
    public class ProductViewModel : INotifyPropertyChanged
    {
        private ProductDataContext _InventoryDB;

        private Category _SelectedCategory;
        private Product _SelectedProduct; 


        public ProductDataContext InventoryDB
        {
            get { return _InventoryDB; }
        }

        // Class constructor, create the data context object.
        public ProductViewModel(string DBConnectionString)
        {
            _InventoryDB = new ProductDataContext(DBConnectionString);
        }

        public ProductViewModel(ProductDataContext db)
        {
            _InventoryDB = db;
        }

        public void SaveChangesToDB()
        {
            _InventoryDB.SubmitChanges(); 
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        private ObservableCollection<Category> _Categories;
        public ObservableCollection<Category> Categories
        {
            get { return _Categories; }
            set
            {
                _Categories = value;
                NotifyPropertyChanged("Categories");
            }

        }

        private ObservableCollection<Product> _Products;
        public ObservableCollection<Product> Products
        {
            get { return _Products; }
            set
            {
                _Products = value;
                NotifyPropertyChanged("Products");
            }

        }

        public Product SelectedProduct
        {
            get { return _SelectedProduct; }
            set
            {
                _SelectedProduct = value;
                NotifyPropertyChanged("SelectedProduct");
            }
        }

        public Category SelectedCategory
        {
            get { return _SelectedCategory; }
            set
            {
                _SelectedCategory = value;
                NotifyPropertyChanged("SelectedCategory"); 
            }
        }

        public void AddProduct(Product product)
        {
            InventoryDB.Inventory.InsertOnSubmit(product); /* Add to data context */

            InventoryDB.SubmitChanges();            /* Save changes to the DB */

            Products.Add(product);  /* Add to observable collection */ 
        }


        // Query database and load the collections and list used by the pivot pages.
        public void LoadCollectionsFromDatabase()
        {

            // Specify the query for all to-do items in the database.
            var ProductsInDB = from Product product in InventoryDB.Inventory select product;

            // Query the database and load all to-do items.
            Products = new ObservableCollection<Product>(ProductsInDB);

        }

        #region Add the products to the database manually on initialization of app 
        public void PopulateProductDB()
        {
            /* I'm having an error right now where each time I run the application I'm putting all of these Products in the database again... :( */ 
            /* Start by creating a list of Products. We'll actually insert them into the database later */ 
            List<Product> ProductInventory = new List<Product>();
            if (!App.GlobalVars.DBHasBeenPopulated)
            {
                #region Hats
                ProductInventory.Add(new Product() { title="Black Hat", imageUrl="/Images/BlackHat.jpg", subcategory="Hats", price=19.99, quantity= 30});
                ProductInventory.Add(new Product() { title = "Gray Hat", imageUrl = "/Images/GrayHat.jpg", subcategory = "Hats", price = 19.99, quantity = 30 });
                ProductInventory.Add(new Product() { title = "Crimson Hat", imageUrl = "/Images/Hats.jpg", subcategory = "Hats", price = 19.99, quantity = 40 });
                ProductInventory.Add(new Product() { title = "Beanie", imageUrl = "/Images/Men_Beanie.jpg", subcategory = "Hats", price = 14.99, quantity = 50 });
                ProductInventory.Add(new Product() { title = "Tie Up Beanie", imageUrl = "/Images/TieUpBeanie.jpg", subcategory = "Hats", price = 14.99, quantity = 50 });
                ProductInventory.Add(new Product() { title = "Visor", imageUrl = "/Images/Visor.jpg", subcategory = "Hats", price = 10, quantity = 1 });
                #endregion 

                #region Men
                ProductInventory.Add(new Product() { title = "Black Polo", imageUrl = "/Images/BlackPolo.jpg", subcategory = "Men", price = 39.99, quantity = 25 });
                ProductInventory.Add(new Product() { title = "Jersey", imageUrl = "/Images/Men_Jersey.jpg", subcategory = "Men", price = 65.99, quantity = 50 });
                ProductInventory.Add(new Product() { title = "T-Shirt", imageUrl = "/Images/Men_Tshirt.jpg", subcategory = "Men", price = 14.99, quantity = 50 });
                #endregion 

                #region Women
                ProductInventory.Add(new Product() { title = "V-neck", imageUrl = "/Images/Women.jpg", subcategory = "Women", price = 14.99, quantity = 1 });
                ProductInventory.Add(new Product() { title = "Hooded Sweatshirt", imageUrl = "/Images/Women_Hoodie.jpg", subcategory = "Women", price = 14.99, quantity = 50 });
                ProductInventory.Add(new Product() { title = "Jersey", imageUrl = "/Images/Women_Jersey.jpg", subcategory = "Women", price = 14.99, quantity = 50 });
                ProductInventory.Add(new Product() { title = "Long Sleeve Tee", imageUrl = "/Images/Women_LongSleeve.jpg", subcategory = "Women", price = 14.99, quantity = 50 });
                ProductInventory.Add(new Product() { title = "Women's Athletic Pants", imageUrl = "/Images/Women_PerformancePants.jpg", subcategory = "Women", price = 24.99, quantity = 50 });
                ProductInventory.Add(new Product() { title = "V-neck", imageUrl = "/Images/Women_VNeck.jpg", subcategory = "Women", price = 14.99, quantity = 50 });
                #endregion 

                #region Performance Apparel 
                ProductInventory.Add(new Product() { title = "Black Golf Shirt", imageUrl = "/Images/BlackPolo.jpg", subcategory = "PerformanceApparel", price = 39.99, quantity = 25 });
                ProductInventory.Add(new Product() { title = "Athletic Shorts", imageUrl = "/Images/Men_Shorts.jpg", subcategory = "PerformanceApparel", price = 29.99, quantity = 30 });
                ProductInventory.Add(new Product() { title = "Athletic Shrots", imageUrl = "/Images/PerformanceApparel.jpg", subcategory = "PerformanceApparel", price = 29.99, quantity = 30 });
                ProductInventory.Add(new Product() { title = "Women's Athletic Pants", imageUrl = "/Images/Women_PerformancePants.jpg", subcategory = "PerformanceApparel", price = 24.99, quantity = 50 });
                #endregion 

                /*
                ProductInventory.Add(new Product() { title = "", imageUrl = "", subcategory = "", price = 10, quantity = 1 });
                ProductInventory.Add(new Product() { title = "", imageUrl = "", subcategory = "", price = 10, quantity = 1 });
                ProductInventory.Add(new Product() { title = "", imageUrl = "", subcategory = "", price = 10, quantity = 1 });
                ProductInventory.Add(new Product() { title = "", imageUrl = "", subcategory = "", price = 10, quantity = 1 });
                 */ 
                

                /* Add the products to the database */ 
                foreach (Product product in ProductInventory)
                {
                    if (!InventoryDB.Inventory.Contains<Product>(product))
                        AddProduct(product);
                }

                InventoryDB.SubmitChanges();
            }

            App.GlobalVars.DBHasBeenPopulated = true;

        }
        #endregion 


    }
}