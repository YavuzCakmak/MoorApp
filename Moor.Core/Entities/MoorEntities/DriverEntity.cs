﻿using Moor.Core.Attributes;
using Moor.Core.Entities.Base;
using Moor.Core.Entities.MoorEntities.AuthorizeEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Core.Entities.MoorEntities
{
    public class DriverEntity : BaseEntity
    {
        [DataFilter]
        [Column("personnel_id")]
        public long? PersonnelId { get; set; }

        [DataFilter]
        [Column("driver_price")]
        public decimal Price { get; set; }

        [DataFilter]
        [ForeignKey("PersonnelId")]
        public virtual PersonnelEntity Personnel { get; set; }

    }
}
