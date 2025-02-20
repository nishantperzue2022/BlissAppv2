namespace BlissApp.ViewModels
{
    public class HealthTipsViewModel
    {
        public List<HealthTips> GetBannerList()
        {
            List<HealthTips> list = new List<HealthTips>
            {
                new HealthTips { URL = "header1.jpeg" },

                new HealthTips { URL = "header2.jpeg" },

                new HealthTips { URL = "header3.jpeg" },

                new HealthTips { URL = "header4.jpeg" },

                new HealthTips { URL = "header5.jpeg" },
            };

            return list;
        }
    }
    public class HealthTips
    {
        public string URL { get; set; }
    }
}
