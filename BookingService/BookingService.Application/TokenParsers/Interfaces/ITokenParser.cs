using Microsoft.AspNetCore.Http;

namespace BookingService.Application.TokenParsers.Interfaces
{
    public interface ITokenParser
    {
        int ParseSubjectId(IHeaderDictionary? headers);
    }
}
