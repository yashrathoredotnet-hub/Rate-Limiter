using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rate_Limiter.Model
{
    public sealed class TokenBucket
    {
        public double Tokens { get; set; }
        public DateTime LastRefill { get; set; }
    }

}
