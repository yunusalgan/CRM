using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Service.Enums
{
    public enum ResultStatuses
    {
        Success = 1,
        Warning = 2,
        Info = 3,
        Error = 4,
        LOGIN_TEMP_TOKEN_EXPIRED = 1001,
        AUTH_NOT_ENABLED = 3001,
        USER_NOT_AUTHENTICATED = 401
    }
}
