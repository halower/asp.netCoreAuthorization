using AuthorizationForoNetCore.IRepository;
using System.Collections.Generic;
using System.Linq;
using AuthorizationForoNetCore.Modles;

namespace AuthorizationForoNetCore.Repository
{
    public class FakeDocumentRepository : IDocumentRepository
    {
        static List<Document> _documents = new List<Document> {
            new Document { Id = 1, Author = "halower" },
            new Document { Id = 2, Author = "others" }
        };
        public IEnumerable<Document> Get()
        {
            return _documents;
        }

        public Document Get(int id)
        {
            return _documents.FirstOrDefault(d => d.Id == id);
        }
    }
}
