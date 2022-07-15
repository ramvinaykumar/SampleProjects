using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebAPI.Proper.Request.Response.Common.AppDbContext;
using WebAPI.Proper.Request.Response.Models;
using WebAPI.Proper.Request.Response.Models.Campaign;
using WebAPI.Proper.Request.Response.Models.Enums;
using WebAPI.Proper.Request.Response.Repository.Helper;
using WebAPI.Proper.Request.Response.Repository.Interface;

namespace WebAPI.Proper.Request.Response.Repository.Services
{
    public class CampaignService : ErrorsHelper, ICampaignService
    {
        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// ApiDbContext
        /// </summary>
        private readonly ApiDbContext _context;

        #region Constructor

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="context">ApiDbContext context</param>
        public CampaignService(ApiDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Methods

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Get Campaign data as list
        /// </summary>
        /// <returns>Return Campaign data as list</returns>
        public async Task<GenericResponse<IEnumerable<Campaigns>>> Listing()
        {
            var response = new GenericResponse<IEnumerable<Campaigns>>();
            try
            {
                var listing = await _context.Campaign
                .Where(x => x.IsDeleted == false)
                .OrderByDescending(o => o.ID)
                .Select(s => new Campaigns
                {
                    ID = s.ID,
                    Name = s.Name,
                    Audience = s.Audience,
                    Region = s.Region,
                    StartDate = s.StartDate,
                    EndDate = s.EndDate,
                    Language = s.Language,
                    Message = s.Message,
                    CreatedBy = s.CreatedBy,
                    IsDeleted = s.IsDeleted,
                    IsActive = s.IsActive,
                    CreatedDate = s.CreatedDate,
                    UpdatedDate = s.UpdatedDate == null ? DateTime.Now : s.UpdatedDate
                }).ToListAsync();

                if (listing != null && listing.Any())
                {
                    response.Count = listing.Count();
                    response.Result = listing;
                    response.IsValid = true;
                    response.StatusCode = StatusCodes.Status200OK;
                    response.Message = "Success!";
                }
                return response;
            }
            catch (Exception ex)
            {
                //return GetNotFoundResponse<IEnumerable<Campaign>>();
                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.Message = "Something went wrong ==>>" + ex.Message;
                response.IsValid = true;
                return response;
            }
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Create Campaign
        /// </summary>
        /// <param name="addCampaign">AddCampaignDTO addCampaign</param>
        /// <returns>Returns the response message</returns>
        public async Task<GenericResponse<Campaigns>> Create(AddCampaignDTO addCampaign)
        {
            var response = new GenericResponse<Campaigns>();

            try
            {
                if (addCampaign == null)
                {
                    return GetErrorResponse<Campaigns>(new ErrorMessage
                    {
                        ErrorType = ErrorType.NullException
                    }, StatusCodes.Status400BadRequest);
                }

                await _context.Campaign.AddAsync(new Campaigns
                {
                    Name = addCampaign.Name,
                    Audience = GetEnumName(typeof(Audience), addCampaign.Audience),
                    Language = GetEnumName(typeof(Language), addCampaign.Language),
                    Region = GetEnumName(typeof(Region), addCampaign.Region),
                    StartDate = addCampaign.StartDate.Value,
                    EndDate = addCampaign.EndDate.Value,
                    Message = addCampaign.Message,
                    CreatedBy = addCampaign.CreatedBy,
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false
                });
                var status = await _context.SaveChangesAsync();
                if (status == 1)
                {
                    response.Count = status;
                    response.Result = await _context.Campaign.Select(x => x).OrderByDescending(o => o.ID).FirstOrDefaultAsync();
                    response.IsValid = response.Result != null;
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "Campaign data has been added successfully!";
                    return response;
                }
                else
                {
                    response.Count = status;
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response.Message = "Something went wrong!";
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
                response.IsValid = false;
                return response;
            }
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Update Campaign
        /// </summary>
        /// <param name="updateCampaign">UpdateCampaignDTO updateCampaign</param>
        /// <returns>Returns the response message</returns>
        public async Task<GenericResponse<Campaigns>> Update(UpdateCampaignDTO updateCampaign)
        {
            var response = new GenericResponse<Campaigns>();

            try
            {
                if (updateCampaign == null)
                {
                    return GetErrorResponse<Campaigns>(new ErrorMessage
                    {
                        ErrorType = ErrorType.NullException
                    }, StatusCodes.Status400BadRequest);
                }

                if (await _context.Campaign.AnyAsync(x => x.ID == updateCampaign.ID))
                {
                    var updateData = await _context.Campaign.Where(x => x.ID == updateCampaign.ID).FirstOrDefaultAsync();
                    if (updateData != null)
                    {
                        updateData.Name = updateCampaign.Name;
                        updateData.Audience = GetEnumName(typeof(Audience), updateCampaign.Audience);
                        updateData.Language = GetEnumName(typeof(Language), updateCampaign.Language);
                        updateData.Region = GetEnumName(typeof(Region), updateCampaign.Region);
                        updateData.StartDate = updateCampaign.StartDate.Value;
                        updateData.EndDate = updateCampaign.EndDate.Value;
                        updateData.Message = updateCampaign.Message;
                        updateData.UpdatedBy = updateCampaign.UpdatedBy;
                        updateData.UpdatedDate = DateTime.Now;
                        updateData.IsActive = updateCampaign.IsActive;
                    }

                    var status = await _context.SaveChangesAsync();
                    if (status == 1)
                    {
                        response.Result = await _context.Campaign.Where(x => x.ID == updateCampaign.ID).Select(s => s).FirstOrDefaultAsync();
                        response.Count = status;
                        response.IsValid = response.Result != null;
                        response.StatusCode = StatusCodes.Status200OK;
                        response.Message = "Campaign data updated successfully!";
                        return response;
                    }
                    else
                    {
                        return GetNotFoundResponse<Campaigns>();
                    }
                }
                else
                {
                    response.Result = null;
                    response.StatusCode = StatusCodes.Status404NotFound;
                    response.Message = "Campaign data not available!";
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.Message = ex.Message;
                return response;
            }
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Delete Campaign by ID
        /// </summary>
        /// <param name="Id">int Id</param>
        /// <returns>Returns the response message</returns>
        public async Task<GenericResponse<bool>> Delete(int Id)
        {
            var response = new GenericResponse<bool>();
            try
            {
                if (Id == 0)
                {
                    return GetErrorResponse<bool>(new ErrorMessage
                    {
                        ErrorType = ErrorType.NullException
                    }, StatusCodes.Status400BadRequest);
                }

                if (await _context.Campaign.AnyAsync(x => x.ID == Id))
                {
                    var Campaign = await _context.Campaign.Where(x => x.ID == Id).FirstOrDefaultAsync();
                    if (Campaign != null && Campaign.IsDeleted)
                    {
                        response.Count = 0;
                        response.Result = false;
                        response.StatusCode = StatusCodes.Status404NotFound;
                        response.Message = "Campaign data doesn't exist!";
                        return response;
                    }
                    else
                    {
                        Campaign.IsDeleted = true;
                        Campaign.UpdatedDate = DateTime.Now;

                        var status = await _context.SaveChangesAsync();
                        if (status > 0)
                        {
                            response.Count = status;
                            response.IsValid = true;
                            response.StatusCode = StatusCodes.Status200OK;
                            response.Result = true;
                            response.Message = "Campaign data deleted successfully!";
                            return response;
                        }
                        else
                        {
                            return GetNotFoundResponse<bool>();
                        }
                    }
                }
                else
                {
                    response.Result = false;
                    response.StatusCode = StatusCodes.Status404NotFound;
                    response.Message = "Campaign data doesn't exist!";
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.Message = "Something went wrong" + ex.Message;
                return response;
            }
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Activate Campaign by ID
        /// </summary>
        /// <param name="Id">int Id</param>
        public async Task<GenericResponse<bool>> Activate(int Id)
        {
            var response = new GenericResponse<bool>();
            try
            {
                if (Id == 0)
                {
                    return GetErrorResponse<bool>(new ErrorMessage
                    {
                        ErrorType = ErrorType.NullException
                    }, StatusCodes.Status400BadRequest);
                }

                if (await _context.Campaign.AnyAsync(x => x.ID == Id && x.IsDeleted == false))
                {
                    var activeData = await _context.Campaign.Where(x => x.ID == Id).FirstOrDefaultAsync();
                    if (activeData.IsActive)
                    {
                        response.Count = 0;
                        response.Result = false;
                        response.IsValid = false;
                        response.StatusCode = StatusCodes.Status409Conflict;
                        response.Message = "Campaign data Already Active!";
                        return response;
                    }
                    else
                    {
                        activeData.IsActive = true;
                        activeData.UpdatedDate = DateTime.UtcNow;

                        var status = await _context.SaveChangesAsync();
                        if (status == 1)
                        {
                            response.Count = status;
                            response.IsValid = true;
                            response.StatusCode = StatusCodes.Status200OK;
                            response.Result = true;
                            response.Message = "Campaign data has been activated!";
                            return response;
                        }
                        else
                        {
                            response.StatusCode = StatusCodes.Status500InternalServerError;
                            response.Message = "Something went wrong!";
                            response.IsValid = false;
                            return response;
                        }
                    }
                }
                else
                {
                    response.Result = false;
                    response.StatusCode = StatusCodes.Status404NotFound;
                    response.Message = "Campaign data not exists!";
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.Message = "Something went wrong " + ex.Message;
                response.IsValid = false;
                return response;
            }
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Deactivate Campaign by ID
        /// </summary>
        /// <param name="Id">int Id</param>
        public async Task<GenericResponse<bool>> Deactivate(int Id)
        {
            var response = new GenericResponse<bool>();
            if (Id == 0)
            {
                return GetErrorResponse<bool>(new ErrorMessage
                {
                    ErrorType = ErrorType.NullException
                }, StatusCodes.Status400BadRequest);
            }

            if (await _context.Campaign.AnyAsync(x => x.ID == Id && x.IsDeleted == false))
            {
                var objData = await _context.Campaign.Where(x => x.ID == Id).FirstOrDefaultAsync();
                if (objData != null && !objData.IsActive)
                {
                    response.Count = 0;
                    response.Result = false;
                    response.StatusCode = StatusCodes.Status409Conflict;
                    response.Message = "Campaign data already deactive!";
                    return response;
                }
                else
                {
                    objData.IsActive = false;
                    objData.UpdatedDate = DateTime.UtcNow;

                    var status = await _context.SaveChangesAsync();
                    if (status > 0)
                    {
                        response.Count = status;
                        response.IsValid = true;
                        response.StatusCode = StatusCodes.Status200OK;
                        response.Result = true;
                        response.Message = "Campaign data DeActivated!";
                        return response;
                    }
                    else
                    {
                        response.StatusCode = StatusCodes.Status500InternalServerError;
                        response.Message = "Something went wrong!";
                        response.IsValid = false;
                        return response;
                    }
                }
            }
            else
            {
                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.Message = "Something went wrong!";
                response.IsValid = false;
                return response;
            }
        }

        #endregion

        private string GetEnumName(Type enumType, object value)
        {
            return Enum.GetName(enumType, value);
        }

    }
}
