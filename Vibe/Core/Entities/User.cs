using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Vibe.Core.Entities
{
    public record User
    {
        [Key]
        public int UserId { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }

        // Связь с предпочтениями и рейтингами
        public List<UserPreference> Preferences { get; set; }
        public List<TrackRating> Ratings { get; set; }
    }
}
