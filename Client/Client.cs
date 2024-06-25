using Client;
using PARENT.Common;
using PARENT.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;

namespace PARENT.Client
{
    public class Client
    {
        public readonly Common.Logger logger;

        readonly ClientConfiguration cfg;
        readonly DeviceData deviceData;
        readonly WebSocket socket;

        public Client(string cfgPath)
        {
            cfg = Configuration.Load<ClientConfiguration>(cfgPath);
            
            logger = new(cfg.LogPath);
            logger.Info("PARENT client initalized, welcome!");

            deviceData = new(Crypto.GetMACIdentifier(), cfg.Nickname);
            logger.Info($"Device Identifier: {deviceData.HardwareID}");

            socket = new(cfg.ServerAddress);
            socket.Connect();
            logger.Info($"Connected on {socket.Url}");
            socket.Send(Packet.Serialize(new DeviceInfoPacket(deviceData)));
            logger.Info("Device Information Package sent");

            Console.ReadKey(true);
            socket.Close();
        }
    }
}
