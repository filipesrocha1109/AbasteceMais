namespace AbasteceMais.Domain.DTOs.GasStation
{
    public class UpdatePricesGasStationDTO
    {
        public string ID { get; set; }

        public string GasStationID { get; set; }

        public string RegistrationID { get; set; }

        public string PriceGasolinaComum { get; set; }

        public string PriceGasolinaAditivada { get; set; }

        public string PriceDisel { get; set; }

        public string PriceGas { get; set; }

        public string CreatedOn { get; set; }
    }
}
