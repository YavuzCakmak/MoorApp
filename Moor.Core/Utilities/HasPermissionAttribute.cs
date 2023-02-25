using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Core.Utilities
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class HasPermissionAttribute : Attribute
    {
        public HasPermissionAttribute()
        {

        }
    }
}
