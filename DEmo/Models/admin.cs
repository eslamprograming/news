namespace DEmo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("admin")]
    public partial class admin
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public admin()
        {
            catalogs = new HashSet<catalog>();
            news = new HashSet<news>();
            users = new HashSet<user>();
        }

        [Key]
        public int admin_id { get; set; }

        [StringLength(50)]
        public string username { get; set; }

        public string password { get; set; }

        public string photo { get; set; }

        public string phone { get; set; }

        [Column("e-mail")]
        public string e_mail { get; set; }

        public string address { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dateofbirth { get; set; }

        public int? delete { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<catalog> catalogs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<news> news { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<user> users { get; set; }
    }
}
