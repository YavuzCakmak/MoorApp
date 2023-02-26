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
        public string Brand { get; set; }
        public string Model { get; set; }
        public string MediaPath { get; set; }
    }
}
