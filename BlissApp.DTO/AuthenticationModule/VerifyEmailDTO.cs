using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlissApp.DTO.AuthenticationModule
{
    public class VerifyEmailDTO
    {
        public Guid Id { get; set; }
        public string OTP { get; set; }
        public string MemberName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string message { get; set; }
        public bool status { get; set; }


    }
}
