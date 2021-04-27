using System;
using System.Diagnostics;

namespace M120Projekt
{
    static class APIDemo
    {
        #region KlasseA
        // Create
        public static void Create()
        {
            Data.Treibstoff treibstoff = new Data.Treibstoff();
            // KlasseA
            Data.Auto klasseA1 = new Data.Auto();
            klasseA1.Automarke = "Audi";
            klasseA1.Erstzulassung = DateTime.Today; 
            klasseA1.CHFproKm = 10;
            klasseA1.Motorisierung = "2.0TFSI";
            //klasseA1.treibstoff = treibstoff.Treibstoffid;
            klasseA1.HatSitzheizung = true;
            Int64 klasseA1Id = klasseA1.Erstellen();
            Debug.Print("Auto erstellt mit Id:" + klasseA1Id);
        }

        /*
        public static void DemoACreateKurz()
        {
            Data.Auto klasseA2 = new Data.Auto { Automarke = "Artikel 2", HatSitzheizung = true, Erstzulassung = DateTime.Today, CHFproKm = 10, Motorisierung = "2.0FSI", IstVermietet = true };
            Int64 klasseA2Id = klasseA2.Erstellen();
            Debug.Print("Artikel erstellt mit Id:" + klasseA2Id);
        }
        */

        // Read
        public static void Read()
        {
            Debug.Print("--- Read ---");
            // Demo liest alle
            foreach (Data.Auto klasseA in Data.Auto.LesenAlle())
            {
                Debug.Print("Artikel Id:" + klasseA.AutoId + " Name:" + klasseA.Automarke);
            }
        }
        // Update
        public static void Update()
        {
            Debug.Print("--- DemoAUpdate ---");
            // KlasseA ändert Attribute
            Data.Auto klasseA1 = Data.Auto.LesenID(1);
            klasseA1.Automarke = "Artikel 1 nach Update";
            klasseA1.Aktualisieren();
        }
        // Delete
        public static void Delete()
        {
            Debug.Print("--- DemoADelete ---");
            Data.Auto.LesenID(2).Loeschen();
            Debug.Print("Artikel mit Id 2 gelöscht");
        }
        #endregion
    }
}
