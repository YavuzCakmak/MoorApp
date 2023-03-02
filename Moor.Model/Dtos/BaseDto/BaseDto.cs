using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Model.Dtos.BaseDto
{
    public class BaseDto
    {
        public string ErrorMessage { get; set; }
        public bool IsSucces { get; set; } = false;
    }
}
