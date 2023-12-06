using Microsoft.AspNetCore.Http;
using Moq;
using OrderService.Application.TokenParsers.Interfaces;

namespace OrderService.Tests.Mocks.TokenParsers
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
