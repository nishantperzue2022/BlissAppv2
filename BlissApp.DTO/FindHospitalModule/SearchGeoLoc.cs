using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlissApp.DTO.FindHospitalModule
{


    public class SearchGeoLoc
    {
        public string hospitalName { get; set; }
        public string countyName { get; set; }
        public int searchType { get; set; }
    }


}
