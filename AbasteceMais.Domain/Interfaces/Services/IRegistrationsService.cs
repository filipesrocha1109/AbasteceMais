using AbasteceMais.Domain.Common;
using AbasteceMais.Domain.Common.Registrations;
using AbasteceMais.Domain.DTOs.Registrations;
using System.Collections.Generic;

namespace AbasteceMais.Domain.Interfaces.Services
{
    public interface IRegistrationsService
    {
        //GET
        IList<RegistrationsDTO> GetRegistrations(RegistrationsParametersGetAll registrationsParametersGetAll, out ReturnValues returnValues);

        //GET ID
        RegistrationsDTO GetRegistrationsByID(RegistrationsParametersID registrationsParametersID, out ReturnValues returnValues);

        //GET PASSWORD
        RegistrationsDTO GetRegistrationsByPassword(RegistrationsParametersPassword registrationsParametersPassword, out ReturnValues returnValues);

        //CREATE
        RegistrationsDTO CreateRegistrations(RegistrationsParametersCreate registrationsParametersCreate, out ReturnValues returnValues);

        //UPDATE
        RegistrationsDTO UpdateRegistrationsbyID(RegistrationsParametersUpdate usersParametersUpdate, out ReturnValues returnValues);

        //DELETE
        RegistrationsDTO DeleteRegistrationsByID(RegistrationsParametersID registrationsParametersID, out ReturnValues returnValues);


        IList<StatesDTO> GetStates(StateParametersGetAll stateParametersGetAll, out ReturnValues returnValues);

        IList<CitysDTO> GetCitys(CityParametersGetAll cityParametersGetAll, out ReturnValues returnValues);

        IList<DistrictsDTO> GetDistricts(DistrictParametersGetAll districtParametersGetAll, out ReturnValues returnValues);



    }
}
