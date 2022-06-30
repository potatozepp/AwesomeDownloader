using Microsoft.Win32;
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
using Microsoft.WindowsAPICodePack.Dialogs;

namespace AwesomeDownloader.View {
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page {
        public SettingsPage() {
            InitializeComponent();
        }

        private void Button_ChooseDownloadFolder_Click(object sender, RoutedEventArgs e) {
            CommonOpenFileDialog cofd = new CommonOpenFileDialog {
                IsFolderPicker = true
            };
            if(cofd.ShowDialog() == CommonFileDialogResult.Ok) {
                TextBox_DownloadfolderPath.Text = cofd.FileName;
            }

        }

        private void Button_SaveSettings_Click(object sender, RoutedEventArgs e) {
            UserSettings.Default.DownloadFolderPath = TextBox_DownloadfolderPath.Text;
            UserSettings.Default.Save();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e) {
            TextBox_DownloadfolderPath.Text = UserSettings.Default.DownloadFolderPath;
        }
    }
}
