﻿using Moor.Core.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Repository.Entities
{
    public class PriceEntity : BaseEntity
    {
        [Column("car_parameter_id")]
        public long CarParameterId { get; set; }

        [Column("disctrict_id")]
        public long DistrictId { get; set; }

        [Column("Price")]
        public decimal Price { get; set; }

        [ForeignKey("DistrictId")]
        public virtual DisctrictEntity DisctrictEntity { get; set; }

        [ForeignKey("CarParameterId")]
        public virtual CarParameterEntity CarParameterEntity { get; set; }
    }
}
