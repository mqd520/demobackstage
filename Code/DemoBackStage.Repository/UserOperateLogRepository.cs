using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DemoBackStage.Entity;
using DemoBackStage.IRepository;

namespace DemoBackStage.Repository
{
    public class UserOperateLogRepository : Repository<UserOperateLogEntity>, IUserOperateLogRepository
    {
        /// <summary>
        /// Get or Set IsAdministrator
        /// </summary>
        public virtual bool IsAdministrator { get; set; }
    }
}
