using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShopApp_Sem2_Project
{
    /// <summary>
    /// Interaction logic for cart.xaml
    /// </summary>
    public partial class cart : Page
    {
        private List<cartItem> cartItems;

        public int totalQuantity;
        public decimal totalPrice;
        public decimal displayPrice = 0;

        public cart(List<cartItem> cartItems, int totalQuantity, decimal totalPrice)
        {
            InitializeComponent();

            this.cartItems = cartItems;
            this.totalQuantity = totalQuantity;
            this.totalPrice = totalPrice;
        }

        private void BtnLgout_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Logging Out! Cart will be cleared. Are you sure you wish to logo out?", "Caution!", MessageBoxButton.YesNo);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    this.NavigationService.Navigate(new login());
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private void BtnViewShop_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new shopHome(cartItems));
        }

        private void LbxCartItems_Loaded(object sender, RoutedEventArgs e)
        {
            displayPrice = displayPrice + totalPrice;

            var results = cartItems.Select(x => x.ProductAdded);

            lbxCartItems.ItemsSource = results;

            tblkTotalQuantity.Text = totalQuantity.ToString();

            tblkTotalPrice.Text = $"{displayPrice:C2}";
        }

        private void BtnClearCart_Click(object sender, RoutedEventArgs e)
        {
            displayPrice = 0;

            MessageBoxResult result = MessageBox.Show("Are you sure you wish to clear your cart?", "Caution!", MessageBoxButton.YesNo);

            switch(result)
            {
                case MessageBoxResult.Yes:
                    lbxCartItems.ItemsSource = null;
                    cartItems.Clear();
                    this.NavigationService.Navigate(new shopHome());
                        break;
                case MessageBoxResult.No:
                    break;
            }
        }
    }
}
