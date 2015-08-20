

using System;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace RepairReach.Data.Infrastructure.Conventions
{
    class MaxStringLengthConvention
        :Convention
    {
        public MaxStringLengthConvention()
        {
            this.Properties<String>()
                .Configure(c => c.HasMaxLength(255));
        }
    }
}
