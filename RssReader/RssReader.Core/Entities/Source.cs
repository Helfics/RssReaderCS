using System;
using SQLite;

namespace RssReader.Core.Entities
{
    public class Source
    {
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Uri { get; set; }

        public DateTime CreatedAt { get; set; }

        public Source()
        {
            
        }
        
        public Source(string title, string uri)
        {
            Title = title;
            Uri = uri;
            CreatedAt = DateTime.Now;
        }
    }
}