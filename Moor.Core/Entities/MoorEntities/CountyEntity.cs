using Moor.Core.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moor.Core.Entities.MoorEntities
{
    [Table("county")]
    public class CountyEntity : BaseEntity
    {
        [Column("city_id")]
        public long CityId { get; set; }
        [Column("code")]
        public int Code { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [ForeignKey("CityId")]
        public virtual CityEntity City { get; set; }
    }
}
