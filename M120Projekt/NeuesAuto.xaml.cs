using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace M120Projekt
{
    /// <summary>
    /// Interaktionslogik für NeuesAuto.xaml
    /// </summary>
    public partial class NeuesAuto : UserControl
    {
        public NeuesAuto()
        {
            InitializeComponent();
            //ErstelleTreibstoffDaten();
            
            SetzeZustandLeer();
            cmbxTreibstoff.ItemsSource = Data.Treibstoff.LesenAlle();
            cmbxTreibstoff.DisplayMemberPath = "Name";
            cmbxTreibstoff.SelectedValuePath = "TreibstoffId";
            
            
        }
        public NeuesAuto(long autoId)
        {
           
            InitializeComponent();

            SetzeZustandLeer();
            cmbxTreibstoff.ItemsSource = Data.Treibstoff.LesenAlle();
            cmbxTreibstoff.DisplayMemberPath = "Name";
            cmbxTreibstoff.SelectedValuePath = "TreibstoffId";
            LadeDaten(autoId);

            lblLadeMeldung.Foreground = Brushes.Red;
            lblLadeMeldung.Content = "Auto mit der ID " + autoId + " wurde geladen";

            System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Interval = new TimeSpan(0,0,3);
            timer.Start();
        }
      
        private void Timer_Tick(object sender , EventArgs e)
        {
            lblLadeMeldung.Content = "";
        }
        private void NeuesAuto_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.N && btnNeu.IsEnabled && (Keyboard.IsKeyDown(Key.LeftCtrl) 
                || Keyboard.IsKeyDown(Key.RightCtrl)))
            {
                btnNeu_Click(sender, e);
            }

            else if(e.Key == Key.S && btnSpeichern.IsEnabled && (Keyboard.IsKeyDown(Key.LeftCtrl) 
                || Keyboard.IsKeyDown(Key.Right)))
            {
                btnSpeichern_Click(sender, e);
            }

            else if(e.Key == Key.L && btnLoeschen.IsEnabled && (Keyboard.IsKeyDown(Key.LeftCtrl)
                || Keyboard.IsKeyDown(Key.Right)))
            {
                btnLoeschen_Click(sender, e);
            }
            else if (e.Key == Key.B && btnAbbrechen.IsEnabled && (Keyboard.IsKeyDown(Key.LeftCtrl)
                || Keyboard.IsKeyDown(Key.Right)))
            {
                btnAbbrechen_Click(sender, e);
            }

            else if (e.Key == Key.F1)
            {
                Tastaturkuerzel tastatur = new Tastaturkuerzel();
               
            }
        }

        private Data.Auto auto;

        private Zustand zustand
        {
            get; set;
        }

        public void LadeDaten(long autoId)
        {
            auto = Data.Auto.LesenID(autoId);

            txtAutomarke.Text = auto.Automarke;
            txtMotorisierung.Text = auto.Motorisierung;
            txtPreis.Text = auto.CHFproKm.ToString();
            chkbxHatSitzheizung.IsChecked = auto.HatSitzheizung;
            chkbxIstVermietet.IsChecked = auto.IstVermietet;
            dtprDatum.SelectedDate = auto.Erstzulassung;
            cmbxTreibstoff.SelectedValue = auto.TreibstoffId;

            btnNeu.IsEnabled = true;
            btnAbbrechen.IsEnabled = false;

            SetzeZustandAnzeige();
        }

        private void ErstelleDatenSatz()
        {
            Data.Auto klasseAuto = new Data.Auto();
            Data.Treibstoff treibstoff = new Data.Treibstoff();
            treibstoff = Data.Treibstoff.LesenID(treibstoff.TreibstoffId);


            klasseAuto.Automarke = txtAutomarke.Text;
            klasseAuto.Motorisierung = txtMotorisierung.Text;
            klasseAuto.CHFproKm = Convert.ToDouble(txtPreis.Text);
            klasseAuto.HatSitzheizung = Convert.ToBoolean(chkbxHatSitzheizung.IsChecked);
            klasseAuto.IstVermietet = Convert.ToBoolean(chkbxIstVermietet.IsChecked);
            klasseAuto.Erstzulassung = Convert.ToDateTime(dtprDatum.SelectedDate);
            klasseAuto.treibstoff = (Data.Treibstoff)cmbxTreibstoff.SelectedItem;


            Int64 klasseAutoId = klasseAuto.Erstellen();
            auto = klasseAuto;
        }

        private void AktualisiereDatenSatz()
        {
            auto.Automarke = txtAutomarke.Text;
            auto.Motorisierung = txtMotorisierung.Text;
            auto.CHFproKm = Convert.ToDouble(txtPreis.Text);
            auto.HatSitzheizung = Convert.ToBoolean(chkbxHatSitzheizung.IsChecked);
            auto.IstVermietet = Convert.ToBoolean(chkbxIstVermietet.IsChecked);
            auto.Erstzulassung = Convert.ToDateTime(dtprDatum.SelectedDate);
            auto.treibstoff = (Data.Treibstoff)cmbxTreibstoff.SelectedItem;
            auto.TreibstoffId = (long)cmbxTreibstoff.SelectedValue;
            auto.Aktualisieren();
        }


        private void ErstelleTreibstoffDaten()
        {
            Data.Treibstoff treibstoff = new Data.Treibstoff();

            treibstoff.TreibstoffId = 1;
            treibstoff.Name = "Benzin";
            Int64 treibstoffId1 = treibstoff.TreibstoffErstellen();

            treibstoff.TreibstoffId = 2;
            treibstoff.Name = "Diesel";
            Int64 treibstoffId2 = treibstoff.TreibstoffErstellen();

            treibstoff.TreibstoffId = 3;
            treibstoff.Name = "Elektro";
            Int64 treibstoffId3 = treibstoff.TreibstoffErstellen();
        }
      

        private enum Zustand
        {
            Leer,
            Anzeige,
            Neu,
            Veraendert,
            Ungespeichert
        }

        
        private bool[] benoetigt = { false, false, false, false, false };
        private void SetzeZustandLeer()
        {
            zustand = Zustand.Leer;

            //Buttons enabled
            btnNeu.IsEnabled = true;
            btnLoeschen.IsEnabled = false;


            //Buttons disabled
            btnSpeichern.IsEnabled = false;
            btnSuche.IsEnabled = false;
            btnAbbrechen.IsEnabled = false;

            //Fields disabled
            txtSuche.IsEnabled = false;

            //Fields disabled
            txtAutomarke.IsEnabled = false;
            txtMotorisierung.IsEnabled = false;
            txtPreis.IsEnabled = false;
            chkbxHatSitzheizung.IsEnabled = false;
            chkbxIstVermietet.IsEnabled = false;
            dtprDatum.IsEnabled = false;
            cmbxTreibstoff.IsEnabled = false;
            
        }

        private void SetzeZustandVeraendert()
        {

            zustand = Zustand.Veraendert;

            //Buttons enabled
            btnAbbrechen.IsEnabled = true;
            btnSpeichern.IsEnabled = true;
            btnLoeschen.IsEnabled = true;

            //Buttons disabled
            btnNeu.IsEnabled = false;
            btnSuche.IsEnabled = false;

            //Fields disabled
            txtSuche.IsEnabled = false;

            //Fields enabled
            txtAutomarke.IsEnabled = true;
            txtMotorisierung.IsEnabled = true;
            txtPreis.IsEnabled = true;
            chkbxHatSitzheizung.IsEnabled = true;
            chkbxIstVermietet.IsEnabled = true;
            dtprDatum.IsEnabled = true;
            cmbxTreibstoff.IsEnabled = true;
        
        }

        private void SetzeZustandUngespeichert()
        {
            zustand = Zustand.Ungespeichert;

            //Buttons enabled
            btnAbbrechen.IsEnabled = true;
            btnSpeichern.IsEnabled = true;

            //Buttons disabled
            btnLoeschen.IsEnabled = false;
            btnNeu.IsEnabled = false;
            btnSuche.IsEnabled = false;

            //Fields disabled
            txtSuche.IsEnabled = false;

            //Fields enabled
            txtAutomarke.IsEnabled = true;
            txtMotorisierung.IsEnabled = true;
            txtPreis.IsEnabled = true;
            chkbxHatSitzheizung.IsEnabled = true;
            chkbxIstVermietet.IsEnabled = true;
            dtprDatum.IsEnabled = true;
            cmbxTreibstoff.IsEnabled = true;
       
        }



        private void SetzeZustandNeu()
        {
            zustand = Zustand.Neu;

            //Buttons disabled
            btnAbbrechen.IsEnabled = false;
            btnLoeschen.IsEnabled = false;
            btnNeu.IsEnabled = false;
            btnSuche.IsEnabled = false;
            btnSpeichern.IsEnabled = false;

            //Fields disabled
            txtSuche.IsEnabled = false;

            //Fields enabled
            txtAutomarke.IsEnabled = true;
            txtMotorisierung.IsEnabled = true;
            txtPreis.IsEnabled = true;
            chkbxHatSitzheizung.IsEnabled = true;
            chkbxIstVermietet.IsEnabled = true;
            dtprDatum.IsEnabled = true;
            cmbxTreibstoff.IsEnabled = true;
          
        }


        private void SetzeZustandAnzeige()
        {
            zustand = Zustand.Anzeige;

            //Buttons enabled
            btnNeu.IsEnabled = true;
            btnLoeschen.IsEnabled = true;
            btnSuche.IsEnabled = true;

            //Buttons disabled
            btnAbbrechen.IsEnabled = false;
            btnSpeichern.IsEnabled = false;


            //Fields disabled
            txtAutomarke.IsEnabled = true;
            txtMotorisierung.IsEnabled = true;
            txtPreis.IsEnabled = true;
            chkbxHatSitzheizung.IsEnabled = true;
            chkbxIstVermietet.IsEnabled = true;
            dtprDatum.IsEnabled = true;
            cmbxTreibstoff.IsEnabled = true;
            txtSuche.IsEnabled = true;
         
        }


        //When a new
        private void btnNeu_Click(object sender, RoutedEventArgs e)
        {
            SetzeZustandNeu();
            txtAutomarke.Clear();
            txtMotorisierung.Clear();
            txtPreis.Clear();
            chkbxHatSitzheizung.IsChecked = false;
            chkbxIstVermietet.IsChecked = false;
            cmbxTreibstoff.SelectedValue = null;
            dtprDatum.SelectedDate = null;
        }

        private void AutomarkeIsChanged(object sender, TextChangedEventArgs e)
        {
            switch(zustand)
            {
                case Zustand.Neu:
                    SetzeZustandUngespeichert();
                    break;
                case Zustand.Anzeige:
                    SetzeZustandVeraendert();
                    break;
            }
          
        }

        private void MotorIsChanged(object sender, TextChangedEventArgs e)
        {
            switch (zustand)
            {
                case Zustand.Neu:
                    SetzeZustandUngespeichert();
                    break;
                case Zustand.Anzeige:
                    SetzeZustandVeraendert();
                    break;
            }
        }

        private void PreisIsChanged(object sender, TextChangedEventArgs e)
        {
            switch (zustand)
            {
                case Zustand.Neu:
                    SetzeZustandUngespeichert();
                    break;
                case Zustand.Anzeige:
                    SetzeZustandVeraendert();
                    break;
            }
        }

        private void TreibstoffChanged(object sender, SelectionChangedEventArgs e)
        {
            benoetigt[0] = true;
            switch (zustand)
            {
                case Zustand.Neu:
                    SetzeZustandUngespeichert();
                   
                    break;
                case Zustand.Anzeige:
                    SetzeZustandVeraendert();
                    benoetigt[0] = true;
                    break;
            }
        }

        private void VermietetIsChanged(object sender, RoutedEventArgs e)
        {
            switch (zustand)
            {
                case Zustand.Neu:
                    SetzeZustandUngespeichert();
                    break;
                case Zustand.Anzeige:
                    SetzeZustandVeraendert();
                    break;
            }
        }

        private void SitzheizungIsChanged(object sender, RoutedEventArgs e)
        {
            switch (zustand)
            {
                case Zustand.Neu:                    
                    SetzeZustandUngespeichert();
                    break;
                case Zustand.Anzeige:
                    SetzeZustandVeraendert();
                    break;
            }
        }

        private void DateIsChanged(object sender, SelectionChangedEventArgs e)
        {
            benoetigt[1] = true;
            lblFehlermeldung5.Content = "";
            switch (zustand)
            {
                case Zustand.Neu:                  
                    SetzeZustandUngespeichert();                    
                    break;
                case Zustand.Anzeige:
                    SetzeZustandVeraendert();  
                    break;
            }
        }

        System.Text.RegularExpressions.Regex regex;

        private void IstAutomarkeGueltig(object sender, RoutedEventArgs e)
        {
            regex = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z0-9\s]{1,30}$");
            System.Text.RegularExpressions.Match match = regex.Match(txtAutomarke.Text);

            if (match.Success)
            {
                lblFehlerMeldung1.Visibility = Visibility.Hidden;
                txtAutomarke.BorderThickness = new Thickness(3);
                txtAutomarke.BorderBrush = Brushes.LightGreen;
                benoetigt[2] = true;
            }
            else
            {
                txtAutomarke.BorderThickness = new Thickness(3);
                txtAutomarke.BorderBrush = Brushes.DarkRed;
                benoetigt[2] = false;
                lblFehlerMeldung1.Visibility = Visibility.Visible;
                lblFehlerMeldung1.Content = "Nur Buchstaben von A - Z und und Ziffen von 0 - 9 Leerzeichen erlaubt";

            }
        }

        private void IstMotorGueltig(object sender, RoutedEventArgs e)
        {
            regex = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z0-9/.\s]{1,30}$");
            System.Text.RegularExpressions.Match match = regex.Match(txtMotorisierung.Text);
           

            if (match.Success)
            {
                lblFehlerMeldung2.Visibility = Visibility.Hidden;
                txtMotorisierung.BorderThickness = new Thickness(3);
                txtMotorisierung.BorderBrush = Brushes.LightGreen;
                benoetigt[3] = true;
            }
            else
            {
                txtMotorisierung.BorderThickness = new Thickness(3);
                txtMotorisierung.BorderBrush = Brushes.DarkRed;
                benoetigt[3] = false;
                lblFehlerMeldung2.Visibility = Visibility.Visible;
                lblFehlerMeldung2.Content = "Nur Ziffern von 0-9, Buchstaben von A -Z, Punkte und Leerzeichen erlaubt";
            }
        }



        private void IstPreisGueltig(object sender, RoutedEventArgs e)
        {
            regex = new System.Text.RegularExpressions.Regex(@"^[0-9/.\s]{1,5}$");
            System.Text.RegularExpressions.Match match = regex.Match(txtPreis.Text);

            if (match.Success)
            {
                lblFehlerMeldung3.Visibility = Visibility.Hidden;
                txtPreis.BorderThickness = new Thickness(3);
                txtPreis.BorderBrush = Brushes.LightGreen;
                benoetigt[4] = true;
            }
            else
            {
                txtMotorisierung.BorderThickness = new Thickness(3);
                txtPreis.BorderBrush = Brushes.DarkRed;
                benoetigt[4] = false;
                lblFehlerMeldung3.Visibility = Visibility.Visible;
                lblFehlerMeldung3.Content = "Nur Ziffern von 0-9 und Leerzeichen erlaubt";
            }
        }

        private void btnAbbrechen_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Wollen sie wirklich abbrechen ?", "Abbrechen ?", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                txtAutomarke.Clear();
                txtMotorisierung.Clear();
                txtPreis.Clear();
                chkbxHatSitzheizung.IsChecked = false;
                chkbxIstVermietet.IsChecked = false;
                cmbxTreibstoff.SelectedValue = null;
                dtprDatum.SelectedDate = null;
                txtMotorisierung.BorderBrush = Brushes.Black;
                txtAutomarke.BorderBrush = Brushes.Black;
                txtPreis.BorderBrush = Brushes.Black;
                txtMotorisierung.BorderThickness = new Thickness(1);
                txtAutomarke.BorderThickness = new Thickness(1);
                txtPreis.BorderThickness = new Thickness(1);
                SetzeZustandLeer();
            }
        }

        
        private void btnLoeschen_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Wollen sie den Eintrag wirklich löschen ?", "Löschen ?", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                Data.Auto.LesenID(auto.AutoId).Loeschen();

                txtAutomarke.Clear();
                txtMotorisierung.Clear();
                txtPreis.Clear();
                chkbxHatSitzheizung.IsChecked = false;
                chkbxIstVermietet.IsChecked = false;
                cmbxTreibstoff.SelectedValue = null;
                dtprDatum.SelectedDate = null;
                txtMotorisierung.BorderBrush = Brushes.Black;
                txtAutomarke.BorderBrush = Brushes.Black;
                txtPreis.BorderBrush = Brushes.Black;
                txtMotorisierung.BorderThickness = new Thickness(1);
                txtAutomarke.BorderThickness = new Thickness(1);
                txtPreis.BorderThickness = new Thickness(1);

                SetzeZustandLeer();

            }
            else
            {
                SetzeZustandAnzeige();
            }
        }

        private void btnSpeichern_Click(object sender, RoutedEventArgs e)
        {
            if(benoetigt[0] == false)
            {
                lblFehlermeldung4.Content = "Bitte einen Treibstoff auswählen";
            }
            if(benoetigt[1] == false)
            {
                lblFehlermeldung5.Content = "Bitte ein Datum auswählen";
            }
            
            int count = 0;
            for (int i = 0; i < benoetigt.Length; i++)
            {
                if (benoetigt[i])
                {
                    count++;
                }

            }
            if (count == 5)
            {
                lblFehlermeldung4.Content = "";
                MessageBoxResult messageBoxResult = MessageBox.Show("Wollen Sie speichern ?", "Speichern", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {

                    txtMotorisierung.BorderBrush = Brushes.Black;
                    txtAutomarke.BorderBrush = Brushes.Black;
                    txtPreis.BorderBrush = Brushes.Black;
                    txtMotorisierung.BorderThickness = new Thickness(1);
                    txtAutomarke.BorderThickness = new Thickness(1);
                    txtPreis.BorderThickness = new Thickness(1);
                    
                    switch (zustand)
                    {
                        case Zustand.Ungespeichert:
                            SetzeZustandAnzeige();
                            ErstelleDatenSatz();
                            MessageBox.Show("Die Daten wurden gespeichert");
                            break;

                        case Zustand.Veraendert:
                            AktualisiereDatenSatz();
                            MessageBox.Show("Die Daten wurden aktualisiert");
                            break;
                    }
                }
            }
            else
            {
                MessageBox.Show("Speichern nur möglich wenn alle mit einem * markierten \n " +
                    "Feld ausgefüllt sind");
            }
        }
    }
}