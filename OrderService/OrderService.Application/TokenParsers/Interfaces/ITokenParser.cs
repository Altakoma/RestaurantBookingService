using Microsoft.AspNetCore.Http;

namespace OrderService.Application.TokenParsers.Interfaces
{
    public interface ITokenParser
    {
        int ParseSubjectId(IHeaderDictionary? headers);
    }
}
