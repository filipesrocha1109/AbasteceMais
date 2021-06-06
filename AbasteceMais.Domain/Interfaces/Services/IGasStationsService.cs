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

        ////CREATE ASSESSMENT
        AssessmentsDTO CreateAssessments(AssessmentsParametersCreate assessmentsParametersCreate, out ReturnValues returnValues);

        ////CREATE comments
        CommentsDTO CreateComments(CommentsParametersCreate commentsParametersCreate, out ReturnValues returnValues);

        ////DELETE comments
        CommentsDTO DeleteCommentsByID(CommentsParametersDelete commentsParametersDelete, out ReturnValues returnValues);

        ////GET comments
        IList<CommentsDTO> GetComments(CommentsGetParameters commentsGetParameters, out ReturnValues returnValues);

        ////GET stars
        StarsDTO GetGasStationStarts(GasStationsParametersID gasStationsParametersID, out ReturnValues returnValues);

        ////UPDATE prices
        UpdatePricesGasStationDTO UpdatePriceGasStations(UpdatePriicesParameters updatePriicesParameters, out ReturnValues returnValues);

    }
}
