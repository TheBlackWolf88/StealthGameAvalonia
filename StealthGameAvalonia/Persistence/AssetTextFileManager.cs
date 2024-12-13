using Avalonia.Platform;
using System;
using System.Collections.Generic;
using System.IO;

namespace StealthGameAvalonia.Persistence
{
    public class AssetTextFileManager(Uri uri) : ITextFileManager
    {
        public Uri FilePath { get; private set; } = uri;
        public string[] Load()
        {
            StreamReader sr = new(AssetLoader.Open(FilePath));
            List<string> file = [];
            while (!sr.EndOfStream)
            {
                string? line = sr.ReadLine();
                if (line != null)
                {
                    file.Add(line);
                }
            }

            return [.. file];
        }



    }
}
