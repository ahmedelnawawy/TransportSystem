namespace TransportSystems.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KhznaMoved")]
    public partial class KhznaMoved
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Key]
        [Column(Order = 1)]
        public bool state { get; set; }

        [Column(TypeName = "money")]
        public decimal? Value { get; set; }

        public long? AccountID { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public DateTime? Date { get; set; }

        public bool? EntryState { get; set; }

        public int? EntryID { get; set; }

        public long? TreasuryID { get; set; }

        [StringLength(200)]
        public string FinancialPostitionType { get; set; }

        public int? FinancialPostitionId { get; set; }

        public int? LoginID { get; set; }

        public virtual AspUser AspUser { get; set; }
    }
}
