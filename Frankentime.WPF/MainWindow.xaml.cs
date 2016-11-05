using System.Windows;
using System.Windows.Input;

namespace Frankentime.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //InitializeComponent();
        }

        private void TitleBar_Mousedown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }


    }
}
