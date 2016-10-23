namespace Common.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Prefix")]
    public partial class Prefix
    {
        [Key]
        [StringLength(100)]
        public string IDPrefix { get; set; }

        [Column("Prefix")]
        public int Prefix1 { get; set; }
    }
}
