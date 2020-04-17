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
using System.Data;
using System.Drawing;

namespace ShopApp_Sem2_Project
{
    /// <summary>
    /// Interaction logic for shopHome.xaml
    /// </summary>
    public partial class shopHome : Page
    {
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Owner\Desktop\College Work\OOD\ShopApp_Sem2_Project\items.mdf;Integrated Security=True;";

        List<Item> allItems = new List<Item>();
        List<cartItem> cartItems = new List<cartItem>();

        public shopHome()
        {
            InitializeComponent();
        }

        private void LbxCategories_Loaded(object sender, RoutedEventArgs e)
        {
            hideButtons();

            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM items ORDER BY itemID";

                sqlCon.Open();

                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);

                SqlDataReader dr = sqlCmd.ExecuteReader();

                if(dr.HasRows)
                {
                    while(dr.Read())
                    {
                        Item newItem = new Item(Convert.ToInt32(dr[0]), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), Convert.ToDecimal(dr[4]), dr[5].ToString(), dr[6].ToString());

                        allItems.Add(newItem);
                    }

                    if(allItems != null)
                    {
                        var results = allItems.Select(x => x.Category).Distinct();

                        lbxCategories.ItemsSource = results;
                    }
                }
                sqlCon.Close();
            }
        }

        private void hideButtons()
        {
            btnAddItem1.IsEnabled = false; btnAddItem1.Visibility = Visibility.Hidden;
            btnAddItem2.IsEnabled = false; btnAddItem2.Visibility = Visibility.Hidden;
            btnAddItem3.IsEnabled = false; btnAddItem3.Visibility = Visibility.Hidden;
            btnAddItem4.IsEnabled = false; btnAddItem4.Visibility = Visibility.Hidden;
        }
        private void showButtons()
        {
            btnAddItem1.IsEnabled = true; btnAddItem1.Visibility = Visibility.Visible;
            btnAddItem2.IsEnabled = true; btnAddItem2.Visibility = Visibility.Visible;
            btnAddItem3.IsEnabled = true; btnAddItem3.Visibility = Visibility.Visible;
            btnAddItem4.IsEnabled = true; btnAddItem4.Visibility = Visibility.Visible;
        }

        private void LbxCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            showButtons();

            clearItems();

            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                string selected = (string)e.AddedItems[0];

                foreach (object item in allItems)
                {
                    var images =  from i in allItems
                                  where selected == i.Category
                                  select i.Image;

                    int a = 0;

                    foreach (string image in images)
                    {
                         switch (a)
                         {
                             case 0:
                                img1.Source = new BitmapImage(new Uri($@"C:\Users\Owner\Desktop\College Work\OOD\ShopApp_Sem2_Project\ShopApp_Sem2_Project\Resources\{image}", UriKind.Absolute));
                                break;
                             case 1:
                                img2.Source = new BitmapImage(new Uri($@"C:\Users\Owner\Desktop\College Work\OOD\ShopApp_Sem2_Project\ShopApp_Sem2_Project\Resources\{image}", UriKind.Absolute));
                                break;
                             case 2:
                                 img3.Source = new BitmapImage(new Uri($@"C:\Users\Owner\Desktop\College Work\OOD\ShopApp_Sem2_Project\ShopApp_Sem2_Project\Resources\{image}", UriKind.Absolute));
                                 break;
                             case 3:
                                 img4.Source = new BitmapImage(new Uri($@"C:\Users\Owner\Desktop\College Work\OOD\ShopApp_Sem2_Project\ShopApp_Sem2_Project\Resources\{image}", UriKind.Absolute));
                                 break;
                             case 4:
                                 break;
                         }
                         a++;
                    }

                    a = 0;

                    var names =   from i in allItems
                                  where selected == i.Category
                                  select i.ProductName;

                    foreach (string name in names)
                    {
                        switch (a)
                        {
                            case 0:
                                tblkName1.Text = name;
                                break;
                            case 1:
                                tblkName2.Text = name;
                                break;
                            case 2:
                                tblkName3.Text = name;
                                break;
                            case 3:
                                tblkName4.Text = name;
                                break;
                            case 4:
                                break;
                        }
                        a++;
                    }

                    a = 0;

                    var manufacturers = from i in allItems
                                where selected == i.Category
                                select i.Manufacturer;

                    foreach (string man in manufacturers)
                    {
                        switch (a)
                        {
                            case 0:
                                tblkMan1.Text = "Manufacturer : " + man;
                                break;
                            case 1:
                                tblkMan2.Text = "Manufacturer : " + man;
                                break;
                            case 2:
                                tblkMan3.Text = "Manufacturer : " + man;
                                break;
                            case 3:
                                tblkMan4.Text = "Manufacturer : " + man;
                                break;
                            case 4:
                                break;
                        }
                        a++;
                    }

                    a = 0;

                    var info =  from i in allItems
                                where selected == i.Category
                                select i.Info;

                    foreach (string i in info)
                    {
                        switch (a)
                        {
                            case 0:
                                tblkInfo1.Text = i;
                                break;
                            case 1:
                                tblkInfo2.Text = i;
                                break;
                            case 2:
                                tblkInfo3.Text = i;
                                break;
                            case 3:
                                tblkInfo4.Text = i;
                                break;
                            case 4:
                                break;
                        }
                        a++;
                    }

                    a = 0;

                    var price = from i in allItems
                                where selected == i.Category
                                select i.Price;

                    foreach (decimal p in price)
                    {
                        string priceWithDecimal = p.ToString("0.00");

                        switch (a)
                        {
                            case 0:
                                tblkPrice1.Text = ("Price : €" + priceWithDecimal);
                                break;
                            case 1:
                                tblkPrice2.Text = ("Price : €" + priceWithDecimal);
                                break;
                            case 2:
                                tblkPrice3.Text = ("Price : €" + priceWithDecimal);
                                break;
                            case 3:
                                tblkPrice4.Text = ("Price : €" + priceWithDecimal);
                                break;
                            case 4:
                                break;
                        }
                        a++;
                    }
                }
            }
        }

        private void clearItems()
        {
            img1.Source = null; img2.Source = null; img3.Source = null; img4.Source = null;
            tblkName1.Text = null; tblkName2.Text = null; tblkName3.Text = null; tblkName4.Text = null;
            tblkInfo1.Text = null; tblkInfo2.Text = null; tblkInfo3.Text = null; tblkInfo4.Text = null;
            tblkPrice1.Text = null; tblkPrice2.Text = null; tblkPrice3.Text = null; tblkPrice4.Text = null;
            tblkMan1.Text = null; tblkMan2.Text = null; tblkMan3.Text = null; tblkMan4.Text = null;
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

        private void BtnAddItem1_Click(object sender, RoutedEventArgs e)
        {
            cartItem item = new cartItem(tblkName1.ToString(), Convert.ToDecimal(tblkPrice1));
        }
    }
}
