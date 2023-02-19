using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Repository.Entities
{
    public class UserEntity
    {
        [Column("id")]
        public long Id { get; set; }
        
        [Column("username")]
        public string Username { get; set; }
        
        [Column("password")]
        public string Password { get; set; }
        
        [Column("department")]
        public int Department { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; }
    }
}
