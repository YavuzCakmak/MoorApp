using Microsoft.AspNetCore.Http;
using Moor.Core.Constant;
using Moor.Model.Authorize;
using Moor.Service.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Service.Utilities.Session
{
    public class SessionManager
    {
        private readonly ISession _session;

        public SessionManager(IHttpContextAccessor httpContextAccessor)
        {
            _session = httpContextAccessor.HttpContext.Session;
        }
        public SessionManager(HttpContext context)
        {
            _session = context.Session;
        }

        public virtual UserSessionModel User
        {
            get
            {
                var _userInfo = _session.GetObjectFromJson<UserSessionModel>(SessionConstant.USER_INFO);
                if (_userInfo != null)
                    return _userInfo;
                else
                    return null;
            }
            set
            {
                _session.SetObjectAsJson(SessionConstant.USER_INFO, value);
            }
        }
    }
}
