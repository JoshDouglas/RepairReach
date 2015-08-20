using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepairReach.Core.Model;

namespace RepairReach.Core.Service
{
    public interface IGeocodingService
    {
        Task<Location> GetLocation(string address);
    }
}
