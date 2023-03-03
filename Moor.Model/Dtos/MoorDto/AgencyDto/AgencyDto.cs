using Moor.Model.Model.Authorize;
using Moor.Model.Models.MoorModels.CityModel;
using Moor.Model.Models.MoorModels.CountyModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Model.Dtos.MoorDto.AgencyDto
{
    public class AgencyDto
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
        public long? PersonnelId { get; set; } // PersonnelId olacak 
        public long? CityId { get; set; }
        public long? CountyId { get; set; }
        public virtual PersonnelModel Personnel { get; set; }
        public virtual CityModel City { get; set; }
        public virtual CountyModel County { get; set; }
    }
}
