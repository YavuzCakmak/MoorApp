using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Core.Services.MoorService
{
    public interface IMailService
    {
        public Task SendTransferMail(long transferId);
    }
}
