using System.ComponentModel.DataAnnotations;

namespace MomentsWebApi.Models
{
    public abstract class Entity
    {
        [Key]
        public int Id { get; set; }
    }
}
