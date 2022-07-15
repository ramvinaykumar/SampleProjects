using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebAPI.Proper.Request.Response.Common.AppDbContext;
using WebAPI.Proper.Request.Response.Models;
using WebAPI.Proper.Request.Response.Models.Enums;
using WebAPI.Proper.Request.Response.Models.Students;
using WebAPI.Proper.Request.Response.Repository.Helper;
using WebAPI.Proper.Request.Response.Repository.Interface;

namespace WebAPI.Proper.Request.Response.Repository.Services
{
    public class StudentServices : ErrorsHelper, IStudentServices
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
        public StudentServices(ApiDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Methods

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Get Student data as list
        /// </summary>
        /// <returns>Return Student data as list</returns>
        public async Task<GenericResponse<IEnumerable<Students>>> Listing()
        {
            var response = new GenericResponse<IEnumerable<Students>>();
            try
            {
                var listing = await _context.Students
                .Where(x => x.IsDeleted == false)
                .OrderByDescending(o => o.ID)
                //.Select(s => new Students
                //{
                //    ID = s.ID,
                //    FirstName = s.FirstName,
                //    LastName = s.LastName,
                //    Email = s.Email,
                //    Mobile = s.Mobile,
                //    Gender = (Gender)s.Gender,
                //    Age = s.Age,
                //    Address = s.Address,
                //    ZipCode = s.ZipCode,
                //    IsDeleted = s.IsDeleted,
                //    IsActive = s.IsActive,
                //    CreatedDate = s.CreatedDate,
                //    UpdatedDate = s.UpdatedDate == null ? DateTime.Now : s.UpdatedDate
                //})
                .ToListAsync();

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
                //return GetNotFoundResponse<IEnumerable<Students>>();
                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.Message = "Something went wrong ==>>" + ex.Message;
                response.IsValid = true;
                return response;
            }
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Create Student
        /// </summary>
        /// <param name="addStudent">AddStudentDTO addStudent</param>
        /// <returns>Returns the response message</returns>
        public async Task<GenericResponse<Students>> Create(AddStudentDTO addStudent)
        {
            var response = new GenericResponse<Students>();

            try
            {
                if (addStudent == null)
                {
                    return GetErrorResponse<Students>(new ErrorMessage
                    {
                        ErrorType = ErrorType.NullException
                    }, StatusCodes.Status400BadRequest);
                }

                var dataExists = await _context.Students.FirstOrDefaultAsync(x => x.Email.ToLower() == addStudent.Email.ToLower()
                                                && x.Mobile == addStudent.Mobile && x.IsDeleted == false);
                if (dataExists == null)
                {
                    await _context.Students.AddAsync(new Students
                    {
                        FirstName = addStudent.FirstName,
                        LastName = addStudent.LastName,
                        Email = addStudent.Email,
                        Mobile = addStudent.Mobile,
                        Address = addStudent.Address,
                        ZipCode = addStudent.ZipCode,
                        Age = addStudent.Age,
                        Gender = addStudent.Gender,
                        CreatedDate = DateTime.Now,
                        IsActive = true,
                        IsDeleted = false
                    });
                    var status = await _context.SaveChangesAsync();
                    if (status == 1)
                    {
                        response.Count = status;
                        response.Result = await _context.Students.Select(x => x).OrderByDescending(o => o.ID).FirstOrDefaultAsync();
                        response.IsValid = response.Result != null;
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.Result = null;
                        response.Message = "Student data has been added successfully!";
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
                    response.Message = "Student data:- " + dataExists.FirstName + ", " + dataExists.Email + " already exist.";
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
        /// Update Student
        /// </summary>
        /// <param name="updateStudent">UpdateStudentDTO updateStudent</param>
        /// <returns>Returns the response message</returns>
        public async Task<GenericResponse<Students>> Update(UpdateStudentDTO updateStudent)
        {
            var response = new GenericResponse<Students>();

            try
            {
                if (updateStudent == null)
                {
                    return GetErrorResponse<Students>(new ErrorMessage
                    {
                        ErrorType = ErrorType.NullException
                    }, StatusCodes.Status400BadRequest);
                }

                if (await _context.Students.AnyAsync(x => x.ID == updateStudent.ID))
                {
                    var updateData = await _context.Students.Where(x => x.ID == updateStudent.ID).FirstOrDefaultAsync();
                    if (updateData != null)
                    {
                        updateData.FirstName = updateStudent.FirstName;
                        updateData.LastName = updateStudent.LastName;
                        updateData.Email = updateStudent.Email;
                        updateData.Mobile = updateStudent.Mobile;
                        updateData.Address = updateStudent.Address;
                        updateData.ZipCode = updateStudent.ZipCode;
                        updateData.Age = updateStudent.Age;
                        updateData.Gender = updateStudent.Gender;
                        updateData.UpdatedDate = DateTime.Now;
                        updateData.IsActive = updateStudent.IsActive;
                    }

                    var status = await _context.SaveChangesAsync();
                    if (status == 1)
                    {
                        response.Result = await _context.Students.Where(x => x.ID == updateStudent.ID).Select(s => s).FirstOrDefaultAsync();
                        response.Count = status;
                        response.IsValid = response.Result != null;
                        response.StatusCode = StatusCodes.Status200OK;
                        response.Message = "Student data updated successfully!";
                        return response;
                    }
                    else
                    {
                        return GetNotFoundResponse<Students>();
                    }
                }
                else
                {
                    response.Result = null;
                    response.StatusCode = StatusCodes.Status404NotFound;
                    response.Message = "Student data not available!";
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
        /// Delete Student by ID
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

                if (await _context.Students.AnyAsync(x => x.ID == Id))
                {
                    var Students = await _context.Students.Where(x => x.ID == Id).FirstOrDefaultAsync();
                    if (Students != null && Students.IsDeleted)
                    {
                        response.Count = 0;
                        response.Result = false;
                        response.StatusCode = StatusCodes.Status404NotFound;
                        response.Message = "Student data doesn't exist!";
                        return response;
                    }
                    else
                    {
                        Students.IsDeleted = true;
                        Students.UpdatedDate = DateTime.Now;

                        var status = await _context.SaveChangesAsync();
                        if (status > 0)
                        {
                            response.Count = status;
                            response.IsValid = true;
                            response.StatusCode = StatusCodes.Status200OK;
                            response.Result = true;
                            response.Message = "Student data deleted successfully!";
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
                    response.Message = "Student data doesn't exist!";
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
        /// Activate Student by ID
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

                if (await _context.Students.AnyAsync(x => x.ID == Id && x.IsDeleted == false))
                {
                    var activeData = await _context.Students.Where(x => x.ID == Id).FirstOrDefaultAsync();
                    if (activeData.IsActive)
                    {
                        response.Count = 0;
                        response.Result = false;
                        response.IsValid = false;
                        response.StatusCode = StatusCodes.Status409Conflict;
                        response.Message = "Student data Already Active!";
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
                            response.Message = "Student data has been activated!";
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
                    response.Message = "Student data not exists!";
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
        /// Deactivate Student by ID
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

            if (await _context.Students.AnyAsync(x => x.ID == Id && x.IsDeleted == false))
            {
                var objData = await _context.Students.Where(x => x.ID == Id).FirstOrDefaultAsync();
                if (objData != null && !objData.IsActive)
                {
                    response.Count = 0;
                    response.Result = false;
                    response.StatusCode = StatusCodes.Status409Conflict;
                    response.Message = "Student data already deactive!";
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
                        response.Message = "Student data DeActivated!";
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
    }
}
