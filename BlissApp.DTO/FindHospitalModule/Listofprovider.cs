namespace BlissApp.DTO.FindHospitalModule
{
    public class Listofprovider
    {
        public string id { get; set; }
        public string providerPhoneNumber { get; set; }
        public string providerEmail { get; set; }
        public string providerName { get; set; }
        public string NewproviderName
        {
            get
            {
                if (string.IsNullOrEmpty(providerName))
                {
                    return "NA";
                }

                else
                {
                    return providerName;
                }
            }
        }
        public string pinCode { get; set; }
        public string email { get; set; }
        public string corporateName { get; set; }
        public string level { get; set; }
        public int hospitalId { get; set; }
        public bool isCorporate { get; set; }
        public bool isO_Accreditation { get; set; }
        public bool nablE_Accreditation { get; set; }
        public bool nhiF_Accreditation { get; set; }
        public bool other_Accreditation { get; set; }
        public string otherAccreditationDesc { get; set; }
        public bool nabH_Accreditation { get; set; }
        public string nabhCertificateNo { get; set; }
        public string ownershipTypeId { get; set; }
        public string providerSetupId { get; set; }
        public DateTime createDate { get; set; }
        public object updatedDate { get; set; }
        public string createdBy { get; set; }
        public string updatedBy { get; set; }
        public int status { get; set; }
        public DateTime? approvalDate { get; set; }
        public string approvalRemarks { get; set; }
        public string approvedBy { get; set; }
        public string rejectedBy { get; set; }
        public DateTime? rejectionDate { get; set; }
        public string rejectionRemarks { get; set; }
        public string county { get; set; }
        public string sub_county { get; set; }
        public string region { get; set; }
        public string town { get; set; }
        public string registeredContact { get; set; }
        public string registeredEmailId { get; set; }
        public string address { get; set; }
        public string website { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public bool? crisil { get; set; }
        public bool? icra { get; set; }
        public bool? jci { get; set; }
        public bool? safecare { get; set; }
        public bool? cohsasa { get; set; }
    }
}
