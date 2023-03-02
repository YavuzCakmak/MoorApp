﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Core.Entities.Base
{
    public abstract class BaseEntity
    {
        [Column("id")]
        [Key]
        public long Id { get; set; }

        [Column("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Column("update_date")]
        public DateTime? UpdateDate { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; }

        [Column("status")]
        public int Status { get; set; }
    }
}
