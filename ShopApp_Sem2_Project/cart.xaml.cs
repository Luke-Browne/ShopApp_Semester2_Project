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
        private decimal discount;
        private decimal grandTotal;

        public cart(List<cartItem> cartItems, int totalQuantity, decimal totalPrice, decimal discount, decimal grandTotal)
        {
            InitializeComponent();

            this.cartItems = cartItems;
            this.totalQuantity = totalQuantity; // info passed from the shopHome page
            this.totalPrice = totalPrice;
            this.grandTotal = grandTotal;
            this.discount = discount;
        }

        private void BtnLgout_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Logging Out! Cart will be cleared. Are you sure you wish to logo out?", "Caution!", MessageBoxButton.YesNo);

            switch (result)
            {
                case MessageBoxResult.Yes: // logout button still performs the same function
                    this.NavigationService.Navigate(new login());
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private void BtnViewShop_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new shopHome(cartItems, totalQuantity, totalPrice, discount, grandTotal));
        }

        private void LbxCartItems_Loaded(object sender, RoutedEventArgs e)
        {
            if(totalQuantity > 0)
            {
                var results = cartItems.Select(x => x.ProductAdded);

                lbxCartItems.ItemsSource = results;
            } // Displays cart emptry if nothing has been added
            else
            {
                List<string> empty = new List<string>(); // This was the only way I could work out how to get the lbx to display "Cart Empty"

                empty.Add("Cart Empty");
                lbxCartItems.ItemsSource = empty;
                lbxCartItems.FontSize = 47;
            }

            tblkTotalQuantity.Text = totalQuantity.ToString();

            tblkTotalPrice.Text = $"{totalPrice:C2}"; // Formatting for money values

            if (discount > 0)
            {
                btnApplyDiscount.IsEnabled = false; // if a discount has been applied then you can't apply another
                tblkDiscount.Text = $"{discount:C2}";
            }

            grandTotal = totalPrice - discount; // simple calculation

            tblkGrandTotal.Text = $"{grandTotal:C2}";

            if (grandTotal == 0)
            {
                var bc = new BrushConverter();
                btnCheckout.IsHitTestVisible = false; // can't check out with an empty cart
                btnCheckout.Background = (Brush)bc.ConvertFrom("#CDEBCA");
            }
            else
            {
                btnCheckout.IsHitTestVisible = true;
            }
        }

        private void BtnClearCart_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you wish to clear your cart?", "Caution!", MessageBoxButton.YesNo);

            switch (result)
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
            MessageBoxResult yesNo = MessageBox.Show("Are you sure you wish to submit this discount code? (After submitting a discount any items added afterwards will not be subject to the discount)", "Caution!", MessageBoxButton.YesNo);

            switch (yesNo)
            {
                case MessageBoxResult.Yes:
                    applyDiscount();
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }
        private void applyDiscount()
        {
            discountCodes.Clear();

            string enteredCode = txtBoxDiscount.Text;

            // connects to the "discounts" db
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Owner\Desktop\College Work\OOD\ShopApp_Sem2_Project\discounts.mdf;Integrated Security=True;";

            SqlConnection sqlCon = new SqlConnection(connectionString);

            string query = "SELECT * FROM discounts ORDER BY codeID";

            sqlCon.Open(); 

            SqlCommand sqlCmd = new SqlCommand(query, sqlCon);

            SqlDataReader dr = sqlCmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    // creates a discount item from the row values
                    discountCode newCode = new discountCode(Convert.ToInt32(dr[0]), dr[1].ToString(), Convert.ToInt32(dr[2]));

                    discountCodes.Add(newCode);
                }
                if (discountCodes != null)
                {
                    var result = from d in discountCodes
                                 where d.Code == enteredCode
                                 select d.Code;
                    // if there is a code which matches then the list should be more than 0

                    if (result.Count() != 0)
                    {
                        btnApplyDiscount.IsEnabled = false;

                        var values = from d in discountCodes
                                     where d.Code == enteredCode
                                     select d.Value;
                        // percentage value of this discount is gotten

                        foreach (int value in values)
                        {
                            MessageBox.Show($"Code Entered Correct! Applying discount of {value}%.");

                            decimal percentage = ((decimal)value) / 100;

                            discount = decimal.Multiply(totalPrice, percentage);

                            tblkDiscount.Text = $"{discount:C2}"; // calculations are made

                            grandTotal = totalPrice - discount;

                            tblkGrandTotal.Text = $"{grandTotal:C2}";

                            deleteCode(); // deletes the code that was just used
                        }
                    }
                    else
                    {
                        MessageBox.Show("Sorry but that Discount Code was not recognized, please check you have entered your code correctly and try again!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Sorry but there are no available discount codes at the moment!"); // if the discount table happens to be empty
            }

            dr.Close();
        }

        private void deleteCode() // after a discount code is entered it must be deleted 
        {
            string enteredCode = txtBoxDiscount.Text;

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Owner\Desktop\College Work\OOD\ShopApp_Sem2_Project\discounts.mdf;Integrated Security=True;";

            SqlConnection sqlCon = new SqlConnection(connectionString);

            sqlCon.Open();

            string queryDel = "DELETE FROM discounts WHERE code='" + enteredCode + "';";

            SqlCommand sqlCmdDel = new SqlCommand(queryDel, sqlCon);

            try
            {
                SqlDataReader drDel = sqlCmdDel.ExecuteReader();
                while (drDel.Read())
                {
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }

        private void BtnCheckout_Click(object sender, RoutedEventArgs e)
        {
            DateTime arrivalDate = DateTime.Today.AddDays(10);

            MessageBox.Show($"Order confirmed! Your package will be with you on {arrivalDate.ToString("dd/MM/yyyy")}\nThank you for shopping with us!");

            MessageBox.Show("Logging out..."); // checkout is pretty simple 

            this.NavigationService.Navigate(new login());
        }
    }
}