using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Model.Models.MoorModels.TransferModel.TransferChangeModel
{
    public class TransferChangeModel
    {
        public long TransferId { get; set; }
        public long? DriverId { get; set; }
        public decimal? Amount { get; set; }
        public int? Status { get; set; }
    }
}
