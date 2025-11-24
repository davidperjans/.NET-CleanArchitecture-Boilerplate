using Application.Auth.DTOs;
using Application.Common;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Auth.Commands.LoginUser
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, OperationResult<LoginUserResult>>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;
        public LoginUserHandler(IRepository<User> userRepository, IJwtService jwtService, IMapper mapper)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _mapper = mapper;
        }
        public async Task<OperationResult<LoginUserResult>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindAsync(u => u.Email!.ToLower() == request.Email.ToLower());

            var foundUser = user.FirstOrDefault();

            if (foundUser == null || !BCrypt.Net.BCrypt.Verify(request.Password, foundUser.PasswordHash))
                return OperationResult<LoginUserResult>.Failure("Invalid email or password.");

            var token = _jwtService.GenerateJwtToken(foundUser);
            var userDto = _mapper.Map<UserDto>(foundUser);
        
            return OperationResult<LoginUserResult>.Success(new LoginUserResult
            {
                Token = token,
                User = userDto
            });
        }
    }
}
