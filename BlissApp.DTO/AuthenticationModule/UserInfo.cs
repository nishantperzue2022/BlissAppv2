using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlissApp.DTO.AuthenticationModule
{
    public class UserInfo
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string MemberName { get; set; }
        public string Password { get; set; }
        public byte? Status { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
