namespace TransportSystems.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TransportCommand")]
    public partial class TransportCommand
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TransportCommand()
        {
            CommandMessages = new HashSet<CommandMessages>();
            DriverLocation = new HashSet<DriverLocation>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public DateTime TransportCommandTime { get; set; }

        [Required]
        [StringLength(50)]
        public string TimeOfShipping { get; set; }

        public int SubAccClientId { get; set; }

        public int SubAccVendorId { get; set; }

        public int CarId { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductId { get; set; }

        public int FromRegionId { get; set; }

        public int ToRegionId { get; set; }

        public double TransportPrice { get; set; }

        [Required]
        [StringLength(50)]
        public string TransportType { get; set; }

        public double Quantity { get; set; }

        public double TotalTransportPrice { get; set; }

        [Required]
        [StringLength(50)]
        public string PaymentWay { get; set; }

        public int? State { get; set; }

        public int? LoginID { get; set; }

        public int? DriverID { get; set; }

        public virtual AspUser AspUser { get; set; }

        public virtual Cars Cars { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CommandMessages> CommandMessages { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DriverLocation> DriverLocation { get; set; }

        public virtual FromRegion FromRegion { get; set; }

        public virtual FromRegion FromRegion1 { get; set; }

        public virtual Product Product { get; set; }

        public virtual SubAccount SubAccount { get; set; }

        public virtual SubAccount SubAccount1 { get; set; }
    }
}
