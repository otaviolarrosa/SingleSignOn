using SingleSignOn.Data.Documents;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SingleSignOn.Data.Interfaces.Repositories
{
    public interface IBaseRepository<TDocument> where TDocument : BaseDocument
    {
        List<TDocument> GetAll();
        Task<List<TDocument>> GetAllAsync();
        TDocument GetById(string id);
        Task<TDocument> GetByIdAsync(string id);
        string Create(TDocument document);
        Task<string> CreateAsync(TDocument document);
        void Replace(TDocument document);
        Task ReplaceAsync(TDocument document);
        void Delete(TDocument document);
        Task DeleteAsync(TDocument document);
    }
}
