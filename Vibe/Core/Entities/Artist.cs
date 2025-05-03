using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Vibe.Core.Entities
{
    public record Artist
    {
        [Key]
        public int ArtistId { get; set; }
        public string Name { get; set; }

        public List<Track> Tracks { get; set; }
        public List<UserPreference> Preferences { get; set; }
    }
}
