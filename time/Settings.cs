using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

namespace time
{
    public class Settings
    {
        public bool RoundOT { get; set; } = true;
        public bool SplitSaturday { get; set; } = true;
        public static Settings LoadSettings(string path)
        {

            if (!File.Exists(path))
            {
                return new Settings();
            }

            string content = File.ReadAllText(path);
            Settings s = JsonSerializer.Deserialize<Settings>(content);
            return s;
        }
        public void Save(string path)
        {
            JsonSerializerOptions options = new JsonSerializerOptions() { WriteIndented = true };
            string content = JsonSerializer.Serialize<Settings>(this, options);
            File.WriteAllText(path, content);
        }
    }
}
