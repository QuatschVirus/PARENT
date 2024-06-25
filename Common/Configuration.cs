using PARENT.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PARENT.Server
{
    public class Configuration
    {
        protected readonly JsonSerializerOptions options = new()
        {
            WriteIndented = true
        };

        public const string DefaultConfigPath = "./PARENT-cfg.json";

        protected string path;

        public void Save<T>() where T : Configuration
        {
            string json = JsonSerializer.Serialize((T)this, options);
            File.WriteAllText(path, json);
        }

        public static T Load<T>(string path) where T : Configuration, new()
        {
            path ??= DefaultConfigPath;
            path = Path.GetFullPath(path);

            if (!File.Exists(path))
            {
                T c = new() { path = path };
                c.Save<T>();
                return c;
            }
            else
            {
                string json = File.ReadAllText(path);
                T cfg = JsonSerializer.Deserialize<T>(json);
                cfg.path = path;
                return cfg;
            }
        }

        public Configuration() { }

        public Configuration(string path)
        {
            this.path = path;
        }

        
    }
}
