using BlissApp.DTO.MemberModule;

namespace BlissApp.DTO.AuthenticationModule
{

    public class UserResponse
    {
        public MemberDTO message { get; set; }
        public bool status { get; set; }
    }

    public class Message
    {
        public int auto_id { get; set; }
        public int control_no { get; set; }
        public int sno { get; set; }
        public string emp_code { get; set; }
        public string name { get; set; }
        public string sex { get; set; }
        public string relation { get; set; }
        public string newRelation { get; set; }
        public int sum_insured { get; set; }
        public int comm { get; set; }
        public int dhl { get; set; }
        public int prem { get; set; }
        public int card_stat { get; set; }
        public int dispatch { get; set; }
        public DateTime datejoin { get; set; }
        public string barCode { get; set; }
        public string society { get; set; }
        public string region { get; set; }
        public string state { get; set; }
        public string district { get; set; }
        public string city { get; set; }
        public string nameRL { get; set; }
        public string address { get; set; }
        public string addressRL { get; set; }
        public string imagePath { get; set; }
        public DateTime entryDate { get; set; }
        public string _class { get; set; }
        public int preCardNo { get; set; }
        public string preCardNoAll { get; set; }
        public DateTime iDate { get; set; }
        public int deleted { get; set; }
        public int zoneCode { get; set; }
        public int dcsCode { get; set; }
        public int dcsSrNo { get; set; }
        public string bankIFSC { get; set; }
        public string bankCity { get; set; }
        public string bankName { get; set; }
        public string accountNo { get; set; }
        public string accountType { get; set; }
        public string bankBranch { get; set; }
        public string mobile { get; set; }
        public DateTime resignDate { get; set; }
        public DateTime dob { get; set; }
        public float height { get; set; }
        public float weight { get; set; }
        public string endoType { get; set; }
        public DateTime endoDate { get; set; }
        public string nomineeName { get; set; }
        public string nomineeRelation { get; set; }
        public string preExistD { get; set; }
        public int idNo { get; set; }
        public int familyID { get; set; }
        public string maritalStatus { get; set; }
        public int endoNo { get; set; }
        public string remarks { get; set; }
        public string bloodGroup { get; set; }
        public int occupationCode { get; set; }
        public string email { get; set; }
        public string location { get; set; }
        public string verificationCode { get; set; }
        public string password { get; set; }
        public string memberId { get; set; }
        public string vipClient { get; set; }
        public int balSumInsured { get; set; }
        public int entityId { get; set; }
        public int banNo { get; set; }
        public string brCode { get; set; }
        public DateTime inceptionDate { get; set; }
        public string superTopUp { get; set; }
        public string spouseName { get; set; }
        public string status { get; set; }
        public string pin { get; set; }
        public string integrationKey { get; set; }
        public string nationalIDPassportNo { get; set; }
        public string hudumaNumber { get; set; }
        public string birthCertificateNumber { get; set; }
        public string spouseNationalIDPassportNo { get; set; }
        public string jobGroup { get; set; }
        public string fingerprint { get; set; }
        public string fingerprintEnrollment { get; set; }
        public bool isActive { get; set; }
        public object activateBy { get; set; }
        public object createdBy { get; set; }
        public string policy_type { get; set; }
        public string minetMemberId { get; set; }
        public object dateOfActivation { get; set; }
        public int age { get; set; }
    }


}
