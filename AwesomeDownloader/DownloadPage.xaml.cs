using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using NAudio;
using System.Windows;
using System.Windows.Controls;
using AwesomeDownloader.BusinessLayer;
using AwesomeDownloader.Models;
using NAudio.Wave;
using System.Collections.ObjectModel;
using Microsoft.VisualBasic;

namespace AwesomeDownloader.View {
    /// <summary>
    /// Interaction logic for DownloadPage.xaml
    /// </summary>
    public partial class DownloadPage : Page {
        public DownloadPage() {
            InitializeComponent();
        }

        ObservableCollection<DownloadPageFileViewModel> Songs { get; set; } = new ObservableCollection<DownloadPageFileViewModel>();

        private async void Button_Download_Click(object sender, RoutedEventArgs e) {
            DownloadFromYT downloadFromYT = new DownloadFromYT();
            string downloadFolder = UserSettings.Default.DownloadFolderPath;

            switch(ComboBox_FileType.SelectedIndex) {
                case 0:
                    await downloadFromYT.DownloadMP3Async(downloadFolder, TextBox_URL.Text);
                    LoadFilesToDataGrid();
                    break;
                case 1:
                    await downloadFromYT.DownloadMP4Async(downloadFolder, TextBox_URL.Text);
                    LoadFilesToDataGrid();
                    break;
                case 2:
                    await downloadFromYT.DownloadMP3AndMP4(downloadFolder, TextBox_URL.Text);
                    LoadFilesToDataGrid();
                    break;
                default:
                    break;
            }
            
       
        }

        private void Window_DownloadPage_Loaded(object sender, RoutedEventArgs e) {
            string downloadFolder = UserSettings.Default.DownloadFolderPath;
            if(!string.IsNullOrEmpty(downloadFolder)) {
                if(!Directory.Exists(downloadFolder)) {
                    Directory.CreateDirectory(downloadFolder);
                }
            } else {
                string input = Interaction.InputBox("Please select a Download Folder (Can be changed in Settings)", "DownloadFolder Edit", @"C:\AwesomeDownloader", 10, 10);
                if (input != "")
                {
                    UserSettings.Default.DownloadFolderPath = input;
                }
                else
                {
                    UserSettings.Default.DownloadFolderPath = @"C:\AwesomeDownloader";
                }
            }

            LoadFilesToDataGrid();
        }

        private void LoadFilesToDataGrid() {
            string downloadFolder = UserSettings.Default.DownloadFolderPath;
            string[] filePaths = Directory.GetFiles(downloadFolder);
            foreach(var item in filePaths) {
                if(Path.GetExtension(item) == ".mp3") {
                    MediaFoundationReader mp3FileReader = new MediaFoundationReader(item);
                    DownloadPageFileViewModel dpfvm = new DownloadPageFileViewModel();

                    dpfvm.FileName = Path.GetFileNameWithoutExtension(item);
                    dpfvm.FileType = Path.GetExtension(item);
                    dpfvm.Duration = mp3FileReader.TotalTime.ToString(@"hh\:mm\:ss");

                    Songs.Add(dpfvm);
                }
            }
            DataGrid_DownloadedFiles.ItemsSource = Songs;
        }

        private void Button_OpenFolder_Click(object sender, RoutedEventArgs e) {
            string downloadFolder = UserSettings.Default.DownloadFolderPath;
            Process.Start("explorer.exe",downloadFolder);
        }

        private void Button_LoadFiles_Click(object sender, RoutedEventArgs e) {
            LoadFilesToDataGrid();
        }
    }
}
