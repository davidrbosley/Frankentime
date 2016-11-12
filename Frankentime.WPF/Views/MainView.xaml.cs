using System.Windows;
using System.Windows.Input;

namespace Frankentime.WPF
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView
    {


        private void TitleBar_Mousedown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CheckForUpdates_Clicked(object sender, RoutedEventArgs e)
        {
            var checkView = new CheckForUpdatesView();
            checkView.ShowDialog();
        }
    }
}
