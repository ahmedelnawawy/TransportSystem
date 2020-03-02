namespace TransportSystems.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CommandMessages
    {
        public int ID { get; set; }

        public string MessageText { get; set; }

        [StringLength(150)]
        public string FirstImage { get; set; }

        [StringLength(150)]
        public string SecondImage { get; set; }

        [StringLength(150)]
        public string ThirdImage { get; set; }

        public DateTime? MessageDate { get; set; }

        public bool state { get; set; }

        public int? TransportCommandID { get; set; }

        public virtual TransportCommand TransportCommand { get; set; }
    }
}
