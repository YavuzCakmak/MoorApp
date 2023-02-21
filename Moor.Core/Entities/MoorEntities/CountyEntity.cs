using System.ComponentModel.DataAnnotations.Schema;

namespace Moor.Core.Entities.MoorEntities
{
    [Table("county")]
    public class CountyEntity
    {
        [Column("id")]
        public long Id { get; set; }
        [Column("city_id")]
        public long CityId { get; set; }
        [Column("code")]
        public int Code { get; set; }
        [Column("name")]
        public string Name { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; }

        [ForeignKey("CityId")]
        public virtual CityEntity City { get; set; }
    }
}
