using Rate_Limiter.DbStore;
using Rate_Limiter.Interfaces;
using Rate_Limiter.Service;

namespace RateLimiterTest
{
    public class TokenBucketRateLimiterTests
    {
        [Fact]
        public void Should_Block_When_Limit_Exceeded()
        {
            // Arrange
            var limiter = CreateLimiter(2);

            // Act & Assert
            Assert.True(limiter.IsAllowed("user1"));
            Assert.True(limiter.IsAllowed("user1"));
            Assert.False(limiter.IsAllowed("user1"));
        }

        private static IRateLimiter CreateLimiter(int limit)
        {
            return new TokenBucketRateLimiter(
                new MemoryRateLimitStore(),
                new OptionsMonitorStub(limit));
        }
    }
}
