namespace AbasteceMais.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UpdatePricesGasStation")]
    public partial class UpdatePricesGasStation
    {
        public int ID { get; set; }

        [Required]
        [StringLength(20)]
        public string GasStationID { get; set; }

        [Required]
        [StringLength(20)]
        public string RegistrationID { get; set; }

        [Column(TypeName = "money")]
        public decimal? PriceGasolinaComum { get; set; }

        [Column(TypeName = "money")]
        public decimal? PriceGasolinaAditivada { get; set; }

        [Column(TypeName = "money")]
        public decimal? PriceDisel { get; set; }

        [Column(TypeName = "money")]
        public decimal? PriceGas { get; set; }

        public DateTime? CreatedOn { get; set; }
    }
}
