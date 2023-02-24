using Moor.Model.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Model.Models.MoorModels.TravellerModel
{
    public class TravellerModel : BaseModel
    {
        public long TransferId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public virtual TransferModel.TransferModel Transfer { get; set; }
    }
}
