using Application.Auth.DTOs;
using Application.Common;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Auth.Commands.RegisterUser
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, OperationResult<RegisterUserResult>>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;

        public RegisterUserHandler(IRepository<User> userRepository, IJwtService jwtService, IMapper mapper)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _mapper = mapper;
        }

        public async Task<OperationResult<RegisterUserResult>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.FindAsync(u => u.Email!.ToLower() == request.Email.ToLower());

            if (existingUser.Any())
                return OperationResult<RegisterUserResult>.Failure("Email is already registered.");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = request.UserName,
                Email = request.Email,
                PasswordHash = hashedPassword,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _userRepository.AddAsync(user);
            
            var token = _jwtService.GenerateJwtToken(user);
            var userDto = _mapper.Map<UserDto>(user);

            var result = new RegisterUserResult
            {
                Token = token,
                User = userDto
            };

            return OperationResult<RegisterUserResult>.Success(result);
        }
    }
}
