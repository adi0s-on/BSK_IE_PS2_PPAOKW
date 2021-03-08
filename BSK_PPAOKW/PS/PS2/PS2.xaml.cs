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
    /// Logika interakcji dla klasy PS2.xaml
    /// </summary>
    public partial class PS2 : UserControl
    {
        public PS2()
        {
            InitializeComponent();
        }

        private void PS2ButtonClick(object sender, RoutedEventArgs e)
        {
            int indexTopMenuCursorPS2 = int.Parse(((Button)e.Source).Uid);
            TopMenuCursorPS2.Margin = new Thickness(0 + (413.3 * indexTopMenuCursorPS2), 35, 0, 0);

            switch (indexTopMenuCursorPS2)
            {
                case 0:
                    {
                        WorkFieldPS2.Children.Clear();
                        WorkFieldPS2.Children.Add(new PS2MatrixConversionPS2());
                        break;
                    }
                case 1:
                    {
                        WorkFieldPS2.Children.Clear();
                        WorkFieldPS2.Children.Add(new PS2CaesarsCipher());
                        break;
                    }
                case 2:
                    {
                        WorkFieldPS2.Children.Clear();
                        WorkFieldPS2.Children.Add(new PS2VigenereCipher());
                        break;
                    }
            }
        }
    }
}
