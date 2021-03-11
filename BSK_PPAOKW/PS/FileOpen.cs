using System.Windows.Controls;

namespace BSK_PPAOKW.PS
{
    public static class FileOpen
    {
        public static void Open_file(TextBlock textblock)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            bool? response = openFileDialog.ShowDialog();
            if (response == true)
            {
                textblock.Text = openFileDialog.FileName;
            }
        }
    }
}
