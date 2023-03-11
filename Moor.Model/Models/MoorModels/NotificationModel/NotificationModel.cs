using Moor.Model.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Model.Models.MoorModels.NotificationModel
{
    public class NotificationModel : BaseModel
    {
        public long? TransferId { get; set; }
        public long? AgencyId { get; set; }
        public long? IsRead { get; set; }
        public string? Explanation { get; set; }
    }
}
