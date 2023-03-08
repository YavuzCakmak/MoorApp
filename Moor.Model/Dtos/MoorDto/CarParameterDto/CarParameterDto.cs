using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Model.Dtos.MoorDto.CarParameterDto
{
    public class CarParameterDto
    {
        public long Id { get; set; }
        public long CarModelId { get; set; }
        public long CarBrandId { get; set; }
        public string? CarBrandName { get; set; }
        public string? CarModelName { get; set; }
    }
}
