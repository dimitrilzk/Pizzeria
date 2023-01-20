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
            public int Quantita { get; set; }

    }
}