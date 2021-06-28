using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVCUitgaven.Models
{
    public class UitgaveDeleteViewModel
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }

        public string Beschrijving { get; set; }
        public string Soort { get; set; }
        public int Bedrag { get; set; }

    }
}
