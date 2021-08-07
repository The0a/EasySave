
using EasySave.WPF.Langages;
using EasySave_V3.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace EasySave.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModel VM { get; set; }
        /*Mutex m { get; set; }*/
        Langage Langage { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            VM = new ViewModel();
            VM.MThread.RunSoft(); //Security
            MonoInstance();
            InitializeVerification();
            InitializeLangage();
        }

        private void InitializeLangage()
        {
            Langage = new Langage(Langages.Langages.English);
            SelectLangage.Content = Langage.Langue;
        }

        private void InitializeVerification()
        {
            if (VM.IJ.NumberOfJobs() == 0)
            {
                ModifyJob.IsEnabled = false;
                DeleteJob.IsEnabled = false;
                ExecuteJob.IsEnabled = false;
            }
        }

        private void DisplayJobs_Click(object sender, RoutedEventArgs e)
        {
            DisplayJobs fenetre = new DisplayJobs();
            fenetre.Show();
            VM.MThread.CloseMutex();
            this.Close();
        }

        private void CreateJob_Click(object sender, RoutedEventArgs e)
        {
            CreateJob fenetre = new CreateJob();
            fenetre.Show();
            VM.MThread.CloseMutex();
            this.Close();
        }

        private void ModifyJob_Click(object sender, RoutedEventArgs e)
        {
            ModifyJob fenetre = new ModifyJob();
            fenetre.Show();
            VM.MThread.CloseMutex();
            this.Close();
        }

        private void DeleteJob_Click(object sender, RoutedEventArgs e)
        {
            DeleteJob fenetre = new DeleteJob();
            fenetre.Show();
            VM.MThread.CloseMutex();
            this.Close();
        }

        private void ExecuteJob_Click(object sender, RoutedEventArgs e)
        {
            //Read File General Param
            if (VM.SR.IsSoftRunning(VM.GP.FileContent["SoftWork"]))
            {
                // TODO : Print fenetre Erreur -> Logiciel métier en cours d'exec
                MessageBox.Show("Error, Work Software is running", "Execute Backup", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                ExecuteJob fenetre = new ExecuteJob();
                fenetre.Show();
                VM.MThread.CloseMutex();
                this.Close();
            }
        }

        private void GeneralParameters_Click(object sender, RoutedEventArgs e)
        {
            Parameter fenetre = new Parameter();
            fenetre.Show();
            VM.MThread.CloseMutex();
            this.Close();
        }

        private void MonoInstance()
        {
            if (!VM.MThread.MonoInstance())
            {
                MessageBox.Show("EasySave is already running", "EasySave Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(0);
            }
        }


        private void SelectLangage_Clic(object sender, RoutedEventArgs e)
        {
            if (Langage.Langue == "English")
            {
                Langage = new Langage(Langages.Langages.French);
            }
            else
            {
                Langage = new Langage(Langages.Langages.English);
            }
            SelectLangage.Content = Langage.Langue;
            DisplayJobs.Content = Langage.DisJobs();
            CreateJob.Content = Langage.CreJobs();
            ModifyJob.Content = Langage.ModJobs();
            ExecuteJob.Content = Langage.ExeJobs();
            DeleteJob.Content = Langage.DelJobs();
            GeneralParameters.Content = Langage.GenPJobs();
        }

    }
}
