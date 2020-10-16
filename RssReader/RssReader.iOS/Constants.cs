using System;
using System.IO;

namespace RssReader.iOS
{
    public static class Constants
    {
        public static string ConnectionString
        {
            get
            {
                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "..", "Library", "Database");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                return Path.Combine(path, "rssreader.db3");
            }
        }
    }
}
