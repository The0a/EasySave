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
//using EasySave.Library.ViewModel;

namespace EasySave.WPF
{
    /// <summary>
    /// Logique d'interaction pour DisplayJobs.xaml
    /// </summary>
    public partial class DisplayJobs : Window
    {
        public ViewModel vm { get; set; }
        public DisplayJobs()
        {
            InitializeComponent();
            vm = new ViewModel();
            PrintAllJobs(vm.DisplayJobs());
        }

        private void PrintAllJobs(string[][] jobs)
        {
            TextBox tb;
            int nameIncr = 1;
            if (jobs.Length == 0)
            {
                tb = new TextBox();
                tb = GenerateTextBox(tb, nameIncr);
                tb.Text = "No Job available";
                ListJobs.Children.Add(tb);
            }
            else
            {
                foreach (string[] job in jobs)
                {
                    tb = new TextBox();
                    tb = GenerateTextBox(tb, nameIncr);
                    tb.Text = TextToPrint(job);
                    ListJobs.Children.Add(tb);
                    nameIncr++;
                }
            }
        }

        private TextBox GenerateTextBox(TextBox tb, int numberTb)
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
            tb.IsReadOnly = true;
            tb.Width = 450f;
            tb.Margin = margin;
            tb.Height = 175f;
            tb.Background = colorBg;
            tb.BorderBrush = colorContour;
            tb.BorderThickness = border;
            tb.TextAlignment = TextAlignment.Center;
            tb.Text = "";
            return tb;
        }

        private string TextToPrint(string[] jobs)
        {
            string message = "";

            message += jobs[0];
            message += "\n\n";
            message += "Source : ";
            message += "\n";
            message += jobs[1];
            message += "\n\n";
            message += "Destination : ";
            message += "\n";
            message += jobs[2];
            message += "\n\n";
            message += "Type de Backup : \n";
            if (jobs[3] == "True")
            {
                message += "Differential";
            }

            else
            {
                message += "Complete";
            }
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
