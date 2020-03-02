namespace TransportSystems.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Indx")]
    public partial class Indx
    {
        public long ID { get; set; }

        [StringLength(50)]
        public string ClientCategory { get; set; }

        [StringLength(50)]
        public string ClientType { get; set; }

        public int? SalesUesrId { get; set; }

        [StringLength(50)]
        public string ResponsiblePersonName { get; set; }

        [StringLength(50)]
        public string ResponsiblePersonPhone { get; set; }

        [StringLength(50)]
        public string AnotherResponsiblePersonPhone { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Address { get; set; }

        [StringLength(50)]
        public string Sgl_TaxNO { get; set; }

        [StringLength(50)]
        public string CommercialDocument { get; set; }

        [StringLength(50)]
        public string TaxDocument { get; set; }

        public long? Sub_ID { get; set; }

        [StringLength(50)]
        public string Maamria { get; set; }

        [StringLength(50)]
        public string MobileNo { get; set; }

        [StringLength(50)]
        public string PersonalID { get; set; }

        public int? LoginID { get; set; }

        public virtual AspUser AspUser { get; set; }
    }
}
