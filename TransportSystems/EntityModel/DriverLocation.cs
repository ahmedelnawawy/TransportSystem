namespace TransportSystems.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DriverLocation")]
    public partial class DriverLocation
    {
        public int ID { get; set; }

        public int command_id { get; set; }

        public int driver_id { get; set; }

        public double? latitude { get; set; }

        public double? longitude { get; set; }

        [StringLength(100)]
        public string last_seen { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public virtual Driver Driver { get; set; }

        public virtual TransportCommand TransportCommand { get; set; }
    }
}
