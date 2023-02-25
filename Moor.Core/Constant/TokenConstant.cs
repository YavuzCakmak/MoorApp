using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Core.Constant
{
    public class TokenConstant
    {
        public const string EXPIRED_TOKEN = "Token zaman aşımına uğramıştır.";

        public const string NOT_AUTH = "Yetkiniz bulunmamaktadır.";

        public const string INVALID_TOKEN = "Token geçerli değildir.";

        public const string NOT_FOUND_TOKEN = "Token bulunamadı.";

        public const string NOT_AUTHORIZATION = "[[%AUTHORIZATIONS%]] yetkilerinden en az bir tanesine sahip olmalısınız.";
    }
}
