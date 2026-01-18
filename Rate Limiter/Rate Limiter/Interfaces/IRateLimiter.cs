using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rate_Limiter.Interfaces
{
    public interface IRateLimiter
    {
        bool IsAllowed(string identifier);
    }
}
