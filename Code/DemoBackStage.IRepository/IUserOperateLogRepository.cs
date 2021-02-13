using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DemoBackStage.Entity;

namespace DemoBackStage.IRepository
{
    public interface IUserOperateLogRepository : IRepository<UserOperateLogEntity>
    {
        /// <summary>
        /// Get or Set IsAdministrator
        /// </summary>
        bool IsAdministrator { get; set; }
    }
}
