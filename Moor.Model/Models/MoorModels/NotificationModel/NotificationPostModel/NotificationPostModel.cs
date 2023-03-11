using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Model.Models.MoorModels.NotificationModel.NotificationPostModel
{
    public class NotificationPostModel
    {
        public long? AgencyId { get; set; }
        public string AgencyName { get; set; }
        public long? TransferId { get; set; }
        public string? Explanation { get; set; }
        public int IsRead { get; set; }
        public bool IsFirst { get; set; } = false;
        public bool IsDriver { get; set; } = false;
        public bool IsDriverChange { get; set; } = false;
        public bool IsAmount { get; set; } = false;
        public bool IsStatus { get; set; } = false;
    }
}
