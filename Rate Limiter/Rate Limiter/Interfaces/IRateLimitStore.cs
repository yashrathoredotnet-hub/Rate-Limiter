using Rate_Limiter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rate_Limiter.Interfaces
{
    public interface IRateLimitStore
    {
        TokenBucket GetOrCreate(string key, Func<TokenBucket> factory);
    }
}
