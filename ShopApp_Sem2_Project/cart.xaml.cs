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
using System.Data.SqlClient;

namespace ShopApp_Sem2_Project
{
    /// <summary>
    /// Interaction logic for cart.xaml
    /// </summary>
    public partial class cart : Page
    {
        public decimal price;

        private List<cartItem> cartItems;
        private List<discountCode> discountCodes = new List<discountCode>(); 

        private int totalQuantity;
        private decimal totalPrice;

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
            this.NavigationService.Navigate(new shopHome(cartItems, totalQuantity, totalPrice));
        }

        private void LbxCartItems_Loaded(object sender, RoutedEventArgs e)
        {
            var results = cartItems.Select(x => x.ProductAdded);

            lbxCartItems.ItemsSource = results;

            tblkTotalQuantity.Text = totalQuantity.ToString();

            tblkTotalPrice.Text = $"{totalPrice:C2}";
        }

        private void BtnClearCart_Click(object sender, RoutedEventArgs e)
        {
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

        private void BtnApplyDiscount_Click(object sender, RoutedEventArgs e)
        {
            discountCodes.Clear();

            string enteredCode = txtBoxDiscount.Text;

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Owner\Desktop\College Work\OOD\ShopApp_Sem2_Project\discounts.mdf;Integrated Security=True;";

            SqlConnection sqlCon = new SqlConnection(connectionString);

            string query = "SELECT * FROM discounts ORDER BY codeID";

            sqlCon.Open();

            SqlCommand sqlCmd = new SqlCommand(query, sqlCon);

            SqlDataReader dr = sqlCmd.ExecuteReader();

            if(dr.HasRows)
            {
                while(dr.Read())
                {
                    discountCode newCode = new discountCode(Convert.ToInt32(dr[0]), dr[1].ToString(), Convert.ToInt32(dr[2]));

                    discountCodes.Add(newCode);
                }
                if(discountCodes != null)
                {
                    var result = from d in discountCodes
                                  where d.Code == enteredCode
                                  select d.Code;

                    if(result.ToString() == enteredCode)
                    {
                        var values = from d in discountCodes
                                    where d.Code == enteredCode
                                    select d.Value;

                        foreach(int value in values)
                        {
                            MessageBox.Show($"Code Entered Correct! Applying discount of {value}%.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Sorry but that Discount Code was not recognized, please check you have entered your code correctly and try again!");
                    }
                }
            }
        }
    }
}
