using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVCUitgaven.Domain
{
    public class Uitgave
    {
        public int Id { set; get; }

        public DateTime Datum { get; set; }

        public string Beschrijving { get; set; }
        public string Soort { get; set; }
        public int Bedrag { get; set; }

        public string PhotoUrl { get; set; }
        public string PhotoUrl2 { get; set; }
        public string Koper { get; set; }
        public string Extra { get; set; }
    }
}
