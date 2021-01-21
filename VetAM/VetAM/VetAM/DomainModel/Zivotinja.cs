using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetAM.DomainModel
{
    class Zivotinja
    {
        public int idZivotinja { get; set; }
        public string vrsta { get; set; }
        public string ime { get; set; }
        public int starost { get; set; }
        public string opis { get; set; }
        public string mikrocip { get; set; }
        public Vlasnik vlasnik { get; set; }
        public List<Pregled> pregledi { get; set; }
    }
}
