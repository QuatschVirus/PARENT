using PARENT.Common;
using PARENT.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARENT.Server
{
    public class ServerConfiguration : Configuration
    {
        public Dictionary<string, DeviceData> Devices { get; set; } = [];
        public Dictionary<int, ProfileData> Profiles { get; set; } = [];
        public string LogPath { get; set; } = "./PARENT-Server.log";
        public string ServerUrl { get; set; } = "http://localhost";
        public string WebDocumentRoot { get; set; } = "./WebInterface";
    }
}
