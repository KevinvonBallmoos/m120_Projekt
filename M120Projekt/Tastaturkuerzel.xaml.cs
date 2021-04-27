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
using System.Windows.Shapes;

namespace M120Projekt
{
    /// <summary>
    /// Interaktionslogik für Tastaturkuerzel.xaml
    /// </summary>
    public partial class Tastaturkuerzel : Window
    {
        public Tastaturkuerzel()
        {
            InitializeComponent();
            lblText.Content = "CTRL + N = Neues Auto anlegen \n" +
                "CTRL + S = Auto speichern \n" +
                "CTRL + L = Auto löschen \n" +
                "CTRL + B = Abbruch \n" +
                "Space = Objekt auswählen in Listenansicht \n" +
                "Esape = Tastaturkürzel-Fenster schliessen \n" + 
                "F1 = Hilfe Fenster";
            Show();
        }

        private void btnBeenden_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Tastatur_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Escape)
            {
                Close();
            }
        }
    }
}
