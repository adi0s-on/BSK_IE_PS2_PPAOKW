using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BSK_PPAOKW.PS
{
    /// <summary>
    /// Logika interakcji dla klasy PS3PseudorandomNumber.xaml
    /// </summary>
    public partial class PS3PseudorandomNumber : UserControl
    {
        public string resultStringMaxTwentyChars = "";

        public Lfsr LfsrMethod { get; set; }
        public PS3PseudorandomNumber()
        {
            InitializeComponent();
            Stop.IsEnabled = false;
            Stop.Background = Brushes.Gray;
            V.Visibility = Visibility.Hidden;
            Result.Visibility = Visibility.Hidden;
            ProgressBarIndefinite.Visibility = Visibility.Hidden;
            Delete.Visibility = Visibility.Hidden;
            DeleteIcon.Visibility = Visibility.Hidden;

        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            string polynomial = Polynomial.Text.ToString();
            int[] Powers = new int[polynomial.Length];
            int counter = 0;
          
            if(LfsrMethod == null)
            {
                try
                {
                    for (int i = 0; i < polynomial.Length; i++)
                    {
                        if (polynomial[i] == '^')
                        {
                            int counter2 = 0;
                            while (Int32.TryParse(polynomial[i + 1 + counter2].ToString(), out int nothing))
                            {
                                Powers[counter] = Int32.Parse(Powers[counter].ToString() + polynomial[i + 1 + counter2].ToString());
                                counter2++;
                                if (i + 1 + counter2 == polynomial.Length)
                                {
                                    break;
                                }
                            }
                            counter++;
                        }
                    }
                    ErrorTextBlock.Text = "";
                    LfsrMethod = new Lfsr(Powers.Max());
                    LfsrMethod.IsStopped = false;
                    V.Visibility = Visibility.Visible;
                    Result.Visibility = Visibility.Visible;
                    ProgressBarIndefinite.Visibility = Visibility.Visible;
                    Delete.Visibility = Visibility.Hidden;
                    DeleteIcon.Visibility = Visibility.Hidden;
                }
                catch (Exception)
                {
                    ErrorTextBlock.Text = "WRONG polynomial!";
                }
            }

            if (LfsrMethod != null)
            {
                LfsrMethod.IsStopped = false;
                new Thread(GenerateKey).Start(); 
                SolidColorBrush brushButtonActive = new SolidColorBrush(Color.FromRgb(0, 173, 181));
                
                Start.IsEnabled = false;
                Start.Background = Brushes.Gray;
                Stop.IsEnabled = true;
                Stop.Background = brushButtonActive;
                ProgressBarIndefinite.IsIndeterminate = true;
                Delete.Visibility = Visibility.Hidden;
                DeleteIcon.Visibility = Visibility.Hidden;
            }
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            if(LfsrMethod != null)
            {
                LfsrMethod.IsStopped = true;
                SolidColorBrush brushButtonActive = new SolidColorBrush(Color.FromRgb(0, 173, 181));

                Start.IsEnabled = true;
                Start.Background = brushButtonActive;
                Stop.IsEnabled = false;
                Stop.Background = Brushes.Gray;
                ProgressBarIndefinite.IsIndeterminate = false;

                Delete.Visibility = Visibility.Visible;
                DeleteIcon.Visibility = Visibility.Visible;
            }
        }

        public void GenerateKey()
        {
            try
            {
                while(!LfsrMethod.IsStopped)
                {
                    int sliderValue = 0;
                    this.Dispatcher.Invoke(() =>
                    {
                        bool row = LfsrMethod.AddRow();
                        sliderValue = Convert.ToInt32(SpeedSlider.Value);
                        string toAdd = "";

                        if (row)
                        {
                            toAdd = "1";
                        }
                        else
                        {
                            toAdd = "0";
                        }

                        if (resultStringMaxTwentyChars.Length >= 21)
                        {
                            resultStringMaxTwentyChars = resultStringMaxTwentyChars.Substring(1);
                            resultStringMaxTwentyChars += toAdd;
                        }
                        else resultStringMaxTwentyChars += toAdd;

                        Result1.Text = ""; Result2.Text = ""; Result3.Text = "";

                        if (resultStringMaxTwentyChars.Length <= 8)
                        {
                            Result3.Text = resultStringMaxTwentyChars;
                        }
                        else
                        {
                            Result3.Text = resultStringMaxTwentyChars.Substring(resultStringMaxTwentyChars.Length - 8);
                        }

                        if(resultStringMaxTwentyChars.Length <= 13 && resultStringMaxTwentyChars.Length > 8)
                        {
                            Result2.Text = resultStringMaxTwentyChars.Remove(resultStringMaxTwentyChars.Length - 8);
                        }
                        else if(resultStringMaxTwentyChars.Length > 13)
                        {
                            Result2.Text = resultStringMaxTwentyChars.Remove(resultStringMaxTwentyChars.Length - 8).Substring(resultStringMaxTwentyChars.Length - 8 - 5);
                        }

                        if (resultStringMaxTwentyChars.Length <= 21 && resultStringMaxTwentyChars.Length > 13)
                        {
                            Result1.Text = resultStringMaxTwentyChars.Remove(resultStringMaxTwentyChars.Length - 8 - 5);
                        }
                    });

                    Thread.Sleep(120 - sliderValue);
                }
            }
            catch
            {
                ErrorTextBlock.Text = "Unknown Error has occured!";
            }

        }

        private void DeletePolynominal(object sender, RoutedEventArgs e)
        {
            Result1.Text = ""; Result2.Text = ""; Result3.Text = ""; Polynomial.Text = "";
            LfsrMethod = null;
            V.Visibility = Visibility.Hidden;
            Result.Visibility = Visibility.Hidden;
            ProgressBarIndefinite.Visibility = Visibility.Hidden;
            Delete.Visibility = Visibility.Hidden;
            DeleteIcon.Visibility = Visibility.Hidden;
            resultStringMaxTwentyChars = "";
            SpeedSlider.Value = 0;

        }
    }
}

