namespace OrderService.Application.DTOs.Exception
{
    public class ExceptionDTO
    {
        public string Message { get; set; } = null!;
        public string ExceptionType { get; set; } = null!;
        public object? Data { get; set; }
    }
}
