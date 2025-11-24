using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Auth.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
    }
}
