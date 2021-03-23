using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BSK_PPAOKW.PS
{
    /// <summary>
    /// Logika interakcji dla klasy PS3PseudorandomNumber.xaml
    /// </summary>
    public partial class PS3PseudorandomNumber : UserControl
    {
        public Lfsr LfsrMethod { get; set; }
        public PS3PseudorandomNumber()
        {
            InitializeComponent();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if (LfsrMethod != null)
            {
                LfsrMethod.IsStopped = false;
            }
            GenerateKey();
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            if(LfsrMethod != null)
            {
                LfsrMethod.IsStopped = true;
            }
        }

        public void GenerateKey()
        {
            if(LfsrMethod==null)
            {
                return;
            }
            while (!LfsrMethod.IsStopped)
            {
                bool row = LfsrMethod.AddRow();
                if (row)
                {
                    Result.Text += "1";
                }
                else
                {
                    Result.Text += "0";
                }
                return;
            }
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