/*


using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.ComponentModel;
using System.Threading;
using System.Windows.Controls;

namespace BSK_PPAOKW.PS
{
    /// <summary>
    /// Logika interakcji dla klasy PS3PseudorandomNumber.xaml
    /// </summary>
    public partial class PS3PseudorandomNumber : UserControl
    {
        public bool receivedBoolean = false;
        BackgroundWorker worker = new BackgroundWorker();
        public Lfsr LfsrMethod { get; set; }
        public PS3PseudorandomNumber()
        {
            InitializeComponent();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;

            if (LfsrMethod != null)
            {
                LfsrMethod.IsStopped = false;
            }
            worker.RunWorkerAsync(100);
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (LfsrMethod == null)
            {
                return;
            }

            
            for (int i = 0; i <= LfsrMethod.RowLength * LfsrMethod.RowLength; i++)
            {
                receivedBoolean = LfsrMethod.AddRow();
                worker.ReportProgress(i);
            }
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }
    
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (receivedBoolean)
            {
                Result.Text += "1";
            }
            else
            {
                Result.Text += "0";
            }
            Thread.Sleep(10);
        }


        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            if(LfsrMethod != null)
            {
                LfsrMethod.IsStopped = true;
            }
            worker.CancelAsync();
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            string polynomial = Polynomial.Text.ToString();
            int[] Powers = new int[polynomial.Length / 2];
            int counter = 0;
            try
            {
                for (int i = 0; i < polynomial.Length; i++)
                {
                    if (polynomial[i] == '^')
                    {
                        Powers[counter] = (Int32.Parse(polynomial[i + 1].ToString()));
                        counter++;
                    }
                }
                ErrorTextBlock.Text = "";
                LfsrMethod = new Lfsr(Powers.Max());
                LfsrMethod.IsStopped = false;

            }
            catch (Exception)
            {
                ErrorTextBlock.Text = "WRONG polynomial!";
            }
        }
    }
}

*/
