using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlissApp.BLL.Utils
{
    public class VerifyAccountResponse
    {
        public Message message { get; set; }

        public class Message
        {
            public string id { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string phoneNumber { get; set; }
            public string password { get; set; }
            public int status { get; set; }
            public DateTime createDate { get; set; }
            public string county { get; set; }
        }

    }
}
