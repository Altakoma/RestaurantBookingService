namespace OrderService.Application.Interfaces.Command
{
    public class Transactional
    {
        public bool IsTransactionSkipped { get; set; } = false;
    }
}
