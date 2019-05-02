using MongoDB.Driver;
using SingleSignOn.Data.Documents;
using SingleSignOn.Data.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SingleSignOn.Data.Repositories
{
    public class BaseRepository<TDocument> : IBaseRepository<TDocument> where TDocument : BaseDocument
    {
        protected IMongoCollection<TDocument> collection;

        public string Create(TDocument document)
        {
            collection.InsertOne(document);
            return document.Id;
        }

        public async Task<string> CreateAsync(TDocument document)
        {
            await collection.InsertOneAsync(document);
            return document.Id;
        }

        public void Delete(TDocument document)
        {
            collection.DeleteOne(x => x.Id == document.Id);
        }

        public async Task DeleteAsync(TDocument document)
        {
            await collection.DeleteOneAsync(_ => _.Id == document.Id);
        }

        public List<TDocument> GetAll()
        {
            return collection.Find(x => true).ToList();
        }

        public async Task<List<TDocument>> GetAllAsync()
        {
            var a = await collection.FindAsync(_ => true);
            return await a.ToListAsync();
        }

        public TDocument GetById(string id)
        {
            return collection.Find(x => x.Id == id).FirstOrDefault();
        }

        public async Task<TDocument> GetByIdAsync(string id)
        {
            var a = await collection.FindAsync(x => x.Id == id);
            return a.FirstOrDefault();
        }

        public void Replace(TDocument document)
        {
            collection.ReplaceOne(x => x.Id == document.Id, document);
        }

        public async Task ReplaceAsync(TDocument document)
        {
            await collection.ReplaceOneAsync(x => x.Id == document.Id, document);
        }
    }
}
