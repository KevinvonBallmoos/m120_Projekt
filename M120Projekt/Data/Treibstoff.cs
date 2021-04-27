using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace M120Projekt.Data
{
    public class Treibstoff
    {
        [Key]
        public Int64 TreibstoffId { get; set; }
        public String Name { get; set; }

        public Treibstoff(String Name)
        {
            
            this.Name = Name;
        }
        public Treibstoff()
        {

        }

        public Int64 TreibstoffErstellen()
        {
            using (var db = new Context())
            {
                db.Treibstoff.Add(this);
                db.SaveChanges();
                return this.TreibstoffId;
            }
        }

        public static List<Treibstoff> LesenAlle()
        {
            using (var db = new Context())
            {
                return (from record in db.Treibstoff select record).ToList();
            }
        }

        public static Treibstoff LesenID(Int64 klasseAId)
        {
            using (var db = new Context())
            {
                return (from record in db.Treibstoff where record.TreibstoffId == klasseAId select record).FirstOrDefault();
            }
        }

    }
}
