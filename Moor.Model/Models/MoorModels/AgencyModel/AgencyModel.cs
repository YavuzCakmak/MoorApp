using Moor.Model.Model.Authorize;
using Moor.Model.Models.Base;

namespace Moor.Model.Models.MoorModels.AgencyModel
{
    public class AgencyModel 
    {
        public string? AgencyName { get; set; }
        public string? AgencyEmail { get; set; }
        public string? Title { get; set; }
        public string? TaxOffice { get; set; }
        public string? TaxNumber { get; set; }
        public string? AgencyPhoneNumber { get; set; }
        public string? OperationPhoneNumber { get; set; }
        public string? AgencyMediaPath { get; set; }
        public string? AgencyDetails { get; set; }
        public decimal? ReceptionPrice { get; set; }
        public long? CityId { get; set; }
        public long? CountyId { get; set; }
        public PersonnelModel PersonnelModel { get; set; } = new PersonnelModel();
    }
}
