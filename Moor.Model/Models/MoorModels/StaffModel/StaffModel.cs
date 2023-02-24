using Moor.Model.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Model.Models.MoorModels.StaffModel
{
    public class StaffModel : BaseModel
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? MediaPath { get; set; }
        public long UserId { get; set; }
        public virtual UserModel.UserModel User { get; set; }
    }
}
