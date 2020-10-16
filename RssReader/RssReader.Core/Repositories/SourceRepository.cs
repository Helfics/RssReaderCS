using System.Collections.Generic;
using RssReader.Core.Entities;
using SQLite;

namespace RssReader.Core.Repositories
{
    public class SourceRepository
    {
        private readonly string _connectionString;

        public SourceRepository(string connectionString)
        {
            _connectionString = connectionString;

            using (var context = new SQLiteConnection(_connectionString))
            {
                context.CreateTable<Source>();
            }
        }

        public int Add(Source sourceToAdd)
        {
            using (var context = new SQLiteConnection(_connectionString))
            {
                context.Insert(sourceToAdd);
            }

            return sourceToAdd.Id;
        }

        public List<Source> Get()
        {
            List<Source> sources = null;
            
            using (var context = new SQLiteConnection(_connectionString))
            {
                sources = context.Table<Source>().ToList();
            }
            
            return sources;
        }

        public Source GetById(int id)
        {
            Source matchingSource = null;
            
            using (var context = new SQLiteConnection(_connectionString))
            {
                matchingSource = context.Table<Source>().FirstOrDefault(x => x.Id == id);
            }
            
            return matchingSource;            
        }
    }
}