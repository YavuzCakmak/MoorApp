using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Model.Models.MoorModels.UserModel
{
    public class UserModel
    {
        public long Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public int Department { get; set; }

        public bool IsDeleted { get; set; }
    }
}
