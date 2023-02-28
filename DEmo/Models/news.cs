namespace DEmo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class news
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public news()
        {
            like_news = new HashSet<like_news>();
            comment_news = new HashSet<comment_news>();
        }

        public int id { get; set; }

        [StringLength(150)]
        public string title { get; set; }

        [StringLength(250)]
        public string bref { get; set; }

        public string desc { get; set; }

        public DateTime? date { get; set; }

        [StringLength(150)]
        public string photo { get; set; }

        public int? cat_id { get; set; }

        public int? user_id { get; set; }

        public int? admin_id { get; set; }

        [StringLength(50)]
        public string state { get; set; }

        public int? delete { get; set; }

        public virtual admin admin { get; set; }

        public virtual catalog catalog { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<like_news> like_news { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<comment_news> comment_news { get; set; }

        public virtual user user { get; set; }
    }
}
