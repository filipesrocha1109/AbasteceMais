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
                    Username = row.Username,
                    Password = row.Password,
                    CEP = row.CEP,
                    Address = row.Address,
                    Number = row.Number,
                    District = row.District,
                    City = row.City,
                    State = row.State,
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
                    Username = registration.Username,
                    Password = registration.Password,
                    CEP = registration.CEP,
                    Address = registration.Address,
                    Number = registration.Number,
                    District = registration.District,
                    City = registration.City,
                    State = registration.State,
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
                    Username = registration.Username,
                    Password = registration.Password,
                    CEP = registration.CEP,
                    Address = registration.Address,
                    Number = registration.Number,
                    District = registration.District,
                    City = registration.City,
                    State = registration.State,
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

            if (!RegistrationExistCPFCNPJ(registrationsParametersCreate.CPFCNPJ))
            {
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
                            Username = registrationsParametersCreate.Email,
                            Password = Utils.Encrypt(registrationsParametersCreate.Password),
                            CEP = registrationsParametersCreate.CEP,
                            Address = registrationsParametersCreate.Address,
                            Number = registrationsParametersCreate.Number,
                            District = registrationsParametersCreate.District,
                            City = registrationsParametersCreate.City,
                            State = registrationsParametersCreate.State,
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
                            Username = registration.Username,
                            Password = registration.Password,
                            CEP = registration.CEP,
                            Address = registration.Address,
                            Number = registration.Number,
                            District = registration.District,
                            City = registration.City,
                            State = registration.State,
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
                registration.Password = Utils.Encrypt(usersParametersUpdate.Password);
                registration.CEP = usersParametersUpdate.CEP;
                registration.Address = usersParametersUpdate.Address;
                registration.Number = usersParametersUpdate.Number;
                registration.District = usersParametersUpdate.District;
                registration.City = usersParametersUpdate.City;
                registration.State = usersParametersUpdate.State;
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
                    Username = registration.Username,
                    Password = registration.Password,
                    CEP = registration.CEP,
                    Address = registration.Address,
                    Number = registration.Number,
                    District = registration.District,
                    City = registration.City,
                    State = registration.State,
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
