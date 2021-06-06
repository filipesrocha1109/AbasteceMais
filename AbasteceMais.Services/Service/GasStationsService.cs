using System;
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
using System.Globalization;
using System.Device.Location;

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
                    UpdatedOn = row.UpdatedOn.ToString(),
                    distance = ""

                }).ToList();

                foreach( var gas in gasStationsDTO)
                {

                    double lat = -30.076721; 
                    double longui = -51.031796;

                    gas.distance = getDistance(Convert.ToDouble(gas.Latitude), Convert.ToDouble(gas.Longitude), lat, longui ).ToString();
                };

                if (!String.IsNullOrEmpty(gasStationsParametersGetAll.Order))
                {
                    switch (gasStationsParametersGetAll.Order)
                    {
                        case "More Relevant":

                            switch (gasStationsParametersGetAll.TypeGas)
                            {
                                case "Gasolina Comum":
                                    gasStationsDTO = gasStationsDTO.OrderBy(o => Convert.ToDecimal(o.PriceGasolinaComum)).OrderBy(o => Convert.ToDouble(o.distance)).Take(30).ToList();
                                    break;
                                case "Gasolina Aditivada":
                                    gasStationsDTO = gasStationsDTO.OrderBy(o => Convert.ToDecimal(o.PriceGasolinaAditivada)).OrderBy(o => Convert.ToDouble(o.distance)).Take(30).ToList();
                                    break;
                                case "Disel":
                                    gasStationsDTO = gasStationsDTO.OrderBy(o => Convert.ToDecimal(o.PriceDisel)).OrderBy(o => Convert.ToDouble(o.distance)).Take(30).ToList();
                                    break;
                                case "Gás":
                                    gasStationsDTO = gasStationsDTO.OrderBy(o => Convert.ToDecimal(o.PriceGas)).OrderBy(o => Convert.ToDouble(o.distance)).Take(30).ToList();
                                    break;
                                default:
                                    gasStationsDTO = gasStationsDTO.OrderBy(o => Convert.ToDecimal(o.PriceGasolinaComum)).OrderBy(o => Convert.ToDouble(o.distance)).Take(30).ToList();
                                    break;
                            }
                            break;

                        case "Highest Price":

                            switch (gasStationsParametersGetAll.TypeGas)
                            {
                                case "Gasolina Comum":
                                    gasStationsDTO = gasStationsDTO.OrderByDescending(o => Convert.ToDecimal(o.PriceGasolinaComum)).Take(30).ToList();
                                    break;
                                case "Gasolina Aditivada":
                                    gasStationsDTO = gasStationsDTO.OrderByDescending(o => Convert.ToDecimal(o.PriceGasolinaAditivada)).Take(30).ToList();
                                    break;
                                case "Disel":
                                    gasStationsDTO = gasStationsDTO.OrderByDescending(o => Convert.ToDecimal(o.PriceDisel)).Take(30).ToList();
                                    break;
                                case "Gás":
                                    gasStationsDTO = gasStationsDTO.OrderByDescending(o => Convert.ToDecimal(o.PriceGas)).Take(30).ToList();
                                    break;
                                default:
                                    gasStationsDTO = gasStationsDTO.OrderByDescending(o => Convert.ToDecimal(o.PriceGasolinaComum)).Take(30).ToList();
                                    break;
                            }

                            break;

                        case "Lower Price":

                            switch (gasStationsParametersGetAll.TypeGas)
                            {
                                case "Gasolina Comum":
                                    gasStationsDTO = gasStationsDTO.OrderBy(o => Convert.ToDecimal(o.PriceGasolinaComum)).Take(30).ToList();
                                    break;
                                case "Gasolina Aditivada":
                                    gasStationsDTO = gasStationsDTO.OrderBy(o => Convert.ToDecimal(o.PriceGasolinaAditivada)).Take(30).ToList();
                                    break;
                                case "Disel":
                                    gasStationsDTO = gasStationsDTO.OrderBy(o => Convert.ToDecimal(o.PriceDisel)).Take(30).ToList();
                                    break;
                                case "Gás":
                                    gasStationsDTO = gasStationsDTO.OrderBy(o => Convert.ToDecimal(o.PriceGas)).Take(30).ToList();
                                    break;
                                default:
                                    gasStationsDTO = gasStationsDTO.OrderBy(o => Convert.ToDecimal(o.PriceGasolinaComum)).Take(30).ToList();
                                    break;
                            }

                            break;
                    }
                }
                else
                {
                    gasStationsDTO = gasStationsDTO.OrderBy(o => Convert.ToDecimal(o.PriceGasolinaComum)).OrderBy(o => Convert.ToDouble(o.distance)).Take(30).ToList();
                }

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


        public UpdatePricesGasStationDTO UpdatePriceGasStations(UpdatePriicesParameters updatePriicesParameters, out ReturnValues returnValues)
        {
            GasStation gasstation;
            UpdatePricesGasStation updatepricesgasstation;
            UpdatePricesGasStationDTO updatePricesGasStationDTO = null;
            returnValues = new ReturnValues();

            int GasstationID = Convert.ToInt32(updatePriicesParameters.GasstationID);

            try
            {
                gasstation = _unitOfWork.GasStationRepository.Get(row => row.ID == GasstationID);
                if (gasstation == null)
                {
                    returnValues.SetReturnValues(true, ErrorCodes.Ok, Utils.GetEnumDescription(ErrorCodes.NotFound));
                    return updatePricesGasStationDTO;

                };

                if (updatePriicesParameters.GasolinaComum > 0)
                {
                    gasstation.PriceGasolinaComum = updatePriicesParameters.GasolinaComum;                   
                };


                if (updatePriicesParameters.GasolinaAditivada > 0)
                {
                    gasstation.PriceGasolinaAditivada = updatePriicesParameters.GasolinaAditivada;
                };
               
                if (updatePriicesParameters.Gas > 0)
                {
                    gasstation.PriceGas = updatePriicesParameters.Gas;
                };

                if (updatePriicesParameters.Disel > 0)
                {
                    gasstation.PriceDisel = updatePriicesParameters.Disel;
                }


                _unitOfWork.GasStationRepository.Update(gasstation);
                _unitOfWork.PersistChanges();

                updatepricesgasstation = new UpdatePricesGasStation()
                {
                    GasStationID = updatePriicesParameters.GasstationID,
                    RegistrationID = updatePriicesParameters.RegistrationID,                  
                    PriceGasolinaComum = updatePriicesParameters.GasolinaComum > 0 ? updatePriicesParameters.GasolinaComum : 0 ,
                    PriceGasolinaAditivada = updatePriicesParameters.GasolinaAditivada > 0 ? updatePriicesParameters.GasolinaAditivada : 0,
                    PriceGas = updatePriicesParameters.Gas > 0 ? updatePriicesParameters.Gas : 0,
                    PriceDisel = updatePriicesParameters.Disel > 0 ? updatePriicesParameters.Disel : 0,
                    CreatedOn = DateTime.Now,
                };

                _unitOfWork.UpdatePricesGasStationRepository.Insert(updatepricesgasstation);
                _unitOfWork.PersistChanges();

                updatePricesGasStationDTO = new UpdatePricesGasStationDTO
                {
                    ID = updatepricesgasstation.ID.ToString()
                };

                returnValues.SetReturnValues(false, ErrorCodes.Ok, Utils.GetEnumDescription(ErrorCodes.Ok));
            }
            catch (Exception ex)
            {
                returnValues.SetReturnValues(true, ErrorCodes.InternalError, ex.Message);
            }

            return updatePricesGasStationDTO;
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

        private double getDistance(double latGas, double longiGas, double lat, double longi)
        {

            double distance = 999;

            if(latGas >= -90 && latGas <= 90 && longiGas >= -90 && longiGas <= 90 && lat >= -90 && lat <= 90 && longi >= -90 && longi <= 90)
            {

                var sCoord = new GeoCoordinate(lat, longi);

                var eCoord = new GeoCoordinate(latGas, longiGas);

                double distanceM = sCoord.GetDistanceTo(eCoord);

                distance = distanceM / 1000;
            }

            return distance;
        }
        #endregion


    }
}
