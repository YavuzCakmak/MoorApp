using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Model.Dtos.MoorDto
{
    public class DriverCarDto
    {
        public long Id { get; set; }
        public long CarId { get; set; }
        public decimal Price { get; set; }
        public string CarModel { get; set; }
        public string CarBrand { get; set; }
    }
}
