namespace TransportSystems.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CarMaintenance")]
    public partial class CarMaintenance
    {
        public int Id { get; set; }

        public int CarId { get; set; }

        public int ServiceId { get; set; }

        [StringLength(50)]
        public string ChangeRate { get; set; }

        [StringLength(50)]
        public string AlertRate { get; set; }

        public bool HaveChangeRate { get; set; }

        public int? LoginID { get; set; }

        public virtual AspUser AspUser { get; set; }

        public virtual Cars Cars { get; set; }

        public virtual Services Services { get; set; }
    }
}
