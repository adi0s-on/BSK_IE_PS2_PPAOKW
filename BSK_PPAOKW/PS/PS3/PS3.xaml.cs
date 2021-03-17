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
using System.Windows.Shapes;

namespace BSK_PPAOKW.PS
{
    /// <summary>
    /// Logika interakcji dla klasy PS3.xaml
    /// </summary>
    public partial class PS3 : UserControl
    {
        public PS3()
        {
            InitializeComponent();
        }

        private void PS3ButtonClick(object sender, RoutedEventArgs e)
        {
            int indexTopMenuCursorPS3 = int.Parse(((Button)e.Source).Uid);
            TopMenuCursorPS3.Margin = new Thickness(0 + (620 * indexTopMenuCursorPS3), 35, 0, 0);

            switch (indexTopMenuCursorPS3)
            {
                case 0:
                    {
                        WorkFieldPS3.Children.Clear();
                        WorkFieldPS3.Children.Add(new PS3PseudorandomNumber());
                        break;
                    }
                case 1:
                    { 
                        WorkFieldPS3.Children.Clear();
                        WorkFieldPS3.Children.Add(new PS3StreamCipher());
                        break;
                    }
            }
        }
    }
}
