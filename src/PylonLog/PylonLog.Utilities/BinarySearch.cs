using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PylonLog.Utilities
{
    public static class BinarySearch
    {
        public static int findPattern(byte[] target, byte[] pattern)
        {
            if (pattern.Length == 0 || target.Length == 0)
            {
                return -1;
            }
            var len = pattern.Length;
            var limit = target.Length - len;
            for (var i = 0; i <= limit; i++)
            {
                var k = 0;
                for (; k < len; k++)
                {
                    if (pattern[k] != target[i + k]) break;
                }
                if (k == len) return i;
            }
            return -1;
        }
    }
}
