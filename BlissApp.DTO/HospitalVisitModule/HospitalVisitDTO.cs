using BlissApp.DTO.MedicalCentres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlissApp.DTO.HospitalVisitModule
{

    public class HospitalVisitDTO
    {
        public Listofmedicalcentre[] listOfMedicalCentres { get; set; }
        public int status { get; set; }
    }




}
