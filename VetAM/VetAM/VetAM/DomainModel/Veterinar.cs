using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetAM.DomainModel
{
    class Veterinar
    {
        public int idVeterinar { get; set; }
        public string ime { get; set; }
        public string prezime { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string adresa { get; set; }
        public string titula { get; set; }
        public string telefon { get; set; }
        public string mail { get; set; }
        public List<Pregled> pregledi { get; set; }
    }
}
