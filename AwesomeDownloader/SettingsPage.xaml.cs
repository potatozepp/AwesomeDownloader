using Microsoft.WindowsAPICodePack.Dialogs;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace AwesomeDownloader.View {
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page {

        List<string> bitrates { get; set; } = new List<string>() { "64 kbps", "128 kbps", "192 kbps" };
        List<string> videoQualities { get; set; } = new List<string>() { "144p", "240p", "360p", "480p", "720p", "1080p", "1440p", "2160p" };
        public SettingsPage() {
            InitializeComponent();
        }

        private void Page_SettingsPage_Loaded(object sender, RoutedEventArgs e) {


            TextBox_DownloadfolderPath.Text = UserSettings.Default.DownloadFolder;

            ComboBox_Bitrate.ItemsSource = bitrates;
            ComboBox_Bitrate.SelectedItem = UserSettings.Default.Bitrate;

            ComboBox_VideoQuality.ItemsSource = videoQualities;
            ComboBox_VideoQuality.SelectedItem = UserSettings.Default.VideoQuality;
            ComboBox_Bitrate.SelectedIndex = ComboBox_Bitrate.Items.IndexOf(UserSettings.Default.Bitrate);
        }

        private void Button_SaveSettings_Click(object sender, RoutedEventArgs e) {
            UserSettings.Default.DownloadFolder = TextBox_DownloadfolderPath.Text;
            UserSettings.Default.Bitrate = ComboBox_Bitrate.SelectedItem.ToString();
            UserSettings.Default.VideoQuality = ComboBox_VideoQuality.SelectedItem.ToString();
            UserSettings.Default.Save();
        }

        private void Button_ChooseDownloadFolder_Click(object sender, RoutedEventArgs e) {
            CommonOpenFileDialog cofd = new CommonOpenFileDialog {
                IsFolderPicker = true
            };
            if(cofd.ShowDialog() == CommonFileDialogResult.Ok)
                TextBox_DownloadfolderPath.Text = cofd.FileName;
        }
    }
}
