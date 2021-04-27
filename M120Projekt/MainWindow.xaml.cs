using System.Windows;

namespace M120Projekt
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
       
        private void btnBeenden_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnAlleAutos_Click(object sender, RoutedEventArgs e)
        {
            scrlvrplatzhalter.Content = new Listenansicht(scrlvrplatzhalter);
        }

        private void btnAuto_Click(object sender, RoutedEventArgs e)
        {
            scrlvrplatzhalter.Content = new NeuesAuto();
        }

        private void MainWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            
        }

        

    }
}
