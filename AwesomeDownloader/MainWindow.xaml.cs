using System;
using System.Collections.Generic;
using System.IO;
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

namespace AwesomeDownloader.View
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

        string DownloadFolder { get; set; } = @"C:\AwesomeDownloader\";

        private void ADWindow_Loaded(object sender, RoutedEventArgs e) {
            if(!Directory.Exists(DownloadFolder))
                Directory.CreateDirectory(DownloadFolder);

        }

        private void Button_DownloadPage_Click(object sender, RoutedEventArgs e) {
            Frame_DownloadPage.Visibility = Visibility.Visible;
            Frame_SettingsPage.Visibility = Visibility.Collapsed;
        }

        private void Button_SettingsPage_Click(object sender, RoutedEventArgs e) {
            Frame_DownloadPage.Visibility = Visibility.Collapsed;
            Frame_SettingsPage.Visibility = Visibility.Visible;
        }
    }
}
