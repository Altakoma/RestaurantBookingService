using Microsoft.AspNetCore.Http;

namespace CatalogService.Application.TokenParsers.Interfaces
{
    public interface ITokenParser
    {
        int ParseSubjectId(IHeaderDictionary? headers);
    }
}
