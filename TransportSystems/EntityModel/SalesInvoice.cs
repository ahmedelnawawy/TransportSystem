namespace TransportSystems.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SalesInvoice")]
    public partial class SalesInvoice
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SalesInvoice()
        {
            SalesInvoiceDetail = new HashSet<SalesInvoiceDetail>();
        }

        [StringLength(250)]
        public string Id { get; set; }

        public DateTime InvoiceDate { get; set; }

        public int CarId { get; set; }

        public int ServiceId { get; set; }

        public bool? PurchaseType { get; set; }

        public double? Total { get; set; }

        public int UserID { get; set; }

        [StringLength(50)]
        public string PaymentMethod { get; set; }

        public double PaymentValue { get; set; }

        public virtual AspUser AspUser { get; set; }

        public virtual Cars Cars { get; set; }

        public virtual Services Services { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalesInvoiceDetail> SalesInvoiceDetail { get; set; }
    }
}
