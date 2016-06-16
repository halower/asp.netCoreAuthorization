using AuthorizationForoNetCore.Modles;
using System.Collections.Generic;

namespace AuthorizationForoNetCore.IRepository
{
    public interface IDocumentRepository
    {
        IEnumerable<Document> Get();
        Document Get(int id);
    }
}
