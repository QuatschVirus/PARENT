using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace PARENT.Common
{
    public struct DeviceData
    {
        public string HardwareID { get; set; }
        public string Nickname { get; set; }

        public DeviceData(string HWID, string nickname) : this()
        {
            this.HardwareID = HWID;
            Nickname = nickname;
        }

        public int Profile = 0;

        public override bool Equals(object obj)
        {
            return obj is DeviceData data &&
                   HardwareID == data.HardwareID;
        }

        public static bool operator ==(DeviceData left, DeviceData right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(DeviceData left, DeviceData right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return HardwareID.GetHashCode();
        }
    }
}
