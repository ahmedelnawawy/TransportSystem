namespace TransportSystems.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PurchaseInvoice")]
    public partial class PurchaseInvoice
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PurchaseInvoice()
        {
            PurchaseInvoiceDetail = new HashSet<PurchaseInvoiceDetail>();
        }

        [StringLength(250)]
        public string Id { get; set; }

        public DateTime InvoiceDate { get; set; }

        public int SubAccountId { get; set; }

        public bool? PurchaseType { get; set; }

        public double? Total { get; set; }

        public int UserID { get; set; }

        [StringLength(50)]
        public string PaymentMethod { get; set; }

        public double PaymentValue { get; set; }

        public int? KeadNo { get; set; }

        public virtual AspUser AspUser { get; set; }

        public virtual SubAccount SubAccount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchaseInvoiceDetail> PurchaseInvoiceDetail { get; set; }
    }
}
