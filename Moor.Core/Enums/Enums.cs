using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Core.Enums
{
    public enum Status
    {
        AKTIF = 0,
        PASIF = 1
    }
    public enum Roles
    {
        MOOR_GIRIS = 86,
        ACENTE = 87,
        SOFOR = 88,
        ADMIN = 89
    }

    public enum DirectionType
    {
        TEK_YON = 1,
        GİDİS_DONUS = 2
    }

    public enum TransferStatus
    {
        BEKLEMEDE = 0,
        IPTAL = 1,
        NON_SHOW = 2,
        TAMAMLANDI = 3,
    }
}
