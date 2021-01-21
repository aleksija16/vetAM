using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetAM.DomainModel
{
    class Vlasnik
    {
        public int idVlasnik { get; set; }
        public string ime { get; set; }
        public string prezime { get; set; }
        public string telefon { get; set; }
        public string mail { get; set; }
        public List<Zivotinja> zivotinje { get; set; }
    }
}
