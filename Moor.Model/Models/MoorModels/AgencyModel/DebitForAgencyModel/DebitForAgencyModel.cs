using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Model.Models.MoorModels.AgencyModel.DebitForAgencyModel
{
    public class DebitForAgencyModel
    {
        public long TransferId { get; set; }
        public decimal Amount { get; set; }
        public long AgencyId { get; set; }
    }
}
