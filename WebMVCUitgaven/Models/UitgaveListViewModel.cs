using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVCUitgaven.Models
{
    public class UitgaveListViewModel
    {
        public string Pijl { get; set; }
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        
        public string Beschrijving { get; set; }
        public string Soort { get; set; }
        public int Bedrag { get; set; }
        //public int Uitgave { get; set; }
        public string PhotoUrl { get; set; }
        public string PhotoUrl2 { get; set; }
        public string Koper { get; set; }
        public string Extra { get; set; }
    }
}
