using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Pizzeria.Models
{
    public partial class ModelDBContext : DbContext
    {
        public ModelDBContext()
            : base("name=ModelDBContext")
        {
        }

        public virtual DbSet<DettagliOrdini> DettagliOrdini { get; set; }
        public virtual DbSet<Ordini> Ordini { get; set; }
        public virtual DbSet<Pizze> Pizze { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pizze>()
                .Property(e => e.Prezzo)
                .HasPrecision(19, 4);
        }
    }
}
