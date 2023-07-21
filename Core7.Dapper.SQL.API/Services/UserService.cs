﻿using AutoMapper;
using Core7.Dapper.SQL.API.Entities;
using Core7.Dapper.SQL.API.Helpers;
using Core7.Dapper.SQL.API.Models.Users;
using Core7.Dapper.SQL.API.Repositories;

namespace Core7.Dapper.SQL.API.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _userRepository.GetAll();
        }

        public async Task<User> GetById(int id)
        {
            var user = await _userRepository.GetById(id);

            if (user == null)
                throw new KeyNotFoundException("User not found");

            return user;
        }

        public async Task Create(CreateRequest model)
        {
            // validate
            if (await _userRepository.GetByEmail(model.Email!) != null)
                throw new AppException("User with the email '" + model.Email + "' already exists");

            // map model to new user object
            var user = _mapper.Map<User>(model);

            // hash password
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            // save user
            await _userRepository.Create(user);
        }

        public async Task Update(int id, UpdateRequest model)
        {
            var user = await _userRepository.GetById(id);

            if (user == null)
                throw new KeyNotFoundException("User not found");

            // validate
            var emailChanged = !string.IsNullOrEmpty(model.Email) && user.Email != model.Email;
            if (emailChanged && await _userRepository.GetByEmail(model.Email!) != null)
                throw new AppException("User with the email '" + model.Email + "' already exists");

            // hash password if it was entered
            if (!string.IsNullOrEmpty(model.Password))
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            // copy model props to user
            _mapper.Map(model, user);

            // save user
            await _userRepository.Update(user);
        }

        public async Task Delete(int id)
        {
            await _userRepository.Delete(id);
        }
    }
}
