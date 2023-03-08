using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Model.Dtos.MoorDto.CarDto
{
    public class CarDto
    {
        public long? Id { get; set; }
        public long? CarParameterId { get; set; }
        public string? CarBrand { get; set; }
        public string? CarModel { get; set; }
        public string? NumberPlate { get; set; }
    }
}
