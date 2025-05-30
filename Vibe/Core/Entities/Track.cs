﻿using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Vibe.Core.Entities
{
    /// <summary>
    ///  Таблица с треками
    /// </summary>
    public record Track
    {
        [Key]
        public int TrackId { get; set; }
        public string Title { get; set; }
        public int ArtistId { get; set; }
        public int GenreId { get; set; }
        public string AlbumArtPath { get; set; } // Путь к изображению обложки
        public int PopularityScore { get; set; } = 50;

        // Навигационные свойства
        public Artist Artist { get; set; }
        public Genre Genre { get; set; }
        public List<TrackRating> Ratings { get; set; }
        public List<UserPreference> Preferences { get; set; }
    }
}
