namespace TransportSystems.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Solar")]
    public partial class Solar
    {
        public int ID { get; set; }

        public DateTime? Date { get; set; }

        public int? CarID { get; set; }

        public int? DriverID { get; set; }

        [Column(TypeName = "money")]
        public decimal? Total { get; set; }

        public double? SolarQty { get; set; }

        public double? CurrentReading { get; set; }

        public double? Average { get; set; }

        public double? LastReading { get; set; }

        public double? Distance { get; set; }

        public string Notes { get; set; }

        public int? ServiceID { get; set; }

        public int? KhaznaBankID { get; set; }

        public virtual Cars Cars { get; set; }

        public virtual Driver Driver { get; set; }

        public virtual Services Services { get; set; }
    }
}
