namespace Pizzeria.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DettagliOrdini")]
    public partial class DettagliOrdini
    {
        [Key]
        public int IdDettaglioOrdine { get; set; }

        public int? Quantita { get; set; }

        public int? IdPizza { get; set; }

        public int? IdOrdine { get; set; }

        public virtual Ordini Ordini { get; set; }

        public virtual Pizze Pizze { get; set; }
    }
}
