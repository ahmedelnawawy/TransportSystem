namespace TransportSystems.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Rol_PrivFT
    {
        public int ID { get; set; }

        public int? Rol_id { get; set; }

        public int? Priv_id { get; set; }

        public bool? AddFlag { get; set; }

        public bool? EditFlag { get; set; }

        public bool? DeleteFlag { get; set; }

        public bool? SearchFlag { get; set; }

        public bool? AllFlag { get; set; }

        public int? LoginID { get; set; }

        public virtual AspUser AspUser { get; set; }

        public virtual privilage privilage { get; set; }

        public virtual Role Role { get; set; }
    }
}
