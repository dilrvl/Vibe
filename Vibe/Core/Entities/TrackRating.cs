using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Vibe.Core.Entities
{
    /// <summary>
    ///  Таблица с оцененными треками пользователя
    /// </summary>
    public record TrackRating
    {
        [Key]
        public int RaitingId { get; set; }
        public int UserId { get; set; }
        public int TrackId { get; set; }
        public int Rating { get; set; }

        // Навигационные свойства
        public User User { get; set; }
        public Track Track { get; set; }
    }
}
