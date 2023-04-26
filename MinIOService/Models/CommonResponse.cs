namespace MinIOService.Models
{
    public class CommonResponse<T>
    {
        public T? Data { get; set; }
        public string Message { get; set; } = "Ok";
        public int StatusCode { get; set; } = 200;
    }
}
