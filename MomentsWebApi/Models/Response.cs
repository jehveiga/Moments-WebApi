namespace MomentsWebApi.Models
{
    public class Response<T> where T : class
    {
        public string? Message { get; set; }
        public T? Data { get; set; }
    }
}
