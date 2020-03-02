namespace TransportSystems.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Services
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Services()
        {
            CarChangeRateOnDis = new HashSet<CarChangeRateOnDis>();
            CarMaintenance = new HashSet<CarMaintenance>();
            SalesInvoice = new HashSet<SalesInvoice>();
            Solar = new HashSet<Solar>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int? LoginID { get; set; }

        [Column(TypeName = "money")]
        public decimal? price { get; set; }

        public virtual AspUser AspUser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CarChangeRateOnDis> CarChangeRateOnDis { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CarMaintenance> CarMaintenance { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalesInvoice> SalesInvoice { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Solar> Solar { get; set; }
    }
}
