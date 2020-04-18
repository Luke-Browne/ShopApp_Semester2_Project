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

        public List<Item> allItems = new List<Item>();
        public List<cartItem> cartItems = new List<cartItem>();

        public decimal price1; public decimal price2; public decimal price3; public decimal price4;
        public string name1; public string name2; public string name3; public string name4;

        public shopHome()
        {
            InitializeComponent();
        }

        public shopHome(List<cartItem> cartItems)
        {
            InitializeComponent();
            this.cartItems = cartItems;
        }

        private void LbxCategories_Loaded(object sender, RoutedEventArgs e)
        {
            lbxCategories.SelectedIndex = 0;

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

        private void LbxCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            populateComboBoxes();

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
                                name1 = name.ToString();
                                break;
                            case 1:
                                tblkName2.Text = name;
                                name2 = name.ToString();
                                break;
                            case 2:
                                tblkName3.Text = name;
                                name3 = name.ToString();
                                break;
                            case 3:
                                tblkName4.Text = name;
                                name4 = name.ToString();
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
                                price1 = Convert.ToDecimal(priceWithDecimal);
                                break;
                            case 1:
                                tblkPrice2.Text = ("Price : €" + priceWithDecimal);
                                price2 = Convert.ToDecimal(priceWithDecimal);
                                break;
                            case 2:
                                tblkPrice3.Text = ("Price : €" + priceWithDecimal);
                                price3 = Convert.ToDecimal(priceWithDecimal);
                                break;
                            case 3:
                                tblkPrice4.Text = ("Price : €" + priceWithDecimal);
                                price4 = Convert.ToDecimal(priceWithDecimal);
                                break;
                            case 4:
                                break;
                        }
                        a++;
                    }
                }
            }
        }

        private void populateComboBoxes()
        {
            int a = 0;
            int i = 1;

            switch (a)
            {
                case 0:
                    while(i < 10)
                    {
                        qty1.Items.Add(i);
                    }
                    i = 0;
                    break;
                case 1:
                    while (i < 10)
                    {
                        qty2.Items.Add(i);
                    }
                    i = 0;
                    break;
                case 2:
                    while (i < 10)
                    {
                        qty3.Items.Add(i);
                    }
                    i = 0;
                    break;
                case 3:
                    while (i < 10)
                    {
                        qty4.Items.Add(i);
                    }
                    i = 0;
                    break;
                case 4:
                    break;
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
            addItem(name1, price1);
        }
        private void BtnAddItem2_Click(object sender, RoutedEventArgs e)
        {
            addItem(name2, price2);
        }
        private void BtnAddItem3_Click(object sender, RoutedEventArgs e)
        {
            addItem(name3, price3);
        }
        private void BtnAddItem4_Click(object sender, RoutedEventArgs e)
        {
            addItem(name4, price4);
        }

        private void addItem(string name, decimal price)
        {
            string productAdded = $"{name}\nPrice : {price:C2}\n-----------------------------------";

            cartItem item = new cartItem(productAdded);

            cartItems.Add(item);
        }

        private void BtnViewCart_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new cart(cartItems));
        }
    }
}
