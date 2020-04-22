// Title: Airsoft Shop App
// Author: Luke Browne 
// Student ID: S00187306

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

namespace ShopApp_Sem2_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Main.Content = new login(); // So in order to switch between pages I decided to use pages instead of tabs
                                            // I have a frame called "main" declared in the MainWindow.xaml which has everything loaded in on top of it

            this.Title = "Airsoft Shop App"; 
        }
    }
}
