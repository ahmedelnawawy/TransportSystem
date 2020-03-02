namespace TransportSystems.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SalesInvoiceDetail")]
    public partial class SalesInvoiceDetail
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductID { get; set; }

        public double ProductPrice { get; set; }

        public double? Qty { get; set; }

        public double PricePerRecord { get; set; }

        [Required]
        [StringLength(250)]
        public string PurchaseInvoiceID { get; set; }

        public virtual Product Product { get; set; }

        public virtual SalesInvoice SalesInvoice { get; set; }
    }
}
