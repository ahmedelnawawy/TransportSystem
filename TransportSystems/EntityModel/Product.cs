namespace TransportSystems.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            PurchaseInvoiceDetail = new HashSet<PurchaseInvoiceDetail>();
            SalesInvoiceDetail = new HashSet<SalesInvoiceDetail>();
            TransportCommand = new HashSet<TransportCommand>();
        }

        [StringLength(100)]
        public string ID { get; set; }

        [Required]
        public string name { get; set; }

        [Column(TypeName = "money")]
        public decimal? PPrice { get; set; }

        [Column(TypeName = "money")]
        public decimal? SPrice { get; set; }

        public double? BalanceQ { get; set; }

        public double? MinBalance { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public double? CurrentBalance { get; set; }

        public int? StorID { get; set; }

        public int SectorID { get; set; }

        public int? LoginID { get; set; }

        public virtual AspUser AspUser { get; set; }

        public virtual Sector Sector { get; set; }

        public virtual SubStore SubStore { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchaseInvoiceDetail> PurchaseInvoiceDetail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalesInvoiceDetail> SalesInvoiceDetail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransportCommand> TransportCommand { get; set; }
    }
}
