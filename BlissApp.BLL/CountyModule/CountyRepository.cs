using BlissApp.DTO.CountyModule;

namespace BlissApp.BLL.CountyModule
{
    public class CountyRepository : ICountyRepository
    {
        public Tuple<bool, List<CountyDTO>, string> GetCounties()
        {
            try
            {
                var counties = new List<CountyDTO>
            {
            new CountyDTO { Name = "Baringo"},
            new CountyDTO { Name = "Bomet"},
            new CountyDTO { Name = "Bungoma"},
            new CountyDTO { Name = "Busia"},
            new CountyDTO { Name = "Elgeyo/Marakwet"},
            new CountyDTO { Name = "Embu"},
            new CountyDTO { Name = "Garissa"},
            new CountyDTO { Name = "Homa Bay"},
            new CountyDTO { Name = "Isiolo"},
            new CountyDTO { Name = "Kajiado"},
            new CountyDTO { Name = "Kakamega"},
            new CountyDTO { Name = "Kericho"},
            new CountyDTO { Name = "Kiambu"},
            new CountyDTO { Name = "Kilifi"},
            new CountyDTO { Name = "Kirinyaga"},
            new CountyDTO { Name = "Kisii"},
            new CountyDTO { Name = "Kisumu"},
            new CountyDTO { Name = "Kitui"},
            new CountyDTO { Name = "Kwale"},
            new CountyDTO { Name = "Laikipia"},
            new CountyDTO { Name = "Lamu"},
            new CountyDTO { Name = "Machakos"},
            new CountyDTO { Name = "Makueni"},
            new CountyDTO { Name = "Mandera"},
            new CountyDTO { Name = "Marsabit"},
            new CountyDTO { Name = "Meru"},
            new CountyDTO { Name = "Migori"},
            new CountyDTO { Name = "Murang'a"},
            new CountyDTO { Name = "Nairobi"},
            new CountyDTO { Name = "Nakuru"},
            new CountyDTO { Name = "Nandi"},
            new CountyDTO { Name = "Narok"},
            new CountyDTO { Name = "Nyamira"},
            new CountyDTO { Name = "Nyandarua"},
            new CountyDTO { Name = "Nyeri"},
            new CountyDTO { Name = "Samburu"},
            new CountyDTO { Name = "Siaya"},
            new CountyDTO { Name = "Taita/Taveta"},
            new CountyDTO { Name = "Tana River"},
            new CountyDTO { Name = "Tharaka-Nithi"},
            new CountyDTO { Name = "Trans Nzoia"},
            new CountyDTO { Name = "Turkana"},
            new CountyDTO { Name = "Uasin Gishu"},
            new CountyDTO { Name = "Vihiga"},
            new CountyDTO { Name = "Wajir"},
            new CountyDTO { Name = "West Pokot"},
            new CountyDTO { Name = "Mombasa" } };

                var data = counties.OrderBy(x => x.Name).ToList();

                return new Tuple<bool, List<CountyDTO>, string>(true, data, "Member details");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return new Tuple<bool, List<CountyDTO>, string>(true, null, "Member details");

            }
        }




    }
}
