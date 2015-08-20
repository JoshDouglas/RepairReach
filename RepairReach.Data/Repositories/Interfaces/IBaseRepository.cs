using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairReach.Data.Repositories.Interfaces
{
    public interface IBaseRepository
    {
        RepairReachContext GetContext();
    }
}
