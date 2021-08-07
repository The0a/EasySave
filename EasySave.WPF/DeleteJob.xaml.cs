/*using EasySave.Library.ViewModel;*/
using EasySave_V3.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EasySave.WPF
{
    /// <summary>
    /// Logique d'interaction pour DeleteJob.xaml
    /// </summary>
    public partial class DeleteJob : Window
    {

        public ViewModel VM { get; set; }
        public DeleteJob()
        {
            InitializeComponent();
            VM = new ViewModel();
            PrintAllJobs(VM.DisplayJobs());
        }

        private void PrintAllJobs(string[][] jobs)
        {
            Button tb;
            int nameIncr = 1;
            if (jobs.Length == 0)
            {
                tb = new Button();
                tb = GenerateTextBox(tb, nameIncr);
                tb.Content = "No Job available";
                ListJobs.Children.Add(tb);
            }
            else
            {
                foreach (string[] job in jobs)
                {
                    tb = new Button();
                    tb = GenerateTextBox(tb, nameIncr);
                    tb.Content = TextToPrint(job);
                    ListJobs.Children.Add(tb);
                    nameIncr++;
                }
            }
        }

        private Button GenerateTextBox(Button tb, int numberTb)
        {
            Thickness margin = new Thickness();
            margin.Left = 100f;
            margin.Top = 25f;
            margin.Right = 100f;
            margin.Bottom = 25f;

            Thickness border = new Thickness();
            border.Left = 3f;
            border.Top = 3f;
            border.Right = 3f;
            border.Bottom = 3f;


            Brush colorBg = new SolidColorBrush(Color.FromRgb(175, 223, 219));
            Brush colorContour = new SolidColorBrush(Color.FromRgb(117, 201, 204));


            tb.Name = "Job_" + numberTb;
            tb.Width = 450f;
            tb.Margin = margin;
            tb.Height = 175f;
            tb.Background = colorBg;
            tb.BorderBrush = colorContour;
            tb.BorderThickness = border;
            tb.HorizontalContentAlignment = HorizontalAlignment.Center;
            tb.Content = "";
            tb.Click += (s, e) =>
            {
                var result = MessageBox.Show("Are you sure you want to Delete this job ?", "Delete Backup", MessageBoxButton.YesNo, MessageBoxImage.Information);

                if (result == MessageBoxResult.Yes)
                {
                    // cancel the closure of the form.
                    VM.DeleteJob(numberTb);
                    MessageBox.Show("Backup has been deleted", "Delete Backup", MessageBoxButton.OK, MessageBoxImage.Information);
                    MainWindow fenetre = new MainWindow();
                    fenetre.Show();
                    this.Close();

                }
            };
            return tb;
        }

        private string TextToPrint(string[] jobs)
        {
            string message = "Click to Delete :\n";

            message += jobs[0];

            return message;
        }

        private void BackHome_Click(object sender, RoutedEventArgs e)
        {
            MainWindow fenetre = new MainWindow();
            fenetre.Show();
            this.Close();
        }
    }
}
