using System;
using AbasteceMais.CrossCutting.Utils;
using AbasteceMais.Domain.Enums;
using AbasteceMais.Domain.Interfaces.Services;
using AbasteceMais.Domain.Interfaces.UnitOfWork;
using AbasteceMais.Domain.DTOs.Registrations;
using AbasteceMais.Domain.Common;
using AbasteceMais.Domain.Common.Registrations;
using AbasteceMais.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace AbasteceMais.Services.Service
{
    public class RegistrationsService : IRegistrationsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegistrationsService
            (
            IUnitOfWork unitOfWork
            )
        {
            _unitOfWork = unitOfWork;
        }
        #region PUBLIC_METHODS

        public IList<RegistrationsDTO> GetRegistrations(RegistrationsParametersGetAll registrationsParametersGetAll, out ReturnValues returnValues)
        {
            IList<RegistrationsDTO> registrationsDTO = null;
            returnValues = new ReturnValues();

            try
            {
                var query = _unitOfWork.RegistrationRepository.QueryableObject();

                registrationsDTO = query.Select(row => new RegistrationsDTO()
                {
                    ID = row.ID.ToString(),
                    Status = row.Status.ToString(),
                    Name = row.Name,
                    Email = row.Email,
                    CPFCNPJ = row.CPFCNPJ,
                    Phone = row.Phone,
                    Username = row.Username,
                    Password = row.Password,
                    CEP = row.CEP,
                    Address = row.Address,
                    Number = row.Number,
                    DistrictID = row.DistrictID,
                    CityID = row.CityID,
                    StateID = row.StateID,
                    Type = row.Type,
                    CreatedOn = row.CreatedOn.ToString(),
                    UpdatedOn = row.UpdatedOn.ToString()

                }).ToList();

                returnValues.SetReturnValues(false, ErrorCodes.Ok, Utils.GetEnumDescription(ErrorCodes.Ok));
            }
            catch (Exception ex)
            {
                returnValues.SetReturnValues(true, ErrorCodes.InternalError, ex.Message);
            }

            return registrationsDTO as IList<RegistrationsDTO>;
        }

        public RegistrationsDTO GetRegistrationsByID(RegistrationsParametersID registrationsParametersID, out ReturnValues returnValues)
        {

            Registration registration;
            RegistrationsDTO registrationsDTO = null;
            returnValues = new ReturnValues();
            int ID = Convert.ToInt32(registrationsParametersID.ID);

            try
            {
                registration = _unitOfWork.RegistrationRepository.Get(row => row.ID == ID);
                if (registration == null)
                {
                    returnValues.SetReturnValues(false, ErrorCodes.Ok, Utils.GetEnumDescription(ErrorCodes.NotFound));
                    return registrationsDTO;
                }

                registrationsDTO = new RegistrationsDTO
                {
                    ID = registration.ID.ToString(),
                    Status = registration.Status.ToString(),
                    Name = registration.Name,
                    Email = registration.Email,
                    CPFCNPJ = registration.CPFCNPJ,
                    Phone = registration.Phone,
                    Username = registration.Username,
                    Password = registration.Password,
                    CEP = registration.CEP,
                    Address = registration.Address,
                    Number = registration.Number,
                    DistrictID = registration.DistrictID,
                    CityID = registration.CityID,
                    StateID = registration.StateID,
                    Type = registration.Type,
                    CreatedOn = registration.CreatedOn.ToString(),
                    UpdatedOn = registration.UpdatedOn.ToString()

                };

                returnValues.SetReturnValues(false, ErrorCodes.Ok, Utils.GetEnumDescription(ErrorCodes.Ok));
            }
            catch (Exception ex)
            {
                returnValues.SetReturnValues(true, ErrorCodes.InternalError, ex.Message);
            }
            return registrationsDTO;
        }

        public RegistrationsDTO GetRegistrationsByPassword(RegistrationsParametersPassword registrationsParametersPassword, out ReturnValues returnValues)
        {
            Registration registration;
            RegistrationsDTO registrationsDTO = null;
            returnValues = new ReturnValues();

            try
            {
                registrationsParametersPassword.Password = Utils.Encrypt(registrationsParametersPassword.Password);

                registration = _unitOfWork.RegistrationRepository.Get(row => row.Username == registrationsParametersPassword.UserName);
                if (registration == null)
                {
                    returnValues.SetReturnValues(true, ErrorCodes.InvalidUserPassword, Utils.GetEnumDescription(ErrorCodes.InvalidUserPassword));
                    return registrationsDTO;
                }
                if (registration.Password != registrationsParametersPassword.Password)
                {
                    returnValues.SetReturnValues(true, ErrorCodes.InvalidUserPassword, Utils.GetEnumDescription(ErrorCodes.InvalidUserPassword));
                    return registrationsDTO;
                }

                registrationsDTO = new RegistrationsDTO
                {
                    ID = registration.ID.ToString(),
                    Status = registration.Status.ToString(),
                    Name = registration.Name,
                    Email = registration.Email,
                    CPFCNPJ = registration.CPFCNPJ,
                    Phone = registration.Phone,
                    Username = registration.Username,
                    Password = registration.Password,
                    CEP = registration.CEP,
                    Address = registration.Address,
                    Number = registration.Number,
                    DistrictID = registration.DistrictID,
                    CityID = registration.CityID,
                    StateID = registration.StateID,
                    Type = registration.Type,
                    CreatedOn = registration.CreatedOn.ToString(),
                    UpdatedOn = registration.UpdatedOn.ToString()
                };

                returnValues.SetReturnValues(false, ErrorCodes.Ok, Utils.GetEnumDescription(ErrorCodes.Ok));
            }
            catch (Exception ex)
            {
                returnValues.SetReturnValues(true, ErrorCodes.InternalError, ex.Message);
            }
            return registrationsDTO;
        }

        public RegistrationsDTO CreateRegistrations(RegistrationsParametersCreate registrationsParametersCreate, out ReturnValues returnValues)
        {
            #region Parameters

            Registration registration;
            RegistrationsDTO registrationsDTO = null;
            returnValues = new ReturnValues();

            #endregion

            
                if (!RegistrationExistEmail(registrationsParametersCreate.Email))
                {
                    try
                    {
                        registration = new Registration()
                        {
                            Status = true,
                            Name = registrationsParametersCreate.Name,
                            Email = registrationsParametersCreate.Email,
                            CPFCNPJ = registrationsParametersCreate.CPFCNPJ,
                            Phone = registrationsParametersCreate.Phone,
                            Username = registrationsParametersCreate.Email,
                            Password = Utils.Encrypt(registrationsParametersCreate.Password),
                            CEP = registrationsParametersCreate.CEP,
                            Address = registrationsParametersCreate.Address,
                            Number = registrationsParametersCreate.Number,
                            DistrictID = registrationsParametersCreate.DistrictID,
                            CityID = registrationsParametersCreate.CityID,
                            StateID = registrationsParametersCreate.StateID,
                            Type = "CLI",
                            CreatedOn = DateTime.Now

                        };

                        _unitOfWork.RegistrationRepository.Insert(registration);
                        _unitOfWork.PersistChanges();

                        registrationsDTO = new RegistrationsDTO
                        {
                            ID = registration.ID.ToString(),
                            Status = registration.Status.ToString(),
                            Name = registration.Name,
                            Email = registration.Email,
                            CPFCNPJ = registration.CPFCNPJ,
                            Phone = registration.Phone,
                            Username = registration.Username,
                            Password = registration.Password,
                            CEP = registration.CEP,
                            Address = registration.Address,
                            Number = registration.Number,
                            DistrictID = registration.DistrictID,
                            CityID = registration.CityID,
                            StateID = registration.StateID,
                            Type = registration.Type,
                            CreatedOn = registration.CreatedOn.ToString(),
                            UpdatedOn = registration.UpdatedOn.ToString()
                        };

                        returnValues.SetReturnValues(false, ErrorCodes.Ok, Utils.GetEnumDescription(ErrorCodes.Ok));
                    }
                    catch (Exception ex)
                    {
                        returnValues.SetReturnValues(true, ErrorCodes.InternalError, ex.Message + " inner --> " + ex.InnerException);
                    }
                }
                else
                {
                    returnValues.SetReturnValues(false, ErrorCodes.Ok, Utils.GetEnumDescription(ErrorCodes.UserExist));
                }

            return registrationsDTO;
        }

        public RegistrationsDTO UpdateRegistrationsbyID(RegistrationsParametersUpdate usersParametersUpdate, out ReturnValues returnValues)
        {
            Registration registration;
            RegistrationsDTO registrationsDTO = null;
            returnValues = new ReturnValues();
            int ID = Convert.ToInt32(usersParametersUpdate.ID);

            try
            {
                registration = _unitOfWork.RegistrationRepository.Get(row => row.ID == ID);
                if (registration == null)
                {
                    returnValues.SetReturnValues(true, ErrorCodes.Ok, Utils.GetEnumDescription(ErrorCodes.NotFound));
                    return registrationsDTO;
                }

                registration.Name = usersParametersUpdate.Name;
                registration.Email = usersParametersUpdate.Email;
                registration.CPFCNPJ = usersParametersUpdate.CPFCNPJ;
                registration.Phone = usersParametersUpdate.Phone;
                registration.Password = Utils.Encrypt(usersParametersUpdate.Password);
                registration.CEP = usersParametersUpdate.CEP;
                registration.Address = usersParametersUpdate.Address;
                registration.Number = usersParametersUpdate.Number;
                registration.DistrictID = usersParametersUpdate.DistrictID;
                registration.CityID = usersParametersUpdate.CityID;
                registration.StateID = usersParametersUpdate.StateID;
                registration.UpdatedOn = DateTime.Now;

                _unitOfWork.RegistrationRepository.Update(registration);
                _unitOfWork.PersistChanges();

                registrationsDTO = new RegistrationsDTO
                {
                    ID = registration.ID.ToString(),
                    Status = registration.Status.ToString(),
                    Name = registration.Name,
                    Email = registration.Email,
                    CPFCNPJ = registration.CPFCNPJ,
                    Phone = registration.Phone,
                    Username = registration.Username,
                    Password = registration.Password,
                    CEP = registration.CEP,
                    Address = registration.Address,
                    Number = registration.Number,
                    DistrictID = registration.DistrictID,
                    CityID = registration.CityID,
                    StateID = registration.StateID,
                    Type = registration.Type,
                    CreatedOn = registration.CreatedOn.ToString(),
                    UpdatedOn = registration.UpdatedOn.ToString()
                };

                returnValues.SetReturnValues(false, ErrorCodes.Ok, Utils.GetEnumDescription(ErrorCodes.Ok));
            }
            catch (Exception ex)
            {
                returnValues.SetReturnValues(true, ErrorCodes.InternalError, ex.Message);
            }

            return registrationsDTO;
        }

        public RegistrationsDTO DeleteRegistrationsByID(RegistrationsParametersID registrationsParametersID, out ReturnValues returnValues)
        {
            #region Parameters

            Registration registration;
            RegistrationsDTO registrationsDTO = null;
            returnValues = new ReturnValues();
            int ID = Convert.ToInt32(registrationsParametersID.ID);

            #endregion

            try
            {
                registration = _unitOfWork.RegistrationRepository.Get(row => row.ID == ID);
                if (registration == null)
                {
                    returnValues.SetReturnValues(true, ErrorCodes.NotFound, Utils.GetEnumDescription(ErrorCodes.NotFound));
                    return registrationsDTO;
                }

                _unitOfWork.RegistrationRepository.Delete(registration);
                _unitOfWork.PersistChanges();

                registrationsDTO = new RegistrationsDTO
                {
                    ID = registration.ID.ToString(),
                };

                returnValues.SetReturnValues(false, ErrorCodes.Ok, Utils.GetEnumDescription(ErrorCodes.Ok));
            }
            catch (Exception ex)
            {
                returnValues.SetReturnValues(true, ErrorCodes.InternalError, ex.Message);
            }
            return registrationsDTO;
        }

        public IList<StatesDTO> GetStates(StateParametersGetAll stateParametersGetAll, out ReturnValues returnValues)
        {
            IList<StatesDTO> statesDTO = null;
            returnValues = new ReturnValues();

            try
            {
                var query = _unitOfWork.StateRepository.QueryableObject();

                statesDTO = query.Select(row => new StatesDTO()
                {
                    ID = row.Id,
                    Name = row.Name

                }).ToList();

                returnValues.SetReturnValues(false, ErrorCodes.Ok, Utils.GetEnumDescription(ErrorCodes.Ok));
            }
            catch (Exception ex)
            {
                returnValues.SetReturnValues(true, ErrorCodes.InternalError, ex.Message);
            }

            return statesDTO as IList<StatesDTO>;
        }

        public IList<CitysDTO> GetCitys(CityParametersGetAll cityParametersGetAll, out ReturnValues returnValues)
        {
            IList<CitysDTO> citysDTO = null;
            returnValues = new ReturnValues();

            var StateID = String.IsNullOrEmpty(cityParametersGetAll.StateID) ? "" : cityParametersGetAll.StateID;

            try
            {
                var query = _unitOfWork.CityRepository.QueryableObject();

                if (!String.IsNullOrEmpty(StateID))
                {
                    citysDTO = query
                    .Where(row => row.StateID == StateID)
                    .Select(row => new CitysDTO()
                    {
                        ID = row.Id,
                        Name = row.Name,


                    }).ToList();
                }

                returnValues.SetReturnValues(false, ErrorCodes.Ok, Utils.GetEnumDescription(ErrorCodes.Ok));
            }
            catch (Exception ex)
            {
                returnValues.SetReturnValues(true, ErrorCodes.InternalError, ex.Message);
            }

            return citysDTO as IList<CitysDTO>;
        }

        public IList<DistrictsDTO> GetDistricts(DistrictParametersGetAll districtParametersGetAll, out ReturnValues returnValues)
        {
            IList<DistrictsDTO> districtsDTO = null;
            returnValues = new ReturnValues();

            var CityID = String.IsNullOrEmpty(districtParametersGetAll.CityID) ? "" : districtParametersGetAll.CityID;

            try
            {
                var query = _unitOfWork.DistrictRepository.QueryableObject();

                if (!String.IsNullOrEmpty(CityID))
                {
                    districtsDTO = query
                    .Where(row => row.CityID == CityID)
                    .Select(row => new DistrictsDTO()
                    {
                        ID = row.Id,
                        Name = row.Name,


                    }).ToList();
                }

                returnValues.SetReturnValues(false, ErrorCodes.Ok, Utils.GetEnumDescription(ErrorCodes.Ok));
            }
            catch (Exception ex)
            {
                returnValues.SetReturnValues(true, ErrorCodes.InternalError, ex.Message);
            }

            return districtsDTO as IList<DistrictsDTO>;
        }





        #endregion

        #region PRIVATE_METHODS

        private bool RegistrationExistCPFCNPJ(string CPFCNPJ)
        {

            Registration registration;
            bool resp = false;

            registration = _unitOfWork.RegistrationRepository.Get(row => row.CPFCNPJ == CPFCNPJ);
            if (registration != null)
            {
                resp = true;
            }

            return resp;
        }

        private bool RegistrationExistEmail(string Email)
        {

            Registration registration;
            bool resp = false;

            registration = _unitOfWork.RegistrationRepository.Get(row => row.Email == Email);
            if (registration != null)
            {
                resp = true;
            }

            return resp;
        }

        #endregion
    }
}
