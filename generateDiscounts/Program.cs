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
        static void Main(string[] args)
        {
            var random = new Random();
            Random rand = new Random();

            Console.Write("Enter the number of codes you would like to be generated: ");
            int numOfCodes = Convert.ToInt32(Console.ReadLine());

            //string newCode;
            int [] values = new int[] { 5,5,5,5,5,10,10,10,10,15,15,15,20,20,25,25,30 }; // the percentage discounts allowed to be generated, by adding a larger amount of smaller numbers 
                                                                                          //  there is a greater chance of a smaller discount which makes sense in a business case
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ01234567890123456789";           // I have two sets of numbers to increase the chance of having a number or two in teh code
                                                                                        // before I did that most codes wouldn't even have a number

            int i = 0;

            while(i != numOfCodes)
            {
                var result = new string(
                Enumerable.Repeat(chars, 6)
                      .Select(s => s[random.Next(s.Length)])
                      .ToArray());

                Console.WriteLine();

                Console.Write(result + " ");

                int discountAmount = rand.Next(0, values.Length);

                Console.Write(values[discountAmount]);

                i++;
            }

            //generateCodes();
        }

        //private static void generateCodes()
        //{
        //    string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Owner\Desktop\College Work\OOD\ShopApp_Sem2_Project\discounts.mdf;Integrated Security=True;";

        //    using (SqlConnection sqlCon = new SqlConnection(connectionString)) // opens a connection to the "discounts" db 
        //    {
        //        sqlCon.Open();

        //        string query = "INSERT INTO discounts";
        //        SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
        //        sqlCmd.CommandType = System.Data.CommandType.Text;

        //        sqlCmd.ExecuteNonQuery();

        //        Console.WriteLine();
        //    }
        //}
    }
}
