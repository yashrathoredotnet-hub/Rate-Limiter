using Microsoft.Extensions.Options;
using Rate_Limiter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimiterTest
{
    public sealed class OptionsMonitorStub : IOptionsMonitor<RateLimitOptions>
    {
        private RateLimitOptions _currentValue;

        public OptionsMonitorStub(int requestsPerMinute)
        {
            _currentValue = new RateLimitOptions
            {
                RequestsPerMinute = requestsPerMinute
            };
        }

        public RateLimitOptions CurrentValue => _currentValue;

        public RateLimitOptions Get(string? name) => _currentValue;

        public IDisposable OnChange(Action<RateLimitOptions, string?> listener)
        {
            // No-op for tests
            return new DummyDisposable();
        }

        private sealed class DummyDisposable : IDisposable
        {
            public void Dispose() { }
        }
    }
}
