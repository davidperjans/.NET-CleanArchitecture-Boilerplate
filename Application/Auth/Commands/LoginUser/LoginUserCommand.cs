using Application.Auth.DTOs;
using Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Auth.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<OperationResult<LoginUserResult>>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class LoginUserResult
    {
        public string Token { get; set; } = string.Empty;
        public UserDto User { get; set; } = default!;
    }
}
