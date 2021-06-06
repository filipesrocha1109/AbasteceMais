using System.ComponentModel.DataAnnotations;

namespace AbasteceMais.Domain.Common.GasStations
{
    public class GasStationsParametersID
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Field 'ID' is required")]
        public string ID { get; set; }
    }

    public class GasStationsParametersGetAll
    {
        public string Name { get; set; }

        public string TypeGas { get; set; }

        public string DistrictID { get; set; }

        public string Order { get; set; }
    }

    public class GasStationsParametersCreate
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Field 'Name' is required")]
        public string Name { get; set; }

        public string Phone { get; set; }

        public string GasolinaComum { get; set; }

        public string GasolinaAditivada { get; set; }

        public string Disel { get; set; }

        public string Gas { get; set; }

        public string PriceGasolinaComum { get; set; }

        public string PriceGasolinaAditivada { get; set; }

        public string PriceDisel { get; set; }

        public string PriceGas { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string CEP { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Field 'Address' is required")]
        public string Address { get; set; }
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "Field 'Number' is required")]
        public string Number { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Field 'District' is required")]
        public string DistrictID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Field 'City' is required")]
        public string CityID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Field 'State' is required")]
        public string StateID { get; set; }

    }

    public class AssessmentsParametersCreate
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Field 'RegistrationID' is required")]
        public string RegistrationID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Field 'GasStationID' is required")]
        public string GasStationID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Field 'Assessment' is required")]
        public string Assessment { get; set; }
    }

    public class AssessmentsGetParametersByID
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Field 'ID' is required")]
        public string ID { get; set; }
    }

    public class AssessmentsGetParametersByGasStation
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Field 'GasStationID' is required")]
        public string GasStationID { get; set; }
    }

    public class CommentsParametersCreate
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Field 'RegistrationID' is required")]
        public string RegistrationID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Field 'GasStationID' is required")]
        public string GasStationID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Field 'Comment' is required")]
        public string Comment { get; set; }
    }

    public class CommentsGetParameters
    {
        public string GasStationID { get; set; }
    }

    public class CommentsGetParametersByID
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Field 'ID' is required")]
        public string ID { get; set; }
    }

    public class CommentsGetParametersByGasStation
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Field 'GasStationID' is required")]
        public string GasStationID { get; set; }
    }

    public class CommentsParametersDelete
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Field 'ID' is required")]
        public string ID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Field 'RegistrationID' is required")]
        public string RegistrationID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Field 'GasStationID' is required")]
        public string GasStationID { get; set; }
    }

    public class UpdatePriicesParameters
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Field 'GasstationID' is required")]
        public string GasstationID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Field 'RegistrationID' is required")]
        public string RegistrationID { get; set; }

        public decimal GasolinaComum { get; set; }

        public decimal GasolinaAditivada { get; set; }
        
        public decimal Gas { get; set; }
        
        public decimal Disel { get; set; }
    }

}
