using System;
using System.Diagnostics;

namespace M120Projekt
{
    static class APIDemo
    {
        #region KlasseA
        // Create
        public static void DemoACreate()
        {
            Debug.Print("--- DemoACreate ---");
            // KlasseA
            Data.Auto klasseA1 = new Data.Auto();
            klasseA1.Automarke = "Artikel 1";
            klasseA1.Erstzulassung = DateTime.Today;
            klasseA1.Bild = "image.png";
            klasseA1.CHFproKm = 10;
            klasseA1.Motorisierung = "2.0TFSI";
            klasseA1.Treibstoff = "Benzin";
            Int64 klasseA1Id = klasseA1.Erstellen();
            Debug.Print("Artikel erstellt mit Id:" + klasseA1Id);
        }
        public static void DemoACreateKurz()
        {
            Data.Auto klasseA2 = new Data.Auto { Automarke = "Artikel 2", Sitzheizung = true, Erstzulassung = DateTime.Today, Bild = "noimage.png", CHFproKm = 10, Motorisierung = "2.0FSI", Treibstoff = "Benzin", Vermietet = true };
            Int64 klasseA2Id = klasseA2.Erstellen();
            Debug.Print("Artikel erstellt mit Id:" + klasseA2Id);
        }

        // Read
        public static void DemoARead()
        {
            Debug.Print("--- DemoARead ---");
            // Demo liest alle
            foreach (Data.Auto klasseA in Data.Auto.LesenAlle())
            {
                Debug.Print("Artikel Id:" + klasseA.AutoId + " Name:" + klasseA.Automarke);
            }
        }
        // Update
        public static void DemoAUpdate()
        {
            Debug.Print("--- DemoAUpdate ---");
            // KlasseA ändert Attribute
            Data.Auto klasseA1 = Data.Auto.LesenID(1);
            klasseA1.Automarke = "Artikel 1 nach Update";
            klasseA1.Aktualisieren();
        }
        // Delete
        public static void DemoADelete()
        {
            Debug.Print("--- DemoADelete ---");
            Data.Auto.LesenID(2).Loeschen();
            Debug.Print("Artikel mit Id 2 gelöscht");
        }
        #endregion
    }
}
