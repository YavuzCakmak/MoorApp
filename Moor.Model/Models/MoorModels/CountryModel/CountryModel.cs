
namespace Moor.Model.Models.MoorModels.CountryModel
{
    public class CountryModel
    {
        public long Id { get; set; }
        public string A2Code { get; set; }
        public string A3Code { get; set; }
        public string NumCode { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}
