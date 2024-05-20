namespace MomentsWebApi.ViewModels
{
    public record CommentViewModel
    {
        public int id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public int MomentId { get; set; }
    }
}
