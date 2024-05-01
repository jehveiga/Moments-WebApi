namespace MomentsWebApi.Models
{
    public class Moment : Entity
    {

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
        public IEnumerable<Comment> Comments { get; set; } = [];
    }
}
