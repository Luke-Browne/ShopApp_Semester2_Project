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
        // Connects to the "users" database
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Owner\Desktop\College Work\OOD\ShopApp_Sem2_Project\ShopApp_Sem2_Project\users.mdf;Integrated Security=True;";

        public login()
        {
            InitializeComponent();
        }

        public void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text.Trim(); // Takes the values entered in the two txtBoxes
            string password = txtPassword.Password.Trim();

            // And runs it through some acceptance tests
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

            // If it passes these tests it gets passed into the isValidUser method where it is put against active usernames and password
            bool userAccess = isValidUser(username, password);

            if(userAccess == true) // Returned true after a match in the isValidUser method
            {
                MessageBox.Show("Username and Password correct! Logging in...");
                this.NavigationService.Navigate(new shopHome());
            }
            else if(userAccess == false) // Returned false after no match in the method
            {
                MessageBox.Show("Username or Password Incorrect");
            }
        }

        public bool isValidUser(string username, string password)
        {
            SqlConnection sqlCon = new SqlConnection(connectionString); // Creates a new sqlConnection using the string provided at the top

            try // Try catch to display if the information could not be found
            {
                if(sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    sqlCon.Open();
                    string query = "SELECT COUNT(1) FROM users WHERE Username=@Username AND Password=@Password";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.CommandType = System.Data.CommandType.Text;  // Runs the entered values against all the values in the databases
                    sqlCmd.Parameters.AddWithValue("@Username", username);
                    sqlCmd.Parameters.AddWithValue("@Password", password);

                    int count = Convert.ToInt32(sqlCmd.ExecuteScalar()); // if there is a match count will be set to 1

                    if(count == 1)
                    {
                        return true; // successful login
                    }
                    else
                    {
                        return false; // login falied
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
                sqlCon.Close(); // Closes the connection to the databases after the neccessary functions were performed
            }
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text.Trim();      // If the register button is clicked then for easier use I pass the already entered username and password to the register page
            string password = txtPassword.Password.Trim();      //  to save the user from entering the same data twice

            this.NavigationService.Navigate(new register(username, password));
        }
    }
}
