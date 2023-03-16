using Moor.Model.Model.Authorize;
using Moor.Model.Models.Base;

namespace Moor.Model.Models.MoorModels.AgencyModel
{
    public class AgencyModel : BaseModel
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string TaxOffice { get; set; }
        public string TaxNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string OperationPhoneNumber { get; set; }
        public string MediaPath { get; set; }
        public string AgencyEmail { get; set; }
        public string Details { get; set; }
        public decimal ReceptionPrice { get; set; }
        public long PersonnelId { get; set; }
        public long? CityId { get; set; }
        public PersonnelModel PersonnelModel { get; set; } = new PersonnelModel();
        public long? CountyId { get; set; }
    }
}
