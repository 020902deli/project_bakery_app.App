using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace project_bakery_app.Models
{
    public class Baker
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }

        public string BakerDetails
        {
            get {return Nume + " "+Prenume;}
        }
        [OneToMany]
        public List<OrderList> OrderLists { get; set; }
    }
}
