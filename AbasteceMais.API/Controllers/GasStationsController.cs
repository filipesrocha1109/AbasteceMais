using AbasteceMais.API.Auth;
using AbasteceMais.Domain.Common;
using AbasteceMais.Domain.Common.GasStations;
using AbasteceMais.Domain.DTOs.GasStation;
using AbasteceMais.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace AbasteceMais.API.Controllers
{
    [BasicAuthentication]
    [RoutePrefix("api/GasStations")]
    public class GasStationsController : ApiController
    {
        private readonly IGasStationsService _gasstationsservice;

        public GasStationsController(IGasStationsService sendService)
        {
            _gasstationsservice = sendService;
        }

        #region PUBLIC_METHODS


        [HttpGet]
        [Route("GetGasStations")]
        public IHttpActionResult GetGasStations([FromUri] GasStationsParametersGetAll gasStationsParametersGetAll)
        {

            IList<GasStationsDTO> gasStationsDTO = _gasstationsservice.GetGasStations(gasStationsParametersGetAll, out ReturnValues returnValues);
            if (!returnValues.Error)
            {
                return Ok(new ResponseSuccess
                {
                    Success = true,
                    Status = Convert.ToInt32(returnValues.Code),
                    Message = returnValues.Message,
                    Data = new
                    {
                        GasStations = gasStationsDTO
                    }
                });
            }

            return Ok(new ResponseError
            {
                Success = false,
                Status = Convert.ToInt32(returnValues.Code),
                Message = returnValues.Message
            });
        }

        [HttpGet]
        [Route("GetGasStationsByID")]
        public IHttpActionResult GetGasStationsByID([FromUri] GasStationsParametersID gasStationsParametersID)
        {
            if (gasStationsParametersID != null && ModelState.IsValid)
            {
                GasStationsDTO gasStationsDTO = _gasstationsservice.GetGasStationsByID(gasStationsParametersID, out ReturnValues returnValues);

                if (!returnValues.Error)
                {
                    return Ok(new ResponseSuccess
                    {
                        Success = true,
                        Status = Convert.ToInt32(returnValues.Code),
                        Message = returnValues.Message,
                        Data = new
                        {
                            gasStation = gasStationsDTO
                        }
                    });
                }

                return Ok(new ResponseError
                {
                    Success = false,
                    Status = Convert.ToInt32(returnValues.Code),
                    Message = returnValues.Message
                });
            }

            return BadRequest(ModelState);
        }


        [HttpPost]
        [Route("CreateGasStations")]
        public IHttpActionResult CreateGasStations([FromBody] GasStationsParametersCreate gasStationsParametersCreate)
        {

            if (gasStationsParametersCreate != null && ModelState.IsValid)
            {
                GasStationsDTO gasStationsDTO = _gasstationsservice.CreateGasStations(gasStationsParametersCreate, out ReturnValues returnValues);

                if (!returnValues.Error)
                {
                    return Ok(new ResponseSuccess
                    {
                        Success = true,
                        Status = Convert.ToInt32(returnValues.Code),
                        Message = returnValues.Message,
                        Data = new
                        {
                            gasStation = gasStationsDTO,
                        },
                    });
                }

                return Ok(new ResponseError
                {
                    Success = false,
                    Status = Convert.ToInt32(returnValues.Code),
                    Message = returnValues.Message
                });
            }

            return BadRequest(ModelState);
        }

        //[HttpPut]
        //[Route("UpdateRegistrationsbyID")]
        //public IHttpActionResult UpdateRegistrationsbyID([FromBody] RegistrationsParametersUpdate usersParametersUpdate)
        //{

        //    if (usersParametersUpdate != null && ModelState.IsValid)
        //    {
        //        RegistrationsDTO registrationsDTO = _registrationsservice.UpdateRegistrationsbyID(usersParametersUpdate, out ReturnValues returnValues);

        //        if (!returnValues.Error)
        //        {
        //            return Ok(new ResponseSuccess
        //            {
        //                Success = true,
        //                Status = Convert.ToInt32(returnValues.Code),
        //                Message = returnValues.Message,
        //                Data = new
        //                {
        //                    Registration = registrationsDTO
        //                }
        //            });
        //        }

        //        return Ok(new ResponseError
        //        {
        //            Success = false,
        //            Status = Convert.ToInt32(returnValues.Code),
        //            Message = returnValues.Message
        //        });
        //    }

        //    return BadRequest(ModelState);
        //}

        [HttpDelete]
        [Route("DeleteGasStationsByID")]
        public IHttpActionResult DeleteGasStationsByID([FromBody] GasStationsParametersID gasStationsParametersID)
        {
            if (gasStationsParametersID != null && ModelState.IsValid)
            {
                GasStationsDTO gasStationsDTO = _gasstationsservice.DeleteGasStationsByID(gasStationsParametersID, out ReturnValues returnValues);

                if (!returnValues.Error)
                {
                    return Ok(new ResponseSuccess
                    {
                        Success = true,
                        Status = Convert.ToInt32(returnValues.Code),
                        Message = returnValues.Message,
                        Data = new
                        {
                            gasStationDeletdID = gasStationsDTO.ID
                        }
                    });
                }

                return Ok(new ResponseError
                {
                    Success = false,
                    Status = Convert.ToInt32(returnValues.Code),
                    Message = returnValues.Message
                });
            }

            return BadRequest(ModelState);
        }

        #endregion
    }
}