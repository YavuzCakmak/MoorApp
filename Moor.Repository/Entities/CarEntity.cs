using Moor.Core.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moor.Repository.Entities
{
    public class CarEntity : BaseEntity
    {
        [Column("car_parameter_id")]
        public long CarParameterId { get; set; }

        [Column("number_plate")]
        public string NumberPlate { get; set; }

        [ForeignKey("CarParameterId")]
        public virtual CarParameterEntity CarParameterEntity { get; set; }
    }
}
