using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Model.Models.Base
{
    public class BaseModel
    {
        public long Id { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;  

        public DateTime? UpdateDate { get; set; }

        public bool IsDeleted { get; set; } = false;

        public int Status { get; set; } = 0;
    }
}
