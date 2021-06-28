using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVCUitgaven.Models
{
    public class UitgaveCreateViewModel
    {
        public DateTime Datum { get; set; }
        [Required]
        public string Beschrijving { get; set; }
        [Required]
        public int Bedrag { get; set; }
        public string Soort { get; set; }
        public IEnumerable<SelectListItem> Soorten { get; set; } = new List<SelectListItem>()
        {
            new SelectListItem("Inkomsten", "Inkomsten"),
            new SelectListItem("Uitgave", "Uitgave")
        };
        [DisplayName("Foto")]
        public IFormFile Photo { get; set; }
        [DisplayName("Foto2")]
        public IFormFile Photo2 { get; set; }
        //public string PhotoUrl { get; set; }
        public string Koper { get; set; }
        public string Extra { get; set; }
    }
}
