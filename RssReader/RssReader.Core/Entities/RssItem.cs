using System;
using SQLite;

namespace RssReader.Core.Entities
{
    public class RssItem
    {
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishDate { get; set; }
        public string ImageUrl { get; set; }
        public bool IsRead { get; set; }
    }
}