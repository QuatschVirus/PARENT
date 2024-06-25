using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PARENT.Common
{
    /// <summary>
    /// Used to define a packet
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class PacketAttribute(uint id) : Attribute
    {
        public uint id = id;
    }

    public class Packet
    {
        public uint Id { get; set; }

        public static readonly Dictionary<uint, Type> typeResolve = [];
        public static readonly Dictionary<Type, uint> idResolve = [];

        public static Packet Deserialize(string data)
        {
            uint id = JsonSerializer.Deserialize<Packet>(data).Id;
            return (Packet)JsonSerializer.Deserialize(data, typeResolve[id]);
        }

        public static string Serialize(Packet packet)
        {
            packet.Id = idResolve[packet.GetType()];
            return JsonSerializer.Serialize(packet, packet.GetType());
        }

        static Packet()
        {
            var packets =
                from a in AppDomain.CurrentDomain.GetAssemblies()
                from t in a.GetTypes()
                where t.IsSubclassOf(typeof(Packet))
                let attribute = Attribute.GetCustomAttribute(t, typeof(PacketAttribute)) as PacketAttribute
                where attribute != null
                select (t, attribute.id);

            foreach (var (t, id) in packets)
            {
                typeResolve.Add(id, t);
                idResolve.Add(t, id);
            }
        }
    }

    public enum SystemPacketKey
    {
        OK,
        NotOK
    }

    [Packet(0)]
    public class SystemPacket(SystemPacketKey key, string custom = "", ulong refernceId = 0) : Packet
    {
        public SystemPacketKey Key { get; set; } = key;
        public string Custom { get; set; } = custom;
        public ulong ReferenceId { get; set; } = refernceId;
    }

    [Packet(1)]
    public class DeviceInfoPacket : Packet
    {
        public DeviceData DeviceData { get; set; } 

        public DeviceInfoPacket(DeviceData deviceData)
        {
            this.DeviceData = deviceData;
            this.Id = 1;
        }
    }
}
