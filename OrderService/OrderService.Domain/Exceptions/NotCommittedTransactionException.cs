namespace OrderService.Domain.Exceptions
{
    public class NotCommittedTransactionException : Exception
    {
        public NotCommittedTransactionException(string message) : base(message)
        {
        }
    }
}
