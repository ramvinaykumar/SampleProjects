using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebAPI.Proper.Request.Response.Common.AppDbContext;
using WebAPI.Proper.Request.Response.Models;
using WebAPI.Proper.Request.Response.Models.Country;
using WebAPI.Proper.Request.Response.Models.Enums;
using WebAPI.Proper.Request.Response.Repository.Helper;
using WebAPI.Proper.Request.Response.Repository.Interface;

namespace WebAPI.Proper.Request.Response.Repository.Services
{
    public class CountryDataService : ErrorsHelper, ICountryDataService
    {
        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// ApiDbContext
        /// </summary>
        private readonly ApiDbContext _context;
        private readonly ILogger _logger;

        #region Constructor

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="context">ApiDbContext context</param>
        public CountryDataService(ApiDbContext context, ILogger<CountryDataService> logger)
        {
            _context = context;
            _logger = logger;
        }

        #endregion

        #region Methods

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Get Country data as list
        /// </summary>
        /// <returns>Return Country data as list</returns>
        public async Task<GenericResponse<IEnumerable<CountryResponseDto>>> Listing()
        {
            var response = new GenericResponse<IEnumerable<CountryResponseDto>>();
            try
            {
                var listing = await _context.Country
                .Where(x => x.IsDeleted == false)
                .OrderByDescending(o => o.CountryID)
                .Select(s => new CountryResponseDto
                {
                    CountryID = s.CountryID,
                    Name = s.Name,
                    ISO3 = s.ISO3,
                    ISO2 = s.ISO2,
                    Code = s.Code,
                    PhoneCode = s.PhoneCode,
                    Capital = s.Capital,
                    Currency = s.Currency,
                    CurrencyName = s.CurrencyName,
                    CurrencySymbol = s.CurrencySymbol,
                    Region = s.Region,
                    Subregion = s.Subregion,
                    IsDeleted = s.IsDeleted == false ? "No" : "Yes"
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
                //return GetNotFoundResponse<IEnumerable<Countrys>>();
                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.Message = "Something went wrong ==>>" + ex.Message;
                response.IsValid = true;
                return response;
            }
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Create Country
        /// </summary>
        /// <param name="requestDto">CountryEntity requestDto</param>
        /// <returns>Returns the response message</returns>
        public async Task<GenericResponse<CountryResponseDto>> Create(CountryEntity requestDto)
        {
            var response = new GenericResponse<CountryResponseDto>();

            try
            {
                if (requestDto == null)
                {
                    return GetErrorResponse<CountryResponseDto>(new ErrorMessage
                    {
                        ErrorType = ErrorType.NullException
                    }, StatusCodes.Status400BadRequest);
                }

                var dataExists = await _context.Country.FirstOrDefaultAsync(x => x.Name.ToLower() == requestDto.Name.ToLower()
                                                && x.Code == requestDto.Code && x.IsDeleted == false);
                if (dataExists == null)
                {
                    await _context.AddAsync(requestDto);
                    var status = await _context.SaveChangesAsync();

                    if (status == 1)
                    {
                        response.Count = status;
                        response.Result = await GetCountryById(requestDto.CountryID);
                        response.IsValid = response.Result != null;
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.Result = null;
                        response.Message = "Country data : "+ requestDto.Name + " has been added successfully!";
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
                else
                {
                    response.StatusCode = (int)HttpStatusCode.Conflict;
                    response.Message = "Country data:- " + dataExists.Name + ", " + dataExists.Code + " already exist.";
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
        /// Update Country
        /// </summary>
        /// <param name="requestDto">CountryEntity requestDto</param>
        /// <returns>Returns the response message</returns>
        public async Task<GenericResponse<CountryResponseDto>> Update(CountryEntity requestDto)
        {
            var response = new GenericResponse<CountryResponseDto>();

            try
            {
                if (requestDto == null)
                {
                    return GetErrorResponse<CountryResponseDto>(new ErrorMessage
                    {
                        ErrorType = ErrorType.NullException
                    }, StatusCodes.Status400BadRequest);
                }

                if (await _context.Country.AnyAsync(x => x.CountryID == requestDto.CountryID))
                {
                    var updateData = await _context.Country.Where(x => x.CountryID == requestDto.CountryID).FirstOrDefaultAsync();
                    if (updateData != null)
                    {
                        _context.Update(requestDto);
                    }

                    var status = await _context.SaveChangesAsync();
                    if (status == 1)
                    {
                        response.Result = await GetCountryById(requestDto.CountryID);
                        response.Count = status;
                        response.IsValid = response.Result != null;
                        response.StatusCode = StatusCodes.Status200OK;
                        response.Message = "Country data : "+ requestDto.Name +" updated successfully!";
                        return response;
                    }
                    else
                    {
                        return GetNotFoundResponse<CountryResponseDto>();
                    }
                }
                else
                {
                    response.Result = null;
                    response.StatusCode = StatusCodes.Status404NotFound;
                    response.Message = "Country data not available!";
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
        /// Delete Country by ID
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

                if (await _context.Country.AnyAsync(x => x.CountryID == Id))
                {
                    var country = await _context.Country.Where(x => x.CountryID == Id).FirstOrDefaultAsync();
                    if (country != null && country.IsDeleted)
                    {
                        response.Count = 0;
                        response.Result = false;
                        response.StatusCode = StatusCodes.Status404NotFound;
                        response.Message = "Country data doesn't exist!";
                        return response;
                    }
                    else
                    {
                        country.IsDeleted = true;
                        _context.Update(country);
                        var status = await _context.SaveChangesAsync();
                        
                        if (status > 0)
                        {
                            response.Count = status;
                            response.IsValid = true;
                            response.StatusCode = StatusCodes.Status200OK;
                            response.Result = true;
                            response.Message = "Country data deleted successfully!";
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
                    response.Message = "Country data doesn't exist!";
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

        #endregion

        private async Task<CountryResponseDto> GetCountryById(int countryID)
        {
            return await _context.Country
                .Where(x => x.IsDeleted == false && x.CountryID == countryID)
                .Select(s => new CountryResponseDto
                {
                    CountryID = s.CountryID,
                    Name = s.Name,
                    ISO3 = s.ISO3,
                    ISO2 = s.ISO2,
                    Code = s.Code,
                    PhoneCode = s.PhoneCode,
                    Capital = s.Capital,
                    Currency = s.Currency,
                    CurrencyName = s.CurrencyName,
                    CurrencySymbol = s.CurrencySymbol,
                    Region = s.Region,
                    Subregion = s.Subregion,
                    IsDeleted = s.IsDeleted == false ? "No" : "Yes"
                }).FirstOrDefaultAsync();
        }
    }
}
