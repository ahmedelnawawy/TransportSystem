namespace TransportSystems.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SubAccount")]
    public partial class SubAccount
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SubAccount()
        {
            Cars = new HashSet<Cars>();
            ListPrice = new HashSet<ListPrice>();
            PurchaseInvoice = new HashSet<PurchaseInvoice>();
            TransportCommand = new HashSet<TransportCommand>();
            TransportCommand1 = new HashSet<TransportCommand>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        public int? Level { get; set; }

        [StringLength(50)]
        public string UpAccount { get; set; }

        [StringLength(50)]
        public string BType { get; set; }

        public DateTime? RegisterDate { get; set; }

        public long? MainAccount_id { get; set; }

        public int? LoginID { get; set; }

        [Column(TypeName = "money")]
        public decimal? ABalance { get; set; }

        public virtual AspUser AspUser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cars> Cars { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ListPrice> ListPrice { get; set; }

        public virtual MainAccount MainAccount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchaseInvoice> PurchaseInvoice { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransportCommand> TransportCommand { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransportCommand> TransportCommand1 { get; set; }
    }
}
