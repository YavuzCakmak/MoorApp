using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Model.Models.MoorModels.NotificationModel.NotificationReadModel
{
    public class NotificationReadModel
    {
        public List<long> NotificationsIds { get; set; }
        public bool IsFirst { get; set; } = false;
        public long AgencyId { get; set; }
    }
}
