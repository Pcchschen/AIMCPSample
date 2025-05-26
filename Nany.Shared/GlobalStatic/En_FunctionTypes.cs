using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nany.Shared
{
    public enum En_FunctionTypes
    {   
        [Description("登录账号管理")]
        FC_1_UserLoginAccountAdmin,


        [Description("材料")]
        FC_32_MaterialAdmin = 33,


        [Description("")]
        FC_100_None = 100,

        FC_31_CRM,
        NA
    }

}
