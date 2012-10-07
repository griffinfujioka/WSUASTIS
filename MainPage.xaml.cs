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
using System.Collections.ObjectModel;   // observable collections
using Microsoft.Phone.Controls;


namespace WSUASTIS
{
    public partial class MainPage : PhoneApplicationPage
    {
        #region MainPage constructor
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            
            DataContext = App.ViewModel;
            BindCategories();

            if (!App.GlobalVars.DBHasBeenPopulated)
                App.ViewModel.PopulateProductDB(); 

        }
        #endregion 

        #region Initialize a list of categories and bind them to the CategoriesListBox
        private void BindCategories()
        {
            List<Category> Categories = new List<Category>()
			{
				new Category() {
				    ImageUri ="/Images/Men_Jersey.jpg", 
				    Title = "Mens", Notification = "10% off!"
				},
				new Category() { 
				    ImageUri ="/Images/Women.jpg", 
				    Title = "Women",
                    Message="Find all your favorite Cougar gear!"
				},
				new Category() {
				    ImageUri = "/Images/Sweatshirts.jpg", 
				    Title = "Sweatshirts",
				    Message = "For those cold Pullman winters"
				},
				new Category() {
				    ImageUri = "/Images/PerformanceApparel.jpg", 
				    Title = "Performance Apparel", Notification = ""
				},
				new Category() {
				    ImageUri = "/Images/Hats.jpg", 
				    Title = "Hats", Message="Starting from $9.99"
				}
			};
            App.ViewModel.Categories = new ObservableCollection<Category>(Categories);
            App.ViewModel.SaveChangesToDB(); 
			this.CategoriesListBox.ItemsSource = Categories;

        }
        #endregion

        #region CategoriesListBox_SelectionChanged: Navigate to subcategories page
        private void CategoriesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var button = (sender as ListBox).SelectedItem as Category;
            App.ViewModel.SelectedCategory = button;

            if (button != null)
            {
                NavigationService.Navigate(new Uri("/SubcategoriesPage.xaml", UriKind.Relative));
            }
            return;
        }
        #endregion 

        #region Login as manager button clicked
        private void ApplicationBarMenuItem_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/ManagerLoginPage.xaml", UriKind.Relative));
        }
        #endregion 

        #region Override OnNavigatedTo
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            CategoriesListBox.SelectedItem = null; 
            base.OnNavigatedTo(e);
        }
        #endregion 
    }
}