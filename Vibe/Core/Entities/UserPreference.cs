using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Vibe.Core.Entities
{
    public record UserPreference
    {
        [Key]
        public int PreferenceId { get; set; }
        public int UserId { get; set; }
        public int? ArtistId { get; set; }
        public int? GenreId { get; set; }

        // Навигационные свойства
        public User User { get; set; }
        public Artist Artist { get; set; }
        public Genre Genre { get; set; }
    }
}
