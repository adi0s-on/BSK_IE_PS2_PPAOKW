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
    public partial class PS4 : UserControl
    {
        public PS4()
        {
            InitializeComponent();
            WorkFieldPS4.Children.Clear();
            WorkFieldPS4.Children.Add(new PS4DES());
        }
    }
}
