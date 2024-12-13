using Avalonia.Platform;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StealthGameAvalonia.Persistence
{
    public static class FileLocator
    {
        public static List<KVP<string, string>> GetFiles()
        {
            List<KVP<string, string>> ret = [];
            //custom maps only available on pc
            if (OperatingSystem.IsWindows() || OperatingSystem.IsLinux() || OperatingSystem.IsMacOS())
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                path = Path.Combine(path, "My Games");
                _ = Directory.CreateDirectory(path);
                path = Path.Combine(path, "StealthGame");
                _ = Directory.CreateDirectory(path);
                string[] fnames = Directory.GetFiles(path);
                foreach (string f in fnames)
                {
                    string lvlName = File.ReadLines(f.ToString()).First();
                    ret.Add(new KVP<string, string>(lvlName, f.ToString()));

                }
            }

            IEnumerable<Uri> defMaps = AssetLoader.GetAssets(new Uri(@"avares://StealthGameAvalonia/Assets/Levels"), null);

            foreach (Uri f in defMaps)
            {
                string lvlName = new StreamReader(AssetLoader.Open(f, null)).ReadLine()!;
                ret.Add(new KVP<string, string>(lvlName!, f.ToString()));
            }

            return ret;
        }
    }
}
