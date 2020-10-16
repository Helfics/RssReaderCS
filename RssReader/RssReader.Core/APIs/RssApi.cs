using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Toolkit.Parsers.Rss;
using RssReader.Core.Entities;

namespace RssReader.Core.APIs
{
    public class RssApi
    {
        public async Task<List<RssItem>> GetAsync(string uri)
        {
            //return new List<RssItem>()
            //{
            //    new RssItem
            //    {
            //        Id = 1,
            //        Title = "aaaa",
            //        Description = "bbbbb",
            //        PublishDate = DateTime.Now
            //    }
            //};

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(uri))
                {
                    var content = await response.Content.ReadAsStringAsync();

                    var items = new RssParser().Parse(content)
                        //.Descendants("item")
                        .Select(x => new RssItem
                        {
                            Title = x.Title,
                            Description = x.Content,
                            PublishDate = x.PublishDate,
                            ImageUrl = x.ImageUrl,
                            IsRead = false
                        }).ToList();

                    return items;
                }

            }

        }
    }
}