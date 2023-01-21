using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Pizzeria.Models
{
    public class ListaPizze
    {
        public int IdPizza { get; set; }
        public string Nome { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Prezzo { get; set; }
        public int Quantita { get; set; }
        [DataType(DataType.Currency)]
        public decimal Totale { get; set; }

    }
}