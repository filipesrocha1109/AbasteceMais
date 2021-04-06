using System.ComponentModel.DataAnnotations;

namespace AbasteceMais.Domain.Common.Registrations
{
    public class RegistrationsParametersID
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Field 'ID' is required")]
        public string ID { get; set; }
    }

    public class RegistrationsParametersCreate
    {

        [Required(AllowEmptyStrings = false, ErrorMessage = "Field 'Name' is required")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Field 'Email' is required")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Field 'CPFCNPJ' is required")]
        public string CPFCNPJ { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Field 'Phone' is required")]
        public string Phone { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Field 'Password' is required")]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Field 'CEP' is required")]
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
    public class RegistrationsParametersGetAll
    {

    }

    public class RegistrationsParametersUpdate
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Field 'ID' is required")]
        public string ID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Field 'Name' is required")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Field 'Email' is required")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Field 'CPFCNPJ' is required")]
        public string CPFCNPJ { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Field 'Phone' is required")]
        public string Phone { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Field 'Password' is required")]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Field 'CEP' is required")]
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

    public class RegistrationsParametersPassword
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Field 'UserName' is required")]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Field 'Password' is required")]
        public string Password { get; set; }
    }

    public class DistrictParametersGetAll
    {

    }

    public class CityParametersGetAll
    {

    }
    public class StateParametersGetAll
    {

    }
}
