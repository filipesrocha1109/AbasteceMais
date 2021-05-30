namespace AbasteceMais.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Comment
    {
        public int ID { get; set; }

        public int GasStaionID { get; set; }

        public int RegistrationID { get; set; }

        [Column("Comment")]
        [Required]
        public string Comment1 { get; set; }

        public DateTime? CreatedOn { get; set; }
    }
}
