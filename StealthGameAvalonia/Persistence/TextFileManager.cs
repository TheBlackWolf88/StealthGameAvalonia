using System;
using System.IO;

namespace StealthGameAvalonia.Persistence
{
    public class TextFileManager : ITextFileManager
    {
        public string FilePath { get; private set; }

        public TextFileManager(string filePath)
        {
            FilePath = filePath;
        }

        public string[] Load()
        {
            return File.Exists(FilePath)
                ? File.ReadAllLines(FilePath)
                : throw new ArgumentException("The filepath doesn't exists!");
        }
    }
}
