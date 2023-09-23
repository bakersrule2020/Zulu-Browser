using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
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
namespace BrowserWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ConsoleWindow consoleWindow = new ConsoleWindow();
        public MainWindow()
        {
            InitializeComponent();
            
            consoleWindow.Show();
           
            urlField.Text = View.Source.AbsoluteUri;
            urlField.GotFocus += TextBox_GotFocus;
            urlField.LostFocus += TextBox_LostFocus;
            Gobutton.Click += Gobutton_Click;
            Console.WriteLine("Initialized browser!");
        }
        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            // Add your code here to handle the closing event
            MessageBoxResult result = MessageBox.Show("Are you sure you want to close the browser?", "Zulu Browser | Are you sure you'd like to quit?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            Console.WriteLine("Prompting user for close");
            if (result == MessageBoxResult.No)
            {
                // Cancel the closing event if the user chooses not to close
                e.Cancel = true;
                Console.WriteLine("User aborted close");
            }
            else
            {
                consoleWindow.Close();
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            // Clear the TextBox when it receives focus
           
                urlField.Text = string.Empty;
            
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            // Restore the placeholder text if the TextBox is empty
            if (string.IsNullOrWhiteSpace(urlField.Text))
            {
                urlField.Text = View.Source.AbsoluteUri;
            }
        }
        private void Gobutton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                Uri Uriforbrowse = new Uri(urlField.Text);
                Console.WriteLine("Going to " + urlField.Text);
                Console.WriteLine("===URI Info===");
                Console.WriteLine("URI Host: " + Uriforbrowse.Host);
                Console.WriteLine("URI Path: " + Uriforbrowse.AbsolutePath);
                Console.WriteLine("URI Query: " + Uriforbrowse.Query);
                Console.WriteLine("==================");
                View.Source = Uriforbrowse;
            }
            catch (UriFormatException ex)
            {
                Console.WriteLine("Error 001: InvalidFormatError\n" + ex);
                var currentpath = Environment.CurrentDirectory;
                Uri erroruri = new Uri("file://" + currentpath + "\\BrowserHtmls/error.html");
                View.Source = erroruri;
            }
        }
    }
}
