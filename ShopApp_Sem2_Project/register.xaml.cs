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
        // Same connection string to ensure that a username with the same credentials doesn't already exist
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Owner\Desktop\College Work\OOD\ShopApp_Sem2_Project\ShopApp_Sem2_Project\users.mdf;Integrated Security=True;";

        public register()
        {
            InitializeComponent();
        }

        public register(string username, string password)
        {
            InitializeComponent();

            if (txtUsername == null)  // sets the first two boxes to the values passed from login
                txtUsername = new TextBox();

            if (txtNewPassword == null)
                txtNewPassword = new PasswordBox();

            txtUsername.Text = username;
            txtNewPassword.Password = password;
        }

        private void BtnRegisterNewUser_Click(object sender, RoutedEventArgs e)
        {
            string newUsername = txtUsername.Text.Trim();
            string newPassword = txtNewPassword.Password.Trim();
            string confirmPassword = txtConfirmPassword.Password.Trim();

            // The values entered must pass the same tests
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
               if(newPassword == confirmPassword) // Password and Confirm Password must match
               {
                    bool isRegistered = isUserRegistered(newUsername, newPassword); // a method is called to check if a user with these credentials already exists

                    if (isRegistered == true) // if the method returns true then the username is already taken and the user must come up with another
                    {
                        MessageBox.Show("Username already taken please try again with a different username");
                        break;
                    }
                    else if (isRegistered == false)
                    {
                        using (SqlConnection sqlCon = new SqlConnection(connectionString)) // opens a connection to the "users" db again
                        {
                            sqlCon.Open();

                            string query = "INSERT INTO users(Username, Password) VALUES(@Username, @Password)"; // this time we are inserting the new values into the db
                            SqlCommand sqlCmd = new SqlCommand(query, sqlCon);             
                            sqlCmd.CommandType = System.Data.CommandType.Text;
                            sqlCmd.Parameters.AddWithValue("@Username", newUsername);
                            sqlCmd.Parameters.AddWithValue("@Password", newPassword);

                            sqlCmd.ExecuteNonQuery();

                            MessageBox.Show($"New user added! Username: {newUsername} Password: {newPassword}"); // Displays the username and password registered

                            this.NavigationService.Navigate(new login()); // returns to the login page
                            break;
                        }
                    }
               }
               else if(newPassword != confirmPassword)
               {
                    MessageBox.Show("Passwords do not match please try again"); // if the passwords do not match the user is asked to retry
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
                sqlCmd.Parameters.AddWithValue("@Username", newUsername); // This time we only want to see if the usernames match
                                                                            // we are fine with passwords matching on different users

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
