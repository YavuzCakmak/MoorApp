using Moor.Model.Models.Base;

namespace Moor.Model.Models.MoorModels.CityModel
{
    public class CityModel : BaseModel
    {
        public long CountryId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public CountryModel.CountryModel Country { get; set; }
    }
}
