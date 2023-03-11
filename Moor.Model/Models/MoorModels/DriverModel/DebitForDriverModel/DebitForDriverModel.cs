using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Model.Models.MoorModels.DriverModel.DebitForDriverModel
{
    public class DebitForDriverModel
    {
        public long TransferId { get; set; }
        public decimal Amount { get; set; }
        public long DriverId { get; set; }
    }
}
