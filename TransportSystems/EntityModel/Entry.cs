namespace TransportSystems.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Entry")]
    public partial class Entry
    {
        public DateTime Date { get; set; }

        [Column(TypeName = "money")]
        public decimal value { get; set; }

        [Required]
        [StringLength(50)]
        public string status { get; set; }

        [Required]
        public string description { get; set; }

        public long SubAccount_id { get; set; }

        public int ID { get; set; }

        public int? EntryID { get; set; }

        public int? RecordID { get; set; }

        [StringLength(50)]
        public string EntryType { get; set; }

        public int? LoginID { get; set; }

        public virtual AspUser AspUser { get; set; }
    }
}
