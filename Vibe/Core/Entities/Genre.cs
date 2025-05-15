using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;


namespace Vibe.Core.Entities
{
    /// <summary>
    ///  Таблица с жанрами
    /// </summary>
    public record Genre
    {
        [Key]
        public int GenreId { get; set; }
        public string Name { get; set; }

        // Связь с треками
        public List<Track> Tracks { get; set; }
        public List<UserPreference> Preferences { get; set; }
    }

}
