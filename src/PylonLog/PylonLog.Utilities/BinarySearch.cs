using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PylonLog.Utilities
{
    public static class BinarySearch
    {
        public static int findPattern(byte[] arrayToSearch, byte[] sequenceToFind, int startingIndex=0)
        {
            if (sequenceToFind.Length == 0 || arrayToSearch.Length == 0 || startingIndex > arrayToSearch.Length || startingIndex < 0)
            {
                 return -1;
            }
            var len = sequenceToFind.Length;
            var limit = arrayToSearch.Length - len;
            for (var i = startingIndex; i <= limit; i++)
            {
                var k = 0;
                for (; k < len; k++)
                {
                    if (sequenceToFind[k] != arrayToSearch[i + k]) break;
                }
                if (k == len) return i;
            }
            return -1;
        }
    }
}
