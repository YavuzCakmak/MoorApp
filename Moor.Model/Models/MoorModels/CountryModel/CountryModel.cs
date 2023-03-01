
using Moor.Model.Models.Base;

namespace Moor.Model.Models.MoorModels.CountryModel
{
    public class CountryModel : BaseModel
    {
        public string A2Code { get; set; }
        public string A3Code { get; set; }
        public string NumCode { get; set; }
        public string Name { get; set; }
    }
}
