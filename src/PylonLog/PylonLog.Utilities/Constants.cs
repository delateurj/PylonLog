using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PylonLog.Utilities
{
    public static class Constants
    {
        public static readonly byte[] NEW_SESSION_LOG_PATTERN = { 0xff, 0xff, 0xff, 0xff };

        public const int HEADER_BLOCK_LENGTH = 36;

        public static readonly byte[] LAST_SUPPLEMENTAL_HEADER_PATTERN = { 0xff,0xff,0xff,0xff,0x17,0x17 };

        public const int DATA_BLOCK_LENGTH = 20;

        public const int PLANE_NAME_OFFSET = 12;
    }
}
