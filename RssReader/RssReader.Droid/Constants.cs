using System;
using System.IO;

namespace RssReader.Droid
{
    public static class Constants
    {
        public const int CREATE_SOURCE_ID = 1;
        public const int READ_SOURCE_ID = 1;

        public static string ConnectionString
        {
            get => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "rssreader.db3");
        }
    }
}