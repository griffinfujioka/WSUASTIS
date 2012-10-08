/* 
 * 
 * Person.cs
 * Author: Griffin Fujioka 
 * CptS 422 - Fall 2012 
 * 
 * This file contains the definition of a Product whose attributes include: 

        *  ProductID      Image        Price
        *  Quantity       Category     Title
        *  Description    Subcategory

 * 
 */
using System.Data.Linq.Mapping;     // Table attributes
using System.ComponentModel;        // Notify Property Changing 
using System.Data.Linq;             // DataContext 
using System.Collections.Generic; 

namespace WSUASTIS
{
    //  TODO:? Define enums for category and subcategory?
    #region Product Table
    [Table]
    
    public class Product : INotifyPropertyChanged, INotifyPropertyChanging
    {
        private int _ProductID;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int ProductID
        {
            get { return _ProductID; }
            set
            {
                if (_ProductID != value)
                {
                    NotifyPropertyChanging("ProductID");
                    _ProductID = value;
                    NotifyPropertyChanged("ProductID");
                }
            }
        }

        private string _imageUrl; 
        [Column]
        public string imageUrl
        {
            get { return _imageUrl; }
            set
            {

                _imageUrl = value;
                NotifyPropertyChanging("imageUrl");
                NotifyPropertyChanged("imageUrl");
            }
        }

        private double _price;
        [Column]
        public double price
        {
            get { return _price; }
            set
            {
                if (_price != value)
                {
                    NotifyPropertyChanging("price");
                    _price = value;
                    NotifyPropertyChanged("price");
                }
            }
        }

        private int _quantity;
        [Column]
        public int quantity
        {
            get { return _quantity; }
            set
            {
                NotifyPropertyChanging("quantity");
                _quantity = value;
                NotifyPropertyChanged("quantity");
            }
        }

        private string _category;
        [Column]
        public string category
        {
            get { return _category; }
            set
            {
                NotifyPropertyChanging("category");
                _category = value;
                NotifyPropertyChanged("category");
            }
        }

        private string _title;
        [Column]
        public string title
        {
            get { return _title; }
            set
            {
                NotifyPropertyChanging("title");
                _title = value;
                NotifyPropertyChanged("title");
            }
        }

        private string _description;
        [Column]
        public string description
        {
            get { return _description; }
            set
            {
                NotifyPropertyChanging("description");
                _description = value;
                NotifyPropertyChanged("description");
            }
        }

        private string _subcategory;
        [Column]
        public string subcategory
        {
            get { return _subcategory; }
            set
            {
                NotifyPropertyChanging("subcategory");
                _subcategory = value;
                NotifyPropertyChanged("subcategory");
            }
        }

        #region Constructor 
        public Product()
        {

        }
        #endregion 
        #region Copy Constructor
        public Product(Product product)
        {
            title = product.title;
            description = product.description;
            category = product.category;
            subcategory = product.subcategory;
            quantity = product.quantity;
            price = product.price;
            ProductID = product.ProductID;
            imageUrl = product.imageUrl; 
        }
        #endregion 
    #endregion

        /* Maybe you could add an array of categories for which this product belongs to
         * Then you could iterate through that array to load products from a particular character */ 

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify that a property changed
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging;

        // Used to notify that a property is about to change
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion
    }

    class ProductEqualityComparer : IEqualityComparer<Product>
    {

        public bool Equals(Product p1, Product p2)
        {
            if (p1.title == p2.title)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public int GetHashCode(Product p)
        {
            int hCode = (int)p.price ^ p.quantity;
            return hCode.GetHashCode();
        }

    }

    #region Inventory DataContext
    public class ProductDataContext : DataContext
    {
        public ProductDataContext(string connectionString)
            : base(connectionString) { }

        public Table<Product> Inventory;
    }
    #endregion 

}