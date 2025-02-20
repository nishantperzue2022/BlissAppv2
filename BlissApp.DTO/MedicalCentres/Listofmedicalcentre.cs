using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlissApp.DTO.MedicalCentres
{
    public class Listofmedicalcentre
    {
        public int Id { get; set; }
        public string? Location { get; set; }
        public string? County { get; set; }
        public string? Region { get; set; }
        public string? GeoLocation { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string? Physical_Address { get; set; }
        public string? Facility_Contact { get; set; }
        public string? Working_Hours { get; set; }
        public string? MO_CO { get; set; }
        public string? Pharmacy { get; set; }
        public string? Laboratory { get; set; }
        public string? Optical { get; set; }
        public string? Dental { get; set; }
        public string? XRAY { get; set; }
        public string? UltraSound { get; set; }
        public string? CTScan { get; set; }
        public string? Physiotherapy { get; set; }
        public double Distance { get; set; }

        public string CentreNameWithDistance
        {
            get
            {
                if (Distance > 0)
                {

                    var newDistance = Math.Round(Distance, 1);

                    var data = Location + " (" + newDistance + " " + "km)";

                    return data;
                }
                else
                {
                    return Location;
                }

            }
        }


    }
}
