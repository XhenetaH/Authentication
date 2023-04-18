using Authentication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Domain.DTOs.User
{
    public class UserDto : AuditColumns
    {
        public int Id { get; set; }

        public string Email { get; set; } = string.Empty;

        public byte[] PasswordHash { get; set; } = new byte[512];

        public byte[] PasswordSalt { get; set; } = new byte[512];

    }
}
