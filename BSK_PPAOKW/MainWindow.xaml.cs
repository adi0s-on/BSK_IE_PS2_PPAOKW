using BSK_PPAOKW.PS;
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

namespace BSK_PPAOKW
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.FontFamily = new FontFamily("Century Gothic");
        }

        private void ShutdownThisApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void MinimizeThisApp(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int menuIndex = ListViewMenu.SelectedIndex;
            MoveMenuCursor(menuIndex);

            switch(menuIndex)
            {
                default:
                    {
                        MainGrid.Children.Clear();
                        MainGrid.Children.Add(new WIP());
                        break;

                    }
                case 0:
                    {
                        MainGrid.Children.Clear();
                        MainGrid.Children.Add(new PS1());
                        break;
                    }
            }
        }

        private void MoveMenuCursor(int menuIndex)
        {
            TransitionSlide.OnApplyTemplate();
            MenuCursor.Margin = new Thickness(0, (50+(60*menuIndex)),0,0);
        }

        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {

        }
        private void ListViewItem_Selected_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
