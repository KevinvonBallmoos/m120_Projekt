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

namespace M120Projekt
{
    /// <summary>
    /// Interaktionslogik für Listenansicht.xaml
    /// </summary>
    public partial class Listenansicht : UserControl
    {
        ScrollViewer platzhalter;
        public Listenansicht(ScrollViewer platzhalter)
        {   
            InitializeComponent();
            this.platzhalter = platzhalter;

            datagridliste.ItemsSource = Data.Auto.LesenAlle();

            // Optionen
            // Neu anfügen
            datagridliste.CanUserAddRows = false;
            // Sortieren
            datagridliste.CanUserSortColumns = true;
            // Anzeigemodus
            datagridliste.IsReadOnly = true;
            // Auswahlvarianten
            datagridliste.SelectionMode = DataGridSelectionMode.Single;
            datagridliste.SelectionMode = DataGridSelectionMode.Extended;
        }

        
        private void datagridliste_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (datagridliste.SelectedItem != null)
            {
                Data.Auto auswahl = (Data.Auto)datagridliste.SelectedItem;
                platzhalter.Content = new NeuesAuto (auswahl.AutoId);
            }
        }    
        
        private void Listenansicht_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                if (datagridliste.SelectedItem != null)
                {
                    Data.Auto auswahl = (Data.Auto)datagridliste.SelectedItem;
                    platzhalter.Content = new NeuesAuto(auswahl.AutoId);
                }
            }

            if(e.Key == Key.F1) {
                Tastaturkuerzel tastatur = new Tastaturkuerzel();      
            }
        }
    }
}
