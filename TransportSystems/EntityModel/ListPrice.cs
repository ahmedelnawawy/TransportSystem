namespace TransportSystems.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ListPrice")]
    public partial class ListPrice
    {
        public int Id { get; set; }

        public int? Price { get; set; }

        public int? RegionFromId { get; set; }

        public int? RegionToId { get; set; }

        public int? SubAccId { get; set; }

        public int? LoginID { get; set; }

        public int? ProductType { get; set; }

        public virtual FromRegion FromRegion { get; set; }

        public virtual FromRegion FromRegion1 { get; set; }

        public virtual SubAccount SubAccount { get; set; }

        public virtual TransferProductType TransferProductType { get; set; }
    }
}
