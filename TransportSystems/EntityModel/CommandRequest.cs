namespace TransportSystems.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CommandRequest")]
    public partial class CommandRequest
    {
        public int ID { get; set; }

        [StringLength(100)]
        public string FromRegion { get; set; }

        [StringLength(100)]
        public string ToRegion { get; set; }

        [StringLength(50)]
        public string Quantity { get; set; }

        [StringLength(100)]
        public string Client { get; set; }

        public DateTime? RequestDate { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public bool state { get; set; }
    }
}
