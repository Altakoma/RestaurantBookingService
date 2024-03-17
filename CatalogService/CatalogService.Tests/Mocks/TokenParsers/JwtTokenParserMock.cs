using CatalogService.Application.TokenParsers.Interfaces;
using Microsoft.AspNetCore.Http;
using Moq;

namespace CatalogService.Tests.Mocks.TokenParsers
{
    public class TokenParserMock : Mock<ITokenParser>
    {
        public TokenParserMock MockParseSubjectId(int subjectId)
        {
            Setup(tokenParser => tokenParser.ParseSubjectId(It.IsAny<IHeaderDictionary>()))
            .Returns(subjectId)
            .Verifiable();

            return this;
        }
    }
}
