namespace Pizzeria.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {
            Ordini = new HashSet<Ordini>();
        }

        [Key]
        public int IdUser { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Campo obbligatorio!")]
        public string Username { get; set; }

        [StringLength(50)]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Campo obbligatorio!")]
        public string Pass { get; set; }

        [StringLength(50)]
        public string Ruolo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ordini> Ordini { get; set; }
    }
}
