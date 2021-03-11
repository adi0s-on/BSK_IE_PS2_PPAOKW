using System.Windows;
using System.Windows.Controls;

namespace BSK_PPAOKW.PS
{
    /// <summary>
    /// Logika interakcji dla klasy PS1.xaml
    /// </summary>
    public partial class PS1 : UserControl
    {
        public PS1()
        {
            InitializeComponent();
        }

        private void PS1ButtonClick(object sender, RoutedEventArgs e)
        {
            int indexTopMenuCursorPS1 = int.Parse(((Button)e.Source).Uid);
            TopMenuCursorPS1.Margin = new Thickness(0+ (620 * indexTopMenuCursorPS1), 35, 0, 0);

            switch(indexTopMenuCursorPS1)
            {
                case 0:
                    {
                        WorkFieldPS1.Children.Clear();
                        WorkFieldPS1.Children.Add(new PS1RailFence());
                        break;
                    }
                case 1:
                    {
                        WorkFieldPS1.Children.Clear();
                        WorkFieldPS1.Children.Add(new PS1MatrixConversions());
                        break;
                    }
            }
        }
    }
}
