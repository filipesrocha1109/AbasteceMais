using AbasteceMais.API.Auth;
using AbasteceMais.Domain.Common;
using AbasteceMais.Domain.Common.Registrations;
using AbasteceMais.Domain.DTOs.Registrations;
using AbasteceMais.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace AbasteceMais.API.Controllers
{
    [BasicAuthentication]
    [RoutePrefix("api/Registrations")]
    public class RegistrationsController : ApiController
    {
        private readonly IRegistrationsService _registrationsservice;

        public RegistrationsController(IRegistrationsService sendService)
        {
            _registrationsservice = sendService;
        }

        #region PUBLIC_METHODS


        [HttpGet]
        [Route("GetRegistrations")]
        public IHttpActionResult GetRegistrations([FromBody] RegistrationsParametersGetAll registrationsParametersGetAll)
        {

            IList<RegistrationsDTO> registrationsDTO = _registrationsservice.GetRegistrations(registrationsParametersGetAll, out ReturnValues returnValues);
            if (!returnValues.Error)
            {
                return Ok(new ResponseSuccess
                {
                    Success = true,
                    Status = Convert.ToInt32(returnValues.Code),
                    Message = returnValues.Message,
                    Data = new
                    {
                        Registrations = registrationsDTO
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
        [Route("GetRegistrationsByID")]
        public IHttpActionResult GetUsGetRegistrationsByIDerID([FromBody] RegistrationsParametersID registrationsParametersID)
        {
            if (registrationsParametersID != null && ModelState.IsValid)
            {
                RegistrationsDTO registrationsDTO = _registrationsservice.GetRegistrationsByID(registrationsParametersID, out ReturnValues returnValues);

                if (!returnValues.Error)
                {
                    return Ok(new ResponseSuccess
                    {
                        Success = true,
                        Status = Convert.ToInt32(returnValues.Code),
                        Message = returnValues.Message,
                        Data = new
                        {
                            registration = registrationsDTO
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
        [Route("LoginRegistrations")]
        public IHttpActionResult GetRegistrationsByPassword([FromBody] RegistrationsParametersPassword registrationsParametersPassword)
        {
            if (registrationsParametersPassword != null && ModelState.IsValid)
            {
                RegistrationsDTO registrationsDTO = _registrationsservice.GetRegistrationsByPassword(registrationsParametersPassword, out ReturnValues returnValues);

                if (!returnValues.Error)
                {
                    return Ok(new ResponseSuccess
                    {
                        Success = true,
                        Status = Convert.ToInt32(returnValues.Code),
                        Message = returnValues.Message,
                        Data = new
                        {
                            registration = registrationsDTO
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
        [Route("CreateRegistrations")]
        public IHttpActionResult CreateRegistrations([FromBody] RegistrationsParametersCreate registrationsParametersCreate)
        {

            if (registrationsParametersCreate != null && ModelState.IsValid)
            {
                RegistrationsDTO registrationsDTO = _registrationsservice.CreateRegistrations(registrationsParametersCreate, out ReturnValues returnValues);

                if (!returnValues.Error)
                {
                    return Ok(new ResponseSuccess
                    {
                        Success = true,
                        Status = Convert.ToInt32(returnValues.Code),
                        Message = returnValues.Message,
                        Data = new
                        {
                            registration = registrationsDTO,
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

        [HttpPut]
        [Route("UpdateRegistrationsbyID")]
        public IHttpActionResult UpdateRegistrationsbyID([FromBody] RegistrationsParametersUpdate usersParametersUpdate)
        {

            if (usersParametersUpdate != null && ModelState.IsValid)
            {
                RegistrationsDTO registrationsDTO = _registrationsservice.UpdateRegistrationsbyID(usersParametersUpdate, out ReturnValues returnValues);

                if (!returnValues.Error)
                {
                    return Ok(new ResponseSuccess
                    {
                        Success = true,
                        Status = Convert.ToInt32(returnValues.Code),
                        Message = returnValues.Message,
                        Data = new
                        {
                            Registration = registrationsDTO
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

        [HttpDelete]
        [Route("DeleteRegistrationsByID")]
        public IHttpActionResult DeleteRegistrationsByID([FromBody] RegistrationsParametersID registrationsParametersID)
        {
            if (registrationsParametersID != null && ModelState.IsValid)
            {
                RegistrationsDTO registrationsDTO = _registrationsservice.DeleteRegistrationsByID(registrationsParametersID, out ReturnValues returnValues);

                if (!returnValues.Error)
                {
                    return Ok(new ResponseSuccess
                    {
                        Success = true,
                        Status = Convert.ToInt32(returnValues.Code),
                        Message = returnValues.Message,
                        Data = new
                        {
                            registrationDeletdID = registrationsDTO.ID
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