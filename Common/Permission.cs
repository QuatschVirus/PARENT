using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARENT.Common
{
    [Flags]
    public enum Permission
    {
        None = 0,
        Recieve = 1 << 0,
        Ping = 1 << 1,
        PingAll = 1 << 2,
        RequestStatus = 1 << 3,
        RequestActivity = 1 << 4,
        SendAudio = 1 << 5
    }
}
