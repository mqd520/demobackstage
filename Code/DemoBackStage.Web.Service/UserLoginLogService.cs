using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DemoBackStage.Entity;
using DemoBackStage.Web.IService;

namespace DemoBackStage.Web.Service
{
    public class UserLoginLogService : Service<UserLoginLogEntity>, IUserLoginLogService
    {

    }
}
