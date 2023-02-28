namespace DEmo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("user")]
    public partial class user
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public user()
        {
            likes = new HashSet<like>();
            likes1 = new HashSet<like>();
            like_news = new HashSet<like_news>();
            news = new HashSet<news>();
            comment_news = new HashSet<comment_news>();
            comments = new HashSet<comment>();
            comments1 = new HashSet<comment>();
        }

        public int id { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage ="Enter your name")]
        public string username { get; set; }

        [StringLength(50)]
        [DataType(DataType.EmailAddress,ErrorMessage ="Enter correct Email")]
        [RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$", ErrorMessage ="Enter correct e-mail")]
        [Required(ErrorMessage ="Enter your E-mail")]
        public string mail { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Enter correct password")]
        [MinLength(8)]
        [MaxLength(20)]
        public string password { get; set; }
        [Compare(nameof(password),ErrorMessage ="password wrong")]
        [NotMapped]
        
        public string confirm_password { get; set; }

        public int? age { get; set; }

        public string address { get; set; }

        [StringLength(250)]
        public string photo { get; set; }

        public int? admin_id { get; set; }

        public int? catalog_id { get; set; }

        [StringLength(50)]
        public string state { get; set; }

        public int? like_id { get; set; }

        public int? delete { get; set; }

        public virtual admin admin { get; set; }

        public virtual catalog catalog { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<like> likes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<like> likes1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<like_news> like_news { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<news> news { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<comment_news> comment_news { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<comment> comments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<comment> comments1 { get; set; }
    }
}
