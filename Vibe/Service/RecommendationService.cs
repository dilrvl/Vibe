using Vibe.Forms;
using Microsoft.EntityFrameworkCore;
using Vibe.Core.Entities;

namespace Vibe.Service
{
    internal class RecommendationService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly Random _random = new Random();
        /// <summary>
        ///  конструктор класса
        /// </summary>
        public RecommendationService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public List<Track> GetRecommendedSongs(int userId)
        {
            try
            {
                if (!_dbContext.Tracks.Any())
                {
                   
                    return GetDefaultTracks();
                }

                var userPreferences = _dbContext.UserPreferences
                    .Where(up => up.UserId == userId)
                    .ToList();

                if (userPreferences.Count == 0)
                {
                    
                    return GetPopularTracks(10);
                }

                var preferredGenreIds = userPreferences
                    .Where(up => up.GenreId.HasValue)
                    .Select(up => up.GenreId.Value)
                    .ToList();

                var preferredArtistIds = userPreferences
                    .Where(up => up.ArtistId.HasValue)
                    .Select(up => up.ArtistId.Value)
                    .ToList();

               

                var ratedTrackIds = _dbContext.TrackRatings
                    .Where(tr => tr.UserId == userId)
                    .Select(tr => tr.TrackId)
                    .ToList();

                var recommendedTracks = new List<Track>();

                // 1. Tracks by preferred artists
                if (preferredArtistIds.Any())
                {
                    var artistTracks = _dbContext.Tracks
                        .Include(t => t.Artist)
                        .Where(t => preferredArtistIds.Contains(t.ArtistId))
                        .Where(t => !ratedTrackIds.Contains(t.TrackId))
                        .OrderByDescending(t => t.PopularityScore)
                        .Take(5)
                        .ToList();

                   
                    recommendedTracks.AddRange(artistTracks);
                }

                // 2. Tracks by preferred genres
                if (preferredGenreIds.Any() && recommendedTracks.Count < 10)
                {
                    var genreTracks = _dbContext.Tracks
                        .Include(t => t.Artist)
                        .Where(t => preferredGenreIds.Contains(t.GenreId))
                        .Where(t => !ratedTrackIds.Contains(t.TrackId))
                        .Where(t => !recommendedTracks.Select(rt => rt.TrackId).Contains(t.TrackId))
                        .OrderByDescending(t => t.PopularityScore)
                        .Take(10 - recommendedTracks.Count)
                        .ToList();

                    
                    recommendedTracks.AddRange(genreTracks);
                }

                // 3. Fill with popular tracks if needed
                if (recommendedTracks.Count < 10)
                {
                    var popularTracks = _dbContext.Tracks
                        .Include(t => t.Artist)
                        .Where(t => !ratedTrackIds.Contains(t.TrackId))
                        .Where(t => !recommendedTracks.Select(rt => rt.TrackId).Contains(t.TrackId))
                        .OrderByDescending(t => t.PopularityScore)
                        .Take(10 - recommendedTracks.Count)
                        .ToList();

                   
                    recommendedTracks.AddRange(popularTracks);
                }

                if (!recommendedTracks.Any())
                {
                   
                    recommendedTracks = _dbContext.Tracks
                        .Include(t => t.Artist)
                        .Take(10)
                        .ToList();
                }

                if (!recommendedTracks.Any())
                {
                   
                    return GetDefaultTracks();
                }

                return recommendedTracks.OrderBy(x => _random.Next()).ToList();
            }
            catch (Exception ex)
            {
               
                return GetDefaultTracks();
            }
        }

        private List<Track> GetPopularTracks(int count)
        {
            try
            {
                var tracks = _dbContext.Tracks
                    .Include(t => t.Artist)
                    .OrderByDescending(t => t.PopularityScore)
                    .Take(count)
                    .ToList();

                return tracks.Any() ? tracks : GetDefaultTracks();
            }
            catch (Exception ex)
            {
               
                return GetDefaultTracks();
            }
        }

        private List<Track> GetDefaultTracks()
        {
            try
            {
                if (!_dbContext.Tracks.Any())
                {
                    return new List<Track>
                    {
                        new Track
                        {
                            TrackId = -1,
                            Title = "Нет доступных треков",
                            Artist = new Artist { ArtistId = -1, Name = "Неизвестный исполнитель" },
                            AlbumArtPath = "C:\\Users\\Admin\\Pictures\\треки\\1473685252276134903.jpg",
                            PopularityScore = 50
                        }
                    };
                }

                return _dbContext.Tracks
                    .Include(t => t.Artist)
                    .Take(10)
                    .ToList();
            }
            catch (Exception ex)
            {
              
                return new List<Track>
                {
                    new Track
                    {
                        TrackId = -1,
                        Title = "Ошибка загрузки",
                        Artist = new Artist { ArtistId = -1, Name = "Неизвестный исполнитель" },
                        AlbumArtPath = "C:\\Users\\Admin\\Pictures\\треки\\1473685252276134903.jpg",
                        PopularityScore = 50
                    }
                };
            }
        }

        public void AddToFavorites(int userId, int trackId)
{
    try
    {
        if (!_dbContext.Users.Any(u => u.UserId == userId))
        {
            throw new InvalidOperationException($"Пользователь с ID {userId} не найден.");
        }

        if (!_dbContext.Tracks.Any(t => t.TrackId == trackId))
        {
            throw new InvalidOperationException($"Трек с ID {trackId} не найден.");
        }

        if (_dbContext.TrackRatings.Any(tr => tr.UserId == userId && tr.TrackId == trackId))
        {
            
            return;
        }

        _dbContext.TrackRatings.Add(new TrackRating
        {
            UserId = userId,
            TrackId = trackId,
            Rating = 1 // Set Rating to 1 to indicate favorite
        });

        _dbContext.SaveChanges();
        
    }
    catch (Exception ex)
    {
        
        throw;
    }
}

        public void RemoveFromFavorites(int userId, int trackId)
        {
            try
            {
                var favorite = _dbContext.TrackRatings
                    .FirstOrDefault(f => f.UserId == userId && f.TrackId == trackId);

                if (favorite != null)
                {
                    _dbContext.TrackRatings.Remove(favorite);
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
              
            }
        }

        public List<Track> GetFavoriteTracks(int userId)
        {
            try
            {
                var favoriteTracks = _dbContext.TrackRatings
                    .Where(tr => tr.UserId == userId && tr.Rating == 1) // Filter for favorited tracks (Rating = 1)
                    .Select(tr => tr.Track)
                    .Include(t => t.Artist)
                    .Distinct()
                    .ToList();

                favoriteTracks = favoriteTracks.Where(t => t != null).ToList();
              
                return favoriteTracks;
            }
            catch (Exception ex)
            {
               
                return new List<Track>();
            }
        }

        public void SaveUserPreferences(int userId, List<int> genreIds, List<int> artistIds)
        {
            try
            {
                var existingPreferences = _dbContext.UserPreferences
                    .Where(up => up.UserId == userId)
                    .ToList();

                _dbContext.UserPreferences.RemoveRange(existingPreferences);

                foreach (var genreId in genreIds ?? new List<int>())
                {
                    _dbContext.UserPreferences.Add(new UserPreference
                    {
                        UserId = userId,
                        GenreId = genreId,
                        ArtistId = null
                    });
                }

                foreach (var artistId in artistIds ?? new List<int>())
                {
                    _dbContext.UserPreferences.Add(new UserPreference
                    {
                        UserId = userId,
                        GenreId = null,
                        ArtistId = artistId
                    });
                }

                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
               
                throw;
            }
        }
    }
}
