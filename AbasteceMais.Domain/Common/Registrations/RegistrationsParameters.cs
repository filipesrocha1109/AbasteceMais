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

        
        public string CPFCNPJ { get; set; }

        
        public string Phone { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Field 'Password' is required")]
        public string Password { get; set; }

        public string CEP { get; set; }

        
        public string Address { get; set; }

        
        public string Number { get; set; }

       
        public string DistrictID { get; set; }

        
        public string CityID { get; set; }

        
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


        public string CPFCNPJ { get; set; }


        public string Phone { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Field 'Password' is required")]
        public string Password { get; set; }

        public string CEP { get; set; }


        public string Address { get; set; }

 
        public string Number { get; set; }

 
        public string DistrictID { get; set; }

        public string CityID { get; set; }


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
        public string CityID { get; set; }
    }

    public class CityParametersGetAll
    {
        public string StateID { get; set; }
    }
    public class StateParametersGetAll
    {

    }
}
