// quick console application to generate random discount codes

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace generateDiscounts
{
    class Program
    { 
        public static void Main(string[] args)
        {
            Random rand = new Random();

            //string newCode;
            int[] values = new int[] { 5, 5, 5, 5, 5, 10, 10, 10, 10, 15, 15, 15, 20, 20, 25, 25, 30 }; // the percentage discounts allowed to be generated, by adding a larger amount of smaller numbers 
                                                                                                        //  there is a greater chance of a smaller discount which makes sense in a business case
                                                                                                        // I have two sets of numbers to increase the chance of having a number or two in teh code
                                                                                                        // before I did that most codes wouldn't even have a number

            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ01234567890123456789";

            var random = new Random();

            Console.Write("Enter the number of codes you would like to be generated: ");
            int numOfCodes = Convert.ToInt32(Console.ReadLine());

            int i = 0;

            while(i != numOfCodes)
            {
                var code = new string(
                Enumerable.Repeat(chars, 6)
                      .Select(s => s[random.Next(s.Length)]) // generates a random 6 char code using our "var chars"
                      .ToArray());

                int discountAmount = values[rand.Next(values.Length)]; // selects a ranom number from the array
                                                                      // lower values like 5 and 10 will have a higher chance of being selected

                int count = testCodesLength();

                generateCodes(code, discountAmount, numOfCodes, count); // passed to the generateCodes method

                i++;
            }
        }


        public static int testCodesLength()
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Owner\Desktop\College Work\OOD\ShopApp_Sem2_Project\discounts.mdf;Integrated Security=True;";

            string test = "SELECT COUNT(*) FROM discounts"; // to get us the number of rows in the discounts db

            int count = 0;

            using (SqlConnection testConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmdCount = new SqlCommand(test, testConnection))
                {
                    testConnection.Open();
                    count = Convert.ToInt32(cmdCount.ExecuteScalar()); // counts the number using the query from above ^^
                }
                testConnection.Close();
                return count;
            }
        }
        public static void generateCodes(string code, int discountAmount, int numOfCodes, int count)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Owner\Desktop\College Work\OOD\ShopApp_Sem2_Project\discounts.mdf;Integrated Security=True;";

            if ((numOfCodes + count) < 101) // doesn't allow more than 100 codes to exist at a time
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString)) // opens a connection to the "discounts" db 
                {
                    sqlCon.Open();

                    string query = "INSERT INTO discounts(Code, Value) VALUES(@Code, @Value)";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.CommandType = System.Data.CommandType.Text;
                    sqlCmd.Parameters.AddWithValue("Code", code);
                    sqlCmd.Parameters.AddWithValue("@Value", discountAmount);

                    sqlCmd.ExecuteNonQuery();

                    Console.WriteLine($"\nEntering Code {code} with a percentage value of {discountAmount}%"); // shows all the codes and their percentages as they get added
                    sqlCon.Close();
                }
            }
            else
            {
                Console.WriteLine("Error writing codes to database. Code limit would be exceeded by {0}", Math.Abs(100 - numOfCodes)); // error message
            }
        }
    }
}
