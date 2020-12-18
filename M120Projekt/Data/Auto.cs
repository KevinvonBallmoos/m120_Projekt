using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace M120Projekt.Data
{
    public class Auto
    {
        #region Datenbankschicht
        [Key]
        public Int64 AutoId { get; set; }
        [Required]
        public String Automarke { get; set; }
        [Required]
        public DateTime Erstzulassung { get; set; }
        [Required]
        public Boolean Sitzheizung { get; set; }
        [Required]
        public int CHFproKm { get; set; }
        [Required]
        public String Motorisierung { get; set; }
        [Required]
        public String Treibstoff { get; set; }
        [Required]
        public Boolean Vermietet { get; set; }
        [Required]
        public String Bild { get; set; }
        #endregion
        #region Applikationsschicht
        public Auto() { }
        [NotMapped]
        public String BerechnetesAttribut
        {
            get
            {
                return "Im Getter kann Code eingefügt werden für berechnete Attribute";
            }
        }
        public static List<Auto> LesenAlle()
        {
            using (var db = new Context())
            {
                return (from record in db.Auto select record).ToList();
            }
        }
        public static Auto LesenID(Int64 klasseAId)
        {
            using (var db = new Context())
            {
                return (from record in db.Auto where record.AutoId == klasseAId select record).FirstOrDefault();
            }
        }
        public static List<Auto> LesenAttributGleich(String suchbegriff)
        {
            using (var db = new Context())
            {
                return (from record in db.Auto where record.Automarke == suchbegriff select record).ToList();
            }
        }
        public static List<Auto> LesenAttributWie(String suchbegriff)
        {
            using (var db = new Context())
            {
                return (from record in db.Auto where record.Automarke.Contains(suchbegriff) select record).ToList();
            }
        }
        public Int64 Erstellen()
        {
            if (this.Automarke == null || this.Automarke == "") this.Automarke = "leer";
            if (this.Erstzulassung == null) this.Erstzulassung = DateTime.MinValue;
            using (var db = new Context())
            {
                db.Auto.Add(this);
                db.SaveChanges();
                return this.AutoId;
            }
        }
        public Int64 Aktualisieren()
        {
            using (var db = new Context())
            {
                db.Entry(this).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return this.AutoId;
            }
        }
        public void Loeschen()
        {
            using (var db = new Context())
            {
                db.Entry(this).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
        }
        public override string ToString()
        {
            return AutoId.ToString(); // Für bessere Coded UI Test Erkennung
        }
        #endregion
    }
}
