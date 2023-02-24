namespace Moor.Model.Models.MoorModels.CityModel
{
    public class CityModel
    {
        public long Id { get; set; }
        public long CountryId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public CountryModel.CountryModel Country { get; set; }
    }
}
