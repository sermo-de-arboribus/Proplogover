using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proplogover.Helpers
{
    public static class MathHelpers
    {
        public static int IntPow(this int bas, int exp)
        {
            if(exp < 0)
            {
                throw new ArgumentOutOfRangeException("The IntPow helper method is only valid for positive exponents");
            }
            return Enumerable
                .Repeat(bas, exp)
                .Aggregate(1, (a, b) => a * b);
        }
    }
}
