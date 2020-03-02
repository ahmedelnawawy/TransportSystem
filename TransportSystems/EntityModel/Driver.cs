namespace TransportSystems.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Driver")]
    public partial class Driver
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Driver()
        {
            DriverLocation = new HashSet<DriverLocation>();
            Solar = new HashSet<Solar>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Address { get; set; }

        [StringLength(50)]
        public string phone { get; set; }

        [StringLength(50)]
        public string license { get; set; }

        public DateTime? LicenceDate { get; set; }

        public int? LicencePeriod { get; set; }

        public DateTime? LicenseEndDate { get; set; }

        public int AlertPeriod { get; set; }

        public int? TrafficDepID { get; set; }

        public int? UserId { get; set; }

        public int? LoginID { get; set; }

        public virtual AspUser AspUser { get; set; }

        public virtual TrafficDepartment TrafficDepartment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DriverLocation> DriverLocation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Solar> Solar { get; set; }
    }
}
