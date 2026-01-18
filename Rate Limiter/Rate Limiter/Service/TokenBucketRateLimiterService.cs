using Microsoft.Extensions.Options;
using Rate_Limiter.Interfaces;
using Rate_Limiter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rate_Limiter.Service
{
    public sealed class TokenBucketRateLimiter : IRateLimiter
    {
        private readonly IRateLimitStore _store;
        private readonly IOptionsMonitor<RateLimitOptions> _options;

        public TokenBucketRateLimiter(
            IRateLimitStore store,
            IOptionsMonitor<RateLimitOptions> options)
        {
            _store = store;
            _options = options;
        }

        public bool IsAllowed(string identifier)
        {
            var limit = _options.CurrentValue.RequestsPerMinute;
            var refillRatePerSecond = limit / 60.0;

            var bucket = _store.GetOrCreate(identifier, () => new TokenBucket
            {
                Tokens = limit,
                LastRefill = DateTime.UtcNow
            });

            lock (bucket)
            {
                var now = DateTime.UtcNow;
                var elapsedSeconds = (now - bucket.LastRefill).TotalSeconds;

                bucket.Tokens = Math.Min(
                    limit,
                    bucket.Tokens + elapsedSeconds * refillRatePerSecond);

                bucket.LastRefill = now;

                if (bucket.Tokens < 1)
                    return false;

                bucket.Tokens--;
                return true;
            }
        }
    }
}
