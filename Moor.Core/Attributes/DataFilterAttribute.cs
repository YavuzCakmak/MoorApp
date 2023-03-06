using Sieve.Attributes;

namespace Moor.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class DataFilterAttribute : SieveAttribute
    {
        public DataFilterAttribute()
        {
            this.CanFilter = this.CanSort = true;
        }
    }
}
