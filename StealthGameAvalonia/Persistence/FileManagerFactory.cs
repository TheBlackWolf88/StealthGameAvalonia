using System;

namespace StealthGameAvalonia.Persistence
{
    public static class FileManagerFactory
    {
        public static ITextFileManager CreateTextFileManager(string levelPath)
        {
            return levelPath.StartsWith("avares") ? new AssetTextFileManager(new Uri(levelPath)) : new TextFileManager(levelPath);
        }
    }
}
