using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using EasySave_V3.Library.ViewModels;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace EasySave.WPF
{
    /// <summary>
    /// Logique d'interaction pour CreateJob.xaml
    /// </summary>
    public partial class CreateJob : Window
    {
        public string NameJob { get; private set; }
        public string SourceJob { get; private set; }
        public string DestinationJob { get; private set; }
        public bool TypeBackupJob { get; private set; }

        public ViewModel vm = new ViewModel();

        public CreateJob()
        {
            InitializeComponent();
            TypeBackupJob = false;
            Valider.IsEnabled = false;
        }

        private void BackHome_Click(object sender, RoutedEventArgs e)
        {
            MainWindow fenetre = new MainWindow();
            fenetre.Show();
            this.Close();
        }


        private void NameJobUser_TextChanged(object sender, TextChangedEventArgs e)
        {
            NameJob = NameJobUser.Text;
            CheckFormComplete();
        }

        private void SourcePath_Click(object sender, RoutedEventArgs e)
        {
            string sourceP = PathFolder();
            if (sourceP == "Fail")
            {
                SourcePath.Content = "Search folder...";
            }
            else
            {
                SourcePath.Content = sourceP;
                SourceJob = sourceP;
            }
            CheckFormComplete();
        }

        private void DestinationPath_Click(object sender, RoutedEventArgs e)
        {
            string destinationP = PathFolder();
            if (destinationP == "Fail")
            {
                DestinationPath.Content = "Search folder...";
            }
            else
            {
                DestinationPath.Content = destinationP;
                DestinationJob = destinationP;
            }
            CheckFormComplete();
        }

        private void Complete_Checked(object sender, RoutedEventArgs e)
        {
            TypeBackupJob = false;
        }

        private void Differential_Checked(object sender, RoutedEventArgs e)
        {
            TypeBackupJob = true;
        }


        private void Valider_Click(object sender, RoutedEventArgs e)
        {
            if (vm.VerifInputUser(NameJob, SourceJob, DestinationJob, TypeBackupJob))
            {
                if (vm.CreateJob(NameJob, SourceJob, DestinationJob, TypeBackupJob))
                {
                    MessageBox.Show("Backup has been created", "Create Backup", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                else
                {
                    MessageBox.Show("Creation of the job has crashed", "Create Backup", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                MainWindow fenetre = new MainWindow();
                fenetre.Show();
                this.Close();

            }
            else
            {
                MessageBox.Show("Bad Informations", "Modify Backup", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private string PathFolder()
        {
            CommonOpenFileDialog dlg = new CommonOpenFileDialog();
            string currentDirectory = Directory.GetCurrentDirectory();

            dlg.Title = "My Title";
            dlg.IsFolderPicker = true;
            dlg.InitialDirectory = currentDirectory;

            dlg.AddToMostRecentlyUsedList = false;
            dlg.AllowNonFileSystemItems = false;
            dlg.DefaultDirectory = currentDirectory;
            dlg.EnsureFileExists = true;
            dlg.EnsurePathExists = true;
            dlg.EnsureReadOnly = false;
            dlg.EnsureValidNames = true;
            dlg.Multiselect = false;
            dlg.ShowPlacesList = true;

            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
            {
                string folder = dlg.FileName;
                // Do something with selected folder string
                return folder;
            }
            else
            {
                return "Fail";
            }
        }

        private void CheckFormComplete()
        {
            if ((NameJob != null && NameJob != "") && SourceJob != null && DestinationJob != null)
            {
                Valider.IsEnabled = true;
            }
            else
            {
                Valider.IsEnabled = false;
            }
        }

    }
}
