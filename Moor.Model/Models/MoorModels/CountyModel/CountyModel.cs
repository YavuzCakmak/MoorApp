using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Model.Models.MoorModels.CountyModel
{
    public class CountyModel
    {
        public long Id { get; set; }
        public long CityId { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public virtual CityModel.CityModel City { get; set; }
    }
}
