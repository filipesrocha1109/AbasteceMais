namespace AbasteceMais.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("State")]
    public partial class State
    {
        [StringLength(5)]
        public string Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }
    }
}
