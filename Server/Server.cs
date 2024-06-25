using PARENT.Common;
using RazorLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace PARENT.Server
{
    public class Handler : WebSocketBehavior
    {
        Server server;
        DeviceData? deviceData;

        public void Init(Server server) { this.server = server; }

        protected override void OnClose(CloseEventArgs e)
        {
            
        }

        protected override void OnError(WebSocketSharp.ErrorEventArgs e)
        {
            
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            Packet packet = Packet.Deserialize(e.Data);
            switch (packet)
            {
                case DeviceInfoPacket p:
                    {
                        if (server.Config.Devices.TryGetValue(p.DeviceData.HardwareID, out var dd))
                        {
                            deviceData = dd;
                            server.logger.Info($"Known device has connected: {p.DeviceData.Nickname} ({p.DeviceData.HardwareID})");
                            Send(Packet.Serialize(new DeviceInfoPacket(dd)));
                        } else
                        {
                            server.Config.Devices.Add(p.DeviceData.HardwareID, p.DeviceData);
                            deviceData = p.DeviceData;
                            server.logger.Info($"New device added, currently unassigned: {p.DeviceData.Nickname} ({p.DeviceData.HardwareID})");
                            Send(Packet.Serialize(new SystemPacket(SystemPacketKey.OK, refernceId: 1)));
                        }
                        server.Connections.Add(deviceData.Value.HardwareID, this);
                        break;
                    }
            }
        }

        protected override void OnOpen()
        {
            server.logger.Info($"Client connected, IP: {Context.UserEndPoint.Address}");
        }
    }

    public class Server
    {
        public readonly Common.Logger logger;

        public ServerConfiguration Config => cfg;
        public Dictionary<string, Handler> Connections => connections;

        readonly HttpServer webserver;
        readonly ServerConfiguration cfg;

        readonly Dictionary<string, Handler> connections = [];

        public readonly byte[] key;
        public readonly byte[] publicKey;

        readonly RazorLightEngine engine;

        public Server(string cfgPath)
        {
            cfg = Configuration.Load<ServerConfiguration>(cfgPath);
            logger = new(cfg.LogPath);
            logger.Info("PARENT Server initialized, welcome!");

            Directory.CreateDirectory(cfg.WebDocumentRoot);

            engine = new RazorLightEngineBuilder()
                .UseFileSystemProject(Path.GetFullPath(cfg.WebDocumentRoot))
                .UseMemoryCachingProvider()
                .Build();

            webserver = new(cfg.ServerUrl);
            webserver.AddWebSocketService<Handler>("/ws", a => a.Init(this));

            webserver.OnGet += OnWebServerGet;
            

            cfg.Save<ServerConfiguration>();
        }

        private void OnWebServerGet(object sender, HttpRequestEventArgs e)
        {
            var req = e.Request;
            var res = e.Response;
            var path = req.RawUrl;

            
        }
    }
}
