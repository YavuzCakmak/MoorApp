using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Core.SSH.Model
{
    public class MediaUploadResult
    {
        public bool IsSuccess { get; set; }
        public string Link { get; set; }
        public string MainRoot { get; set; }
        public List<string> ErrorMessageList { get; set; } = new List<string>();
    }
}
