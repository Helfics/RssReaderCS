using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RssReader.Core.Entities;
using RssReader.Core.Repositories;

namespace RssReader.Core.Services
{
    public class SourceService
    {
        private readonly SourceRepository _sourceRepository;

        public SourceService(string connectionString)
        {
/*
            if (File.Exists(connectionString))
            {
                File.Delete(connectionString);
            }
*/
            _sourceRepository = new SourceRepository(connectionString);
        }


        public List<Source> Get()
        {
            return _sourceRepository.Get();
        }
        
        public Source GetById(int id)
        {
            return _sourceRepository.GetById(id);
        }

        public int Add(string title, string url)
        {
            bool result = false;
            
            var source = new Source(title, url);
            
            var id = _sourceRepository.Add(source);
    
            return id;
        }

        public bool Delete(int id)
        {
            // bool result = false;
            //
            // var matchingSource = _sources.FirstOrDefault(x => x.Id == id);
            //
            // if (matchingSource != null)
            // {
            //     _sources.Remove(matchingSource);
            //     result = true;
            // }
            //
            // return result;
            return false;
        }
    }
}