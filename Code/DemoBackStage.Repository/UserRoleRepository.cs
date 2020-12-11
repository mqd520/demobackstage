using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DemoBackStage.Entity;
using DemoBackStage.IRepository;

namespace DemoBackStage.Repository
{
    public class UserRoleRepository : Repository<UserRoleEntity>, IUserRoleRepository
    {

    }
}
