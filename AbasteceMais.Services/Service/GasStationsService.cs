﻿using System;
using AbasteceMais.CrossCutting.Utils;
using AbasteceMais.Domain.Enums;
using AbasteceMais.Domain.Interfaces.Services;
using AbasteceMais.Domain.Interfaces.UnitOfWork;
using AbasteceMais.Domain.DTOs.GasStation;
using AbasteceMais.Domain.Common;
using AbasteceMais.Domain.Common.GasStations;
using AbasteceMais.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace AbasteceMais.Services.Service
{
    public class GasStationsService : IGasStationsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GasStationsService
            (
            IUnitOfWork unitOfWork
            )
        {
            _unitOfWork = unitOfWork;
        }
        #region PUBLIC_METHODS

        public IList<GasStationsDTO> GetGasStations(GasStationsParametersGetAll gasStationsParametersGetAll, out ReturnValues returnValues)
        {
            IList<GasStationsDTO> gasStationsDTO = null;
            returnValues = new ReturnValues();

            try
            {
                var query = _unitOfWork.GasStationRepository.QueryableObject();

                if (!String.IsNullOrEmpty(gasStationsParametersGetAll.Name))
                {
                    query = query.Where(row => row.Name.Contains(gasStationsParametersGetAll.Name));
                }

                if (!String.IsNullOrEmpty(gasStationsParametersGetAll.TypeGas))
                {
                    switch (gasStationsParametersGetAll.TypeGas)
                    {
                        case "Gasolina Comum":
                            query = query.Where(row => row.GasolinaComum == true);
                            break;
                        case "Gasolina Aditivada":
                            query = query.Where(row => row.GasolinaAditivada == true);
                            break;
                        case "Disel":
                            query = query.Where(row => row.Disel == true);
                            break;
                        case "Gás":
                            query = query.Where(row => row.Gas == true);
                            break;
                       
                    }
                }

                if (!String.IsNullOrEmpty(gasStationsParametersGetAll.DistrictID))
                {
                    query = query.Where(row => row.DistrictID == gasStationsParametersGetAll.DistrictID);
                }
                if (!String.IsNullOrEmpty(gasStationsParametersGetAll.Order))
                {
                    switch (gasStationsParametersGetAll.Order)
                    {
                        case "More Relevant":
                            query = query.OrderBy(row => row.GasolinaComum);
                            break;

                        case "Highest Price":

                            if (!String.IsNullOrEmpty(gasStationsParametersGetAll.TypeGas))
                            {
                                switch (gasStationsParametersGetAll.TypeGas)
                                {
                                    case "Gasolina Comum":
                                        query = query.OrderByDescending(row => row.PriceGasolinaComum);
                                        break;
                                    case "Gasolina Aditivada":
                                        query = query.OrderByDescending(row => row.PriceGasolinaAditivada);
                                        break;
                                    case "Disel":
                                        query = query.OrderByDescending(row => row.PriceDisel);
                                        break;
                                    case "Gás":
                                        query = query.OrderByDescending(row => row.PriceGas);
                                        break;
                                    default:
                                        query = query.OrderByDescending(row => row.PriceGasolinaComum);
                                        break;

                                }
                            }
                            else
                            {
                                query = query.OrderByDescending(row => row.PriceGasolinaComum);
                            }

                            break;

                        case "Lower Price":

                            if (!String.IsNullOrEmpty(gasStationsParametersGetAll.TypeGas))
                            {
                                switch (gasStationsParametersGetAll.TypeGas)
                                {
                                    case "Gasolina Comum":
                                        query = query.OrderBy(row => row.PriceGasolinaComum);
                                        break;
                                    case "Gasolina Aditivada":
                                        query = query.OrderBy(row => row.PriceGasolinaAditivada);
                                        break;
                                    case "Disel":
                                        query = query.OrderBy(row => row.PriceDisel);
                                        break;
                                    case "Gás":
                                        query = query.OrderBy(row => row.PriceGas);
                                        break;
                                    default:
                                        query = query.OrderBy(row => row.PriceGasolinaComum);
                                        break;

                                }
                            }
                            else
                            {
                                query = query.OrderBy(row => row.PriceGasolinaComum);
                            }

                            break;

                        default:
                            query = query.OrderBy(row => row.PriceGasolinaComum);
                            break;

                    }
                }
                else
                {
                    query = query.OrderBy(row => row.PriceGasolinaComum);
                }

                gasStationsDTO = query.Select(row => new GasStationsDTO()
                {
                    ID = row.ID.ToString(),
                    Status = row.Status.ToString(),
                    Name = row.Name,
                    Phone = row.Phone,
                    GasolinaComum = row.GasolinaComum.ToString(),
                    GasolinaAditivada = row.GasolinaAditivada.ToString(),
                    Disel = row.Disel.ToString(),
                    Gas = row.Gas.ToString(),
                    PriceGasolinaComum = row.PriceGasolinaComum.ToString(),
                    PriceGasolinaAditivada = row.PriceGasolinaAditivada.ToString(),
                    PriceDisel = row.PriceDisel.ToString(),
                    PriceGas = row.PriceGas.ToString(),
                    Latitude = row.Latitude,
                    Longitude = row.Longitude,
                    CEP = row.CEP,
                    Address = row.Address,
                    Number = row.Number,
                    DistrictID = row.DistrictID,
                    CityID = row.CityID,
                    StateID = row.StateID,
                    CreatedOn = row.CreatedOn.ToString(),
                    UpdatedOn = row.UpdatedOn.ToString()

                }).Take(20).ToList();

                returnValues.SetReturnValues(false, ErrorCodes.Ok, Utils.GetEnumDescription(ErrorCodes.Ok));
            }
            catch (Exception ex)
            {
                returnValues.SetReturnValues(true, ErrorCodes.InternalError, ex.Message);
            }

            return gasStationsDTO as IList<GasStationsDTO>;
        }

        public GasStationsDTO GetGasStationsByID(GasStationsParametersID gasStationsParametersID, out ReturnValues returnValues)
        {

            GasStation gasStation;
            GasStationsDTO gasStationsDTO = null;
            returnValues = new ReturnValues();
            int ID = Convert.ToInt32(gasStationsParametersID.ID);

            try
            {
                gasStation = _unitOfWork.GasStationRepository.Get(row => row.ID == ID);
                if (gasStation == null)
                {
                    returnValues.SetReturnValues(false, ErrorCodes.Ok, Utils.GetEnumDescription(ErrorCodes.NotFound));
                    return gasStationsDTO;
                }

                gasStationsDTO = new GasStationsDTO
                {
                    ID = gasStation.ID.ToString(),
                    Status = gasStation.Status.ToString(),
                    Name = gasStation.Name,
                    Phone = gasStation.Phone,
                    GasolinaComum = gasStation.GasolinaComum.ToString(),
                    GasolinaAditivada = gasStation.GasolinaAditivada.ToString(),
                    Disel = gasStation.Disel.ToString(),
                    Gas = gasStation.Gas.ToString(),
                    PriceGasolinaComum = gasStation.PriceGasolinaComum.ToString(),
                    PriceGasolinaAditivada = gasStation.PriceGasolinaAditivada.ToString(),
                    PriceDisel = gasStation.PriceDisel.ToString(),
                    PriceGas = gasStation.PriceGas.ToString(),
                    Latitude = gasStation.Latitude,
                    Longitude = gasStation.Longitude,
                    CEP = gasStation.CEP,
                    Address = gasStation.Address,
                    Number = gasStation.Number,
                    DistrictID = gasStation.DistrictID,
                    CityID = gasStation.CityID,
                    StateID = gasStation.StateID,
                    CreatedOn = gasStation.CreatedOn.ToString(),
                    UpdatedOn = gasStation.UpdatedOn.ToString()

                };

                returnValues.SetReturnValues(false, ErrorCodes.Ok, Utils.GetEnumDescription(ErrorCodes.Ok));
            }
            catch (Exception ex)
            {
                returnValues.SetReturnValues(true, ErrorCodes.InternalError, ex.Message);
            }
            return gasStationsDTO;
        }

        public GasStationsDTO CreateGasStations(GasStationsParametersCreate gasStationsParametersCreate, out ReturnValues returnValues)
        {
            #region Parameters

            GasStation gasStation;
            GasStationsDTO gasStationsDTO = null;
            returnValues = new ReturnValues();

            #endregion

            try
            {
                gasStation = new GasStation()
                {
                    Status = true,
                    Name = gasStationsParametersCreate.Name,
                    Phone = string.IsNullOrEmpty(gasStationsParametersCreate.Phone) ? null : gasStationsParametersCreate.Phone,
                    GasolinaComum = string.IsNullOrEmpty(gasStationsParametersCreate.GasolinaComum) ? false :  Convert.ToBoolean(gasStationsParametersCreate.GasolinaComum) ,
                    GasolinaAditivada = string.IsNullOrEmpty(gasStationsParametersCreate.GasolinaAditivada) ? false : Convert.ToBoolean(gasStationsParametersCreate.GasolinaAditivada),
                    Disel = string.IsNullOrEmpty(gasStationsParametersCreate.Disel) ? false : Convert.ToBoolean(gasStationsParametersCreate.Disel),
                    Gas = string.IsNullOrEmpty(gasStationsParametersCreate.Gas) ? false : Convert.ToBoolean(gasStationsParametersCreate.Gas),
                    PriceGasolinaComum = string.IsNullOrEmpty(gasStationsParametersCreate.PriceGasolinaComum) ? 0 : Math.Round(Convert.ToDecimal(gasStationsParametersCreate.PriceGasolinaComum),2),
                    PriceGasolinaAditivada = string.IsNullOrEmpty(gasStationsParametersCreate.PriceGasolinaAditivada) ? 0 : Math.Round(Convert.ToDecimal(gasStationsParametersCreate.PriceGasolinaAditivada), 2),
                    PriceDisel = string.IsNullOrEmpty(gasStationsParametersCreate.PriceDisel) ? 0 : Math.Round(Convert.ToDecimal(gasStationsParametersCreate.PriceDisel), 2),
                    PriceGas = string.IsNullOrEmpty(gasStationsParametersCreate.PriceGas) ? 0 : Math.Round(Convert.ToDecimal(gasStationsParametersCreate.PriceGas), 2),
                    Latitude = string.IsNullOrEmpty(gasStationsParametersCreate.Latitude) ? null : gasStationsParametersCreate.Latitude,
                    Longitude = string.IsNullOrEmpty(gasStationsParametersCreate.Longitude) ? null : gasStationsParametersCreate.Longitude,
                    CEP = string.IsNullOrEmpty(gasStationsParametersCreate.CEP) ? null : gasStationsParametersCreate.CEP,
                    Address = gasStationsParametersCreate.Address,
                    Number = gasStationsParametersCreate.Number,
                    DistrictID = gasStationsParametersCreate.DistrictID,
                    CityID = gasStationsParametersCreate.CityID,
                    StateID = gasStationsParametersCreate.StateID,
                    CreatedOn = DateTime.Now
                    
                };

                _unitOfWork.GasStationRepository.Insert(gasStation);
                _unitOfWork.PersistChanges();

                gasStationsDTO = new GasStationsDTO
                {
                    ID = gasStation.ID.ToString(),
                    Status = gasStation.Status.ToString(),
                    Name = gasStation.Name,
                    Phone = gasStation.Phone,
                    GasolinaComum = gasStation.GasolinaComum.ToString(),
                    GasolinaAditivada = gasStation.GasolinaAditivada.ToString(),
                    Disel = gasStation.Disel.ToString(),
                    Gas = gasStation.Gas.ToString(),
                    PriceGasolinaComum = gasStation.PriceGasolinaComum.ToString(),
                    PriceGasolinaAditivada = gasStation.PriceGasolinaAditivada.ToString(),
                    PriceDisel = gasStation.PriceDisel.ToString(),
                    PriceGas = gasStation.PriceGas.ToString(),
                    Latitude = gasStation.Latitude,
                    Longitude = gasStation.Longitude,
                    CEP = gasStation.CEP,
                    Address = gasStation.Address,
                    Number = gasStation.Number,
                    DistrictID = gasStation.DistrictID,
                    CityID = gasStation.CityID,
                    StateID = gasStation.StateID,
                    CreatedOn = gasStation.CreatedOn.ToString(),
                    UpdatedOn = gasStation.UpdatedOn.ToString(),

                };

                returnValues.SetReturnValues(false, ErrorCodes.Ok, Utils.GetEnumDescription(ErrorCodes.Ok));
            }
            catch (Exception ex)
            {
                returnValues.SetReturnValues(true, ErrorCodes.InternalError, ex.Message + " inner --> " + ex.InnerException);
            }
                

            return gasStationsDTO;
        }

        //public RegistrationsDTO UpdateRegistrationsbyID(RegistrationsParametersUpdate usersParametersUpdate, out ReturnValues returnValues)
        //{
        //    Registration registration;
        //    RegistrationsDTO registrationsDTO = null;
        //    returnValues = new ReturnValues();
        //    int ID = Convert.ToInt32(usersParametersUpdate.ID);

        //    try
        //    {
        //        registration = _unitOfWork.RegistrationRepository.Get(row => row.ID == ID);
        //        if (registration == null)
        //        {
        //            returnValues.SetReturnValues(true, ErrorCodes.Ok, Utils.GetEnumDescription(ErrorCodes.NotFound));
        //            return registrationsDTO;
        //        }

        //        registration.Name = usersParametersUpdate.Name;
        //        registration.Email = usersParametersUpdate.Email;
        //        registration.CPFCNPJ = usersParametersUpdate.CPFCNPJ;
        //        registration.Phone = usersParametersUpdate.Phone;
        //        registration.Password = Utils.Encrypt(usersParametersUpdate.Password);
        //        registration.CEP = usersParametersUpdate.CEP;
        //        registration.Address = usersParametersUpdate.Address;
        //        registration.Number = usersParametersUpdate.Number;
        //        registration.DistrictID = usersParametersUpdate.DistrictID;
        //        registration.CityID = usersParametersUpdate.CityID;
        //        registration.StateID = usersParametersUpdate.StateID;
        //        registration.UpdatedOn = DateTime.Now;

        //        _unitOfWork.RegistrationRepository.Update(registration);
        //        _unitOfWork.PersistChanges();

        //        registrationsDTO = new RegistrationsDTO
        //        {
        //            ID = registration.ID.ToString(),
        //            Status = registration.Status.ToString(),
        //            Name = registration.Name,
        //            Email = registration.Email,
        //            CPFCNPJ = registration.CPFCNPJ,
        //            Phone = registration.Phone,
        //            Username = registration.Username,
        //            Password = registration.Password,
        //            CEP = registration.CEP,
        //            Address = registration.Address,
        //            Number = registration.Number,
        //            DistrictID = registration.DistrictID,
        //            CityID = registration.CityID,
        //            StateID = registration.StateID,
        //            Type = registration.Type,
        //            CreatedOn = registration.CreatedOn.ToString(),
        //            UpdatedOn = registration.UpdatedOn.ToString()
        //        };

        //        returnValues.SetReturnValues(false, ErrorCodes.Ok, Utils.GetEnumDescription(ErrorCodes.Ok));
        //    }
        //    catch (Exception ex)
        //    {
        //        returnValues.SetReturnValues(true, ErrorCodes.InternalError, ex.Message);
        //    }

        //    return registrationsDTO;
        //}

        public GasStationsDTO DeleteGasStationsByID(GasStationsParametersID gasStationsParametersID, out ReturnValues returnValues)
        {
            #region Parameters

            GasStation gasStation;
            GasStationsDTO gasStationsDTO = null;
            returnValues = new ReturnValues();
            int ID = Convert.ToInt32(gasStationsParametersID.ID);

            #endregion

            try
            {
                gasStation = _unitOfWork.GasStationRepository.Get(row => row.ID == ID);
                if (gasStation == null)
                {
                    returnValues.SetReturnValues(true, ErrorCodes.NotFound, Utils.GetEnumDescription(ErrorCodes.NotFound));
                    return gasStationsDTO;
                }

                _unitOfWork.GasStationRepository.Delete(gasStation);
                _unitOfWork.PersistChanges();

                gasStationsDTO = new GasStationsDTO
                {
                    ID = gasStation.ID.ToString(),
                };

                returnValues.SetReturnValues(false, ErrorCodes.Ok, Utils.GetEnumDescription(ErrorCodes.Ok));
            }
            catch (Exception ex)
            {
                returnValues.SetReturnValues(true, ErrorCodes.InternalError, ex.Message);
            }
            return gasStationsDTO;
        }

        public AssessmentsDTO CreateAssessments(AssessmentsParametersCreate assessmentsParametersCreate, out ReturnValues returnValues)
        {
            #region Parameters

            Assessment assessment;
            AssessmentsDTO assessmentsDTO = null;
            returnValues = new ReturnValues();
            bool edit = EditAssessment(assessmentsParametersCreate.GasStationID, assessmentsParametersCreate.RegistrationID);
            #endregion

            if (!edit)
            {
                try
                {
                    assessment = new Assessment()
                    {
                        GasStaionID = Convert.ToInt32(assessmentsParametersCreate.GasStationID),
                        RegistrationID = Convert.ToInt32(assessmentsParametersCreate.RegistrationID),
                        Assessment1 = Convert.ToBoolean(assessmentsParametersCreate.Assessment),
                        CreatedOn = DateTime.Now

                    };

                    _unitOfWork.AssessmentRepository.Insert(assessment);
                    _unitOfWork.PersistChanges();

                    assessmentsDTO = new AssessmentsDTO
                    {
                        ID = assessment.ID.ToString(),
                        GasStaionID = assessment.GasStaionID.ToString(),
                        RegistrationID = assessment.RegistrationID.ToString(),
                        Assessment = assessment.Assessment1.ToString(),
                        CreatedOn = assessment.CreatedOn.ToString(),
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
                try
                {
                    int gasstation = Convert.ToInt32(assessmentsParametersCreate.GasStationID);
                    int registration = Convert.ToInt32(assessmentsParametersCreate.RegistrationID);

                    assessment = _unitOfWork.AssessmentRepository.Get(row => row.GasStaionID == gasstation  && row.RegistrationID == registration );
                    
                    if (assessment == null)
                    {
                        returnValues.SetReturnValues(true, ErrorCodes.Ok, Utils.GetEnumDescription(ErrorCodes.NotFound));
                        return assessmentsDTO;
                    }

                    assessment.Assessment1 = Convert.ToBoolean(assessmentsParametersCreate.Assessment);

                    _unitOfWork.AssessmentRepository.Update(assessment);
                    _unitOfWork.PersistChanges();

                    assessmentsDTO = new AssessmentsDTO
                    {
                        ID = assessment.ID.ToString(),
                        GasStaionID = assessment.GasStaionID.ToString(),
                        RegistrationID = assessment.RegistrationID.ToString(),
                        Assessment = assessment.Assessment1.ToString(),
                        CreatedOn = assessment.CreatedOn.ToString(),
                    };

                    returnValues.SetReturnValues(false, ErrorCodes.Ok, Utils.GetEnumDescription(ErrorCodes.Ok));
                }
                catch (Exception ex)
                {
                    returnValues.SetReturnValues(true, ErrorCodes.InternalError, ex.Message);
                }
            }
            return assessmentsDTO;
        }

        public IList<CommentsDTO> GetComments(CommentsGetParameters commentsGetParameters, out ReturnValues returnValues)
        {
            IList<CommentsDTO> commentsDTO = null;
            returnValues = new ReturnValues();
            int gasstaion;

            try
            {
                var query = _unitOfWork.CommentRepository.QueryableObject();

                if(!String.IsNullOrEmpty(commentsGetParameters.GasStationID))
                {
                    gasstaion = Convert.ToInt32(commentsGetParameters.GasStationID);
                    query = query.Where(row => row.GasStaionID == gasstaion);
                }

                query = query.OrderByDescending(row => row.CreatedOn);

                commentsDTO = query.Select(row => new CommentsDTO()
                {
                    ID = row.ID.ToString(),
                    GasStaionID = row.GasStaionID.ToString(),
                    RegistrationID = row.RegistrationID.ToString(),
                    Comment = row.Comment1,
                    CreatedOn = row.CreatedOn.ToString(),

                }).ToList();

                returnValues.SetReturnValues(false, ErrorCodes.Ok, Utils.GetEnumDescription(ErrorCodes.Ok));
            }
            catch (Exception ex)
            {
                returnValues.SetReturnValues(true, ErrorCodes.InternalError, ex.Message);
            }

            return commentsDTO as IList<CommentsDTO>;
        }

        public CommentsDTO CreateComments(CommentsParametersCreate commentsParametersCreate, out ReturnValues returnValues)
        {
            #region Parameters

            Comment comments;
            CommentsDTO commentsDTO = null;
            returnValues = new ReturnValues();
           
            #endregion

            try
            {
                comments = new Comment()
                {
                    GasStaionID = Convert.ToInt32(commentsParametersCreate.GasStationID),
                    RegistrationID = Convert.ToInt32(commentsParametersCreate.RegistrationID),
                    Comment1 = commentsParametersCreate.Comment,
                    CreatedOn = DateTime.Now

                };

                _unitOfWork.CommentRepository.Insert(comments);
                _unitOfWork.PersistChanges();

                commentsDTO = new CommentsDTO
                {
                    ID = comments.ID.ToString(),
                    GasStaionID = comments.GasStaionID.ToString(),
                    RegistrationID = comments.RegistrationID.ToString(),
                    Comment = comments.Comment1,
                    CreatedOn = comments.CreatedOn.ToString(),
                };

                returnValues.SetReturnValues(false, ErrorCodes.Ok, Utils.GetEnumDescription(ErrorCodes.Ok));
            }
            catch (Exception ex)
            {
                returnValues.SetReturnValues(true, ErrorCodes.InternalError, ex.Message + " inner --> " + ex.InnerException);
            }

            return commentsDTO;
        }

        public CommentsDTO DeleteCommentsByID(CommentsParametersDelete commentsParametersDelete, out ReturnValues returnValues)
        {
            #region Parameters

            Comment comments;
            CommentsDTO commentsDTO = null;
            returnValues = new ReturnValues();
            int ID = Convert.ToInt32(commentsParametersDelete.ID);
            int registration = Convert.ToInt32(commentsParametersDelete.RegistrationID);
            int gasstation = Convert.ToInt32(commentsParametersDelete.GasStationID);

            #endregion

            try
            {
                comments = _unitOfWork.CommentRepository.Get(row => row.ID == ID && row.GasStaionID == gasstation && row.RegistrationID == registration);
                if (comments == null)
                {
                    returnValues.SetReturnValues(true, ErrorCodes.NotFound, Utils.GetEnumDescription(ErrorCodes.NotFound));
                    return commentsDTO;
                }

                _unitOfWork.CommentRepository.Delete(comments);
                _unitOfWork.PersistChanges();

                commentsDTO = new CommentsDTO
                {
                    ID = comments.ID.ToString(),
                };

                returnValues.SetReturnValues(false, ErrorCodes.Ok, Utils.GetEnumDescription(ErrorCodes.Ok));
            }
            catch (Exception ex)
            {
                returnValues.SetReturnValues(true, ErrorCodes.InternalError, ex.Message);
            }
            return commentsDTO;

        }

        public StarsDTO GetGasStationStarts(GasStationsParametersID gasStationsParametersID, out ReturnValues returnValues)
        {
            StarsDTO starsDTO = null;
            returnValues = new ReturnValues();

            int ID = Convert.ToInt32(gasStationsParametersID.ID);

            var countTotal = _unitOfWork.AssessmentRepository.QueryableObject()
                .Where(row => row.GasStaionID == ID)
                .Count();

            var countPositive = _unitOfWork.AssessmentRepository.QueryableObject()
                .Where(row => row.GasStaionID == ID && row.Assessment1 == true )
                .Count();

            var countNegative = _unitOfWork.AssessmentRepository.QueryableObject()
                .Where(row => row.GasStaionID == ID && row.Assessment1 == false)
                .Count();

            starsDTO = new StarsDTO
            {
                GasStaionID = ID.ToString(),
                Total = countTotal.ToString(),
                Positive = countPositive.ToString(),
                Negative = countNegative.ToString()
            };

            return starsDTO;
        }

        #endregion

        #region PRIVATE_METHODS

        private bool EditAssessment(string gasStationID, string RegistrationID)
        {
            Assessment assessment;
            int gasstation = Convert.ToInt32(gasStationID);
            int registration = Convert.ToInt32(RegistrationID);

            bool value;

            assessment = _unitOfWork.AssessmentRepository.Get(row => row.GasStaionID == gasstation && row.RegistrationID == registration);

            if (assessment == null)
            {
                value = false;
            }
            else
            {
                value = true ;
            }

            return value;

        }
        #endregion
    }
}
