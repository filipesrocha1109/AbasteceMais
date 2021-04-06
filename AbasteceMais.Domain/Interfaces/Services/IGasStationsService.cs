using AbasteceMais.Domain.Common;
using AbasteceMais.Domain.Common.GasStations;
using AbasteceMais.Domain.DTOs.GasStation;
using System.Collections.Generic;

namespace AbasteceMais.Domain.Interfaces.Services
{
    public interface IGasStationsService
    {
        //GET
        IList<GasStationsDTO> GetGasStations(GasStationsParametersGetAll gasStationsParametersGetAll, out ReturnValues returnValues);

        ////GET ID
        GasStationsDTO GetGasStationsByID(GasStationsParametersID gasStationsParametersID, out ReturnValues returnValues);

        ////CREATE
        GasStationsDTO CreateGasStations(GasStationsParametersCreate gasStationsParametersCreate, out ReturnValues returnValues);

        ////UPDATE
        //RegistrationsDTO UpdateRegistrationsbyID(RegistrationsParametersUpdate usersParametersUpdate, out ReturnValues returnValues);

        ////DELETE
        GasStationsDTO DeleteGasStationsByID(GasStationsParametersID gasStationsParametersID, out ReturnValues returnValues);

    }
}
