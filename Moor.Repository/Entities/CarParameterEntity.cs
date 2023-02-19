using Moor.Core.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moor.Repository.Entities
{
    public class CarParameterEntity : BaseEntity
    {
        [Column("brand")]
        public string Brand { get; set; }

        [Column("model")]
        public string Model { get; set; }
    }
}
