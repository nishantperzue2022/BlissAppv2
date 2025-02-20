using BlissApp.DTO.CountyModule;

namespace BlissApp.BLL.CountyModule
{
    public interface ICountyRepository
    {
        Tuple<bool, List<CountyDTO>, string> GetCounties();
    }
}