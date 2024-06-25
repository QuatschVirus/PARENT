using Microsoft.Win32;
using PARENT.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class ClientConfiguration : Configuration
    {
        public string LogPath { get; set; } = "./PARENT-Client.log";

        public string ServerAddress { get; set; } = "ws://127.0.0.1";
        public string Nickname { get; set; }
    }
}
