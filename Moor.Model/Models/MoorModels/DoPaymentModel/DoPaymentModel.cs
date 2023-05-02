using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Model.Models.MoorModels.DoPaymentModel
{
    public class DoPaymentModel
    {
        public long? AgencyId { get; set; }
        public long? DriverId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Description { get; set; }
    }
}
