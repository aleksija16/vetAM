using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetAM.DomainModel
{
    class Pregled
    {
        public int idPregled { get; set; }
        public string datum { get; set; }
        public string dijagnoza { get; set; }
        public string terapija { get; set; }
        public double cenaPregleda { get; set; }
        public Zivotinja zivotinja { get; set; }
        public Veterinar veterinar { get; set; }
    }
}
