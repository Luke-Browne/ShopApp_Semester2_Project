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
    /// Interaction logic for register.xaml
    /// </summary>
    public partial class register : Page
    {
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Owner\Desktop\College Work\OOD\ShopApp_Sem2_Project\ShopApp_Sem2_Project\users.mdf;Integrated Security=True;";

        public register()
        {
            InitializeComponent();
        }

        private void BtnRegisterNewUser_Click(object sender, RoutedEventArgs e)
        {
            string newUsername = txtUsername.Text.Trim();
            string newPassword = txtNewPassword.Password.Trim();
            string confirmPassword = txtConfirmPassword.Password.Trim();

            if (newUsername.Length == 0 && newPassword.Length == 0)
            {
                MessageBox.Show("Please enter a username and password");
                this.NavigationService.Navigate(new register());
            }
            else if (newUsername.Length == 0)
            {
                MessageBox.Show("Please enter a username");
                this.NavigationService.Navigate(new register());
            }
            else if (newPassword.Length == 0)
            {
                MessageBox.Show("Please enter a password");
                this.NavigationService.Navigate(new register());
            }
            else if (confirmPassword.Length == 0)
            {
                MessageBox.Show("Please confirm password");
                this.NavigationService.Navigate(new register());
            }

            while(newUsername.Length > 0 && newPassword.Length > 0)
            {
               if(newPassword == confirmPassword)
               {
                    bool isRegistered = isUserRegistered(newUsername, newPassword);

                    if (isRegistered == true)
                    {
                        MessageBox.Show("Username already taken please try again");
                    }
                    else if (isRegistered == false)
                    {
                        using (SqlConnection sqlCon = new SqlConnection(connectionString))
                        {
                            sqlCon.Open();

                            string query = "INSERT INTO users(Username, Password) VALUES(@Username, @Password)";
                            SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                            sqlCmd.CommandType = System.Data.CommandType.Text;
                            sqlCmd.Parameters.AddWithValue("@Username", newUsername);
                            sqlCmd.Parameters.AddWithValue("@Password", newPassword);

                            sqlCmd.ExecuteNonQuery();

                            MessageBox.Show($"New user added! Username: {newUsername} Password: {newPassword}");

                            this.NavigationService.Navigate(new login());
                            break;
                        }
                    }
               }
               else if(newPassword != confirmPassword)
               {
                    MessageBox.Show("Passwords do not match please try again");
                    break;
               }
            }
        }

        public bool isUserRegistered(string newUsername, string newPassword)
        {
            SqlConnection sqlCon = new SqlConnection(connectionString);

            if (sqlCon.State == System.Data.ConnectionState.Closed)
            {
                sqlCon.Open();
                string query = "SELECT COUNT(1) FROM users WHERE Username=@Username";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.CommandType = System.Data.CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Username", newUsername);

                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());

                if (count == 1)
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

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you wish to cancel?", "Caution!", MessageBoxButton.YesNo);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    this.NavigationService.Navigate(new login());
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }
    }
}
