using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARENT.Common
{
    public struct ProfileData
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public Permission Permission { get; private set; }
        public ulong Groups { get; private set; }
        public List<string> KnownDevices { get; private set; }

        public bool IsInGroup(int groupId)
        {
            if (groupId < 0 || groupId >= 64) throw new ArgumentOutOfRangeException(nameof(groupId), groupId, "Group ID needs to be between 0 and 63 (inclusive)");
            return (Groups & 1ul << groupId) != 0;
        }

        public bool HasPermission(Permission permission)
        {
            return (Permission & permission) != 0;
        }

        public bool IsAssociated(DeviceData device)
        {
            return KnownDevices.Contains(device.HardwareID);
        }
 

    }
}
