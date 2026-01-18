using Rate_Limiter.Interfaces;
using Rate_Limiter.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rate_Limiter.DbStore
{
    public sealed class MemoryRateLimitStore : IRateLimitStore
    {
        private readonly ConcurrentDictionary<string, TokenBucket> _store = new();

        public TokenBucket GetOrCreate(string key, Func<TokenBucket> factory)
            => _store.GetOrAdd(key, _ => factory());
    }
}
