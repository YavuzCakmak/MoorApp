using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Model.Dtos.MoorDto.DriverDto
{
    public class DriverDto
    {
        public long Id { get; set; }
        public long? PersonnelId { get; set; }
        public decimal? Price { get; set; }
        public decimal? DriverTotalPrice { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
