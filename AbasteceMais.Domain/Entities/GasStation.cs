namespace AbasteceMais.Domain.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("GasStation")]
    public partial class GasStation
    {
        public int ID { get; set; }

        public bool? Status { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        public bool? GasolinaComum { get; set; }

        public bool? GasolinaAditivada { get; set; }

        public bool? Disel { get; set; }

        public bool? Gas { get; set; }

        [Column(TypeName = "money")]
        public decimal? PriceGasolinaComum { get; set; }

        [Column(TypeName = "money")]
        public decimal? PriceGasolinaAditivada { get; set; }

        [Column(TypeName = "money")]
        public decimal? PriceDisel { get; set; }

        [Column(TypeName = "money")]
        public decimal? PriceGas { get; set; }

        [StringLength(50)]
        public string Latitude { get; set; }

        [StringLength(50)]
        public string Longitude { get; set; }

        [StringLength(50)]
        public string CEP { get; set; }

        [StringLength(150)]
        public string Address { get; set; }

        [StringLength(10)]
        public string Number { get; set; }

        [StringLength(100)]
        public string DistrictID { get; set; }

        [StringLength(100)]
        public string CityID { get; set; }

        [StringLength(2)]
        public string StateID { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
