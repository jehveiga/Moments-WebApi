namespace MomentsWebApi.Models
{
    public class Comment : Entity
    {
        public string UserName { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int MomentId { get; set; }

        // EF Relation
        public Moment Moment { get; set; } = new();
    }
}