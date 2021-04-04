using BSK_PPAOKW.PS;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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
                case 1:
                    {
                        MainGrid.Children.Clear();
                        MainGrid.Children.Add(new PS2());
                        break;
                    }
                case 2:
                    {
                        MainGrid.Children.Clear();
                        MainGrid.Children.Add(new PS3());
                        break;
                    }
                case 3:
                    {
                        MainGrid.Children.Clear();
                        MainGrid.Children.Add(new PS4());
                        break;
                    }
            }
        }

        private void MoveMenuCursor(int menuIndex)
        {
            TransitionSlide.OnApplyTemplate();
            MenuCursor.Margin = new Thickness(0, (50+(60*menuIndex)),0,0);
        }


    }
}
