namespace TransportSystems.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CarChangeRateOnDis
    {
        public int Id { get; set; }

        public int CarId { get; set; }

        public int ServiceId { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public double Before { get; set; }

        public DateTime? DateBefore { get; set; }

        [StringLength(50)]
        public string AtHourBefore { get; set; }

        public double After { get; set; }

        public DateTime? DateAfter { get; set; }

        [StringLength(50)]
        public string AtHourAfter { get; set; }

        public bool State { get; set; }

        public int? LoginID { get; set; }

        public virtual Cars Cars { get; set; }

        public virtual Services Services { get; set; }
    }
}
