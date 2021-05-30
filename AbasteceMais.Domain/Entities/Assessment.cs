namespace AbasteceMais.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Assessment")]
    public partial class Assessment
    {
        public int ID { get; set; }

        public int GasStaionID { get; set; }

        public int RegistrationID { get; set; }

        [Column("Assessment")]
        public bool Assessment1 { get; set; }

        public DateTime? CreatedOn { get; set; }
    }
}
