using Moor.Core.Attributes;
using Moor.Core.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moor.Core.Entities.MoorEntities
{
    [Table("county")]
    public class CountyEntity : BaseEntity
    {
        [DataFilter]
        [Column("city_id")]
        public long CityId { get; set; }

        [DataFilter]
        [Column("code")]
        public int Code { get; set; }

        [DataFilter]
        [Column("name")]
        public string Name { get; set; }

        [DataFilter]
        [ForeignKey("CityId")]
        public virtual CityEntity City { get; set; }
    }
}
