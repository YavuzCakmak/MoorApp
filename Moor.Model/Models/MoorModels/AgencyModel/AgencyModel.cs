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
        public string Email { get; set; }
        public string Details { get; set; }
        public decimal ReceptionPrice { get; set; }
        public long StaffId { get; set; }
        public long CityId { get; set; }
        public long CountyId { get; set; }

        public virtual StaffModel.StaffModel Staff { get; set; }
        public virtual CityModel.CityModel City { get; set; }
        public virtual CountyModel.CountyModel County { get; set; }
    }
}
