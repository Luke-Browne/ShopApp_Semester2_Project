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
using System.Data.Linq;

namespace ShopApp_Sem2_Project
{
    /// <summary>
    /// Interaction logic for login.xaml
    /// </summary>
    public partial class login : Page
    {
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Owner\Desktop\College Work\OOD\ShopApp_Sem2_Project\ShopApp_Sem2_Project\users.mdf;Integrated Security=True;";

        public login()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Password.Trim();
            if(username.Length == 0 && password.Length == 0)
            {
                MessageBox.Show("Please enter a username and password");
                this.NavigationService.Navigate(new login());
            }
            else if(username.Length == 0)
            {
                MessageBox.Show("Please enter a username");
                this.NavigationService.Navigate(new login());
            }
            else if(password.Length == 0)
            {
                MessageBox.Show("Please enter a password");
                this.NavigationService.Navigate(new login());
            }

            bool userAccess = isValidUser(username, password);

            if(userAccess == true)
            {
                MessageBox.Show("Username and Password correct! Logging in...");
                this.NavigationService.Navigate(new shopHome());
            }
            else if(userAccess == false)
            {
                MessageBox.Show("Username or Password Incorrect");
            }
        }

        public bool isValidUser(string username, string password)
        {
            SqlConnection sqlCon = new SqlConnection(connectionString);

            try
            {
                if(sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    sqlCon.Open();
                    string query = "SELECT COUNT(1) FROM users WHERE Username=@Username AND Password=@Password";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.CommandType = System.Data.CommandType.Text;
                    sqlCmd.Parameters.AddWithValue("@Username", username);
                    sqlCmd.Parameters.AddWithValue("@Password", password);

                    int count = Convert.ToInt32(sqlCmd.ExecuteScalar());

                    if(count == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                sqlCon.Close();
            }
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new register());
        }
    }
}
