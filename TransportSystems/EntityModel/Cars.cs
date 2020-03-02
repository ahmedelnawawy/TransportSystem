namespace TransportSystems.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cars
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cars()
        {
            CarChangeRateOnDis = new HashSet<CarChangeRateOnDis>();
            CarMaintenance = new HashSet<CarMaintenance>();
            SalesInvoice = new HashSet<SalesInvoice>();
            Solar = new HashSet<Solar>();
            TransportCommand = new HashSet<TransportCommand>();
        }

        public int id { get; set; }

        [StringLength(10)]
        public string CarNo { get; set; }

        [StringLength(50)]
        public string LicenceNO { get; set; }

        public DateTime? LicenceDate { get; set; }

        public int? LicencePeriod { get; set; }

        public DateTime? LicenseEndDate { get; set; }

        public int AlertPeriod { get; set; }

        public int? ColorId { get; set; }

        public int? CityId { get; set; }

        public int? CarTypeId { get; set; }

        public int? TrafficDepID { get; set; }

        public int? SubAccId { get; set; }

        public int? LoginID { get; set; }

        public virtual AspUser AspUser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CarChangeRateOnDis> CarChangeRateOnDis { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CarMaintenance> CarMaintenance { get; set; }

        public virtual SubAccount SubAccount { get; set; }

        public virtual TrafficDepartment TrafficDepartment { get; set; }

        public virtual City City { get; set; }

        public virtual Colors Colors { get; set; }

        public virtual CarType CarType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalesInvoice> SalesInvoice { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Solar> Solar { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransportCommand> TransportCommand { get; set; }
    }
}
