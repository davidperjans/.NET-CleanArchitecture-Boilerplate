using Application.Auth.DTOs;
using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Auth.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<OperationResult<RegisterUserResult>>
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class RegisterUserResult
    {
        public string? Token { get; set; }
        public UserDto? User { get; set; }
    }
}
