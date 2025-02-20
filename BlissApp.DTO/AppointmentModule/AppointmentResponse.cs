using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlissApp.DTO.AppointmentModule
{
    public class AppointmentResponse
    {
        public AppointmentDetailsDTO[] listOfAppointments { get; set; }
        public int status { get; set; }

        //public class Listofappointment
        //{
        //    public string id { get; set; }
        //    public string memberId { get; set; }
        //    public string department { get; set; }
        //    public string relation { get; set; }
        //    public int status { get; set; }
        //    public DateTime createDate { get; set; }
        //    public DateTime proposedDate { get; set; }
        //    public object updatedDate { get; set; }
        //    public string phoneNumber { get; set; }
        //    public object email { get; set; }
        //    public string patientName { get; set; }
        //    public string priorityTime { get; set; }
        //}

    }
}
