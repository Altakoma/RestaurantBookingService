using Hangfire;
using Moq;
using System.Linq.Expressions;

namespace OrderService.Tests.Mocks.BackgroundJobClients
{
    public class BackgroundJobClientMock : Mock<IBackgroundJobClient>
    {
        public BackgroundJobClientMock MockEnqueue(Expression<Func<Task>> func)
        {
            Setup(backgroundJobClient => backgroundJobClient.Enqueue(func))
            .Verifiable();

            return this;
        }
    }
}
