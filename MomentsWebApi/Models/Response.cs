namespace MomentsWebApi.Models
{
    public class Response<T> where T : class
    {
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
    }
}
