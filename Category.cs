/* Categories will remain constant - nobody can add a new category,
 * therefore categories do not interact with the database. */ 


using System.Collections.Generic;

namespace WSUASTIS
{
    public enum Categories
    {
        Men,
        Women, 
        Performance_Apparel, 
        Hats, 
        T_shirts,
        Sweatshirts
    }; 
    public class Category
    {
        public string ImageUri
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public string Notification
        {
            get;
            set;
        }

        public bool DisplayNotification
        {
            get
            {
                return !string.IsNullOrEmpty(this.Notification);
            }
        }

        public string Message
        {
            get;
            set;
        }

        public string GroupTag
        {
            get;
            set;
        }
        
        // TODO: For each category, go through and populate its products into this data structure 
        public List<Product> ProductsInCategory
        {
            get; 
            set; 
        }
        
    }
}