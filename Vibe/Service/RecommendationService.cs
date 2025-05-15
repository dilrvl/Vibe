using Vibe.Forms;
using Microsoft.EntityFrameworkCore;
using Vibe.Core.Entities;

namespace Vibe.Service
{
    internal class RecommendationService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly Random _random = new Random();

        public RecommendationService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Track> GetRecommendedSongs(int userId)
        {
            try
            {
 
                bool hasTracks = false;
                foreach (var track in _dbContext.Tracks)
                {
                    hasTracks = true;
                    break;
                }

                if (!hasTracks)
                {
                    return GetDefaultTracks();
                }

                // получить прдепочтения пользователя
                List<UserPreference> userPreferences = new List<UserPreference>();
                foreach (var pref in _dbContext.UserPreferences)
                {
                    if (pref.UserId == userId)
                    {
                        userPreferences.Add(pref);
                    }
                }
                if (userPreferences.Count == 0)
                {
                    return GetPopularTracks(10);
                }
                List<int> preferredGenreIds = new List<int>();
                foreach (var pref in userPreferences)
                {
                    if (pref.GenreId.HasValue)
                    {
                        preferredGenreIds.Add(pref.GenreId.Value);
                    }
                }
                List<int> preferredArtistIds = new List<int>();
                foreach (var pref in userPreferences)
                {
                    if (pref.ArtistId.HasValue)
                    {
                        preferredArtistIds.Add(pref.ArtistId.Value);
                    }
                }
                List<int> ratedTrackIds = new List<int>();
                foreach (var rating in _dbContext.TrackRatings)
                {
                    if (rating.UserId == userId)
                    {
                        ratedTrackIds.Add(rating.TrackId);
                    }
                }
                List<Track> recommendedTracks = new List<Track>();
                if (preferredArtistIds.Count > 0)
                {
                    List<Track> artistTracks = new List<Track>();
                    foreach (var track in _dbContext.Tracks)
                    {

                        track.Artist = _dbContext.Artists.FirstOrDefault(a => a.ArtistId == track.ArtistId);

                        bool isPreferredArtist = false;
                        foreach (var artistId in preferredArtistIds)
                        {
                            if (track.ArtistId == artistId)
                            {
                                isPreferredArtist = true;
                                break;
                            }
                        }
                        bool isRated = false;
                        foreach (var ratedId in ratedTrackIds)
                        {
                            if (track.TrackId == ratedId)
                            {
                                isRated = true;
                                break;
                            }
                        }
                        if (isPreferredArtist && !isRated)
                        {
                            artistTracks.Add(track);
                        }
                    }
                    for (int i = 0; i < artistTracks.Count - 1; i++)
                    {
                        for (int j = i + 1; j < artistTracks.Count; j++)
                        {
                            if (artistTracks[i].PopularityScore < artistTracks[j].PopularityScore)
                            {
                                var temp = artistTracks[i];
                                artistTracks[i] = artistTracks[j];
                                artistTracks[j] = temp;
                            }
                        }
                    }
                    int count = Math.Min(5, artistTracks.Count);
                    for (int i = 0; i < count; i++)
                    {
                        recommendedTracks.Add(artistTracks[i]);
                    }
                }

                if (preferredGenreIds.Count > 0 && recommendedTracks.Count < 10)
                {
                    List<Track> genreTracks = new List<Track>();
                    foreach (var track in _dbContext.Tracks)
                    {
                        track.Artist = _dbContext.Artists.FirstOrDefault(a => a.ArtistId == track.ArtistId);

                        bool isPreferredGenre = false;
                        foreach (var genreId in preferredGenreIds)
                        {
                            if (track.GenreId == genreId)
                            {
                                isPreferredGenre = true;
                                break;
                            }
                        }

                        bool isRated = false;
                        foreach (var ratedId in ratedTrackIds)
                        {
                            if (track.TrackId == ratedId)
                            {
                                isRated = true;
                                break;
                            }
                        }
                        bool alreadyRecommended = false;
                        foreach (var recTrack in recommendedTracks)
                        {
                            if (track.TrackId == recTrack.TrackId)
                            {
                                alreadyRecommended = true;
                                break;
                            }
                        }
                        if (isPreferredGenre && !isRated && !alreadyRecommended)
                        {
                            genreTracks.Add(track);
                        }
                    }
                    for (int i = 0; i < genreTracks.Count - 1; i++)
                    {
                        for (int j = i + 1; j < genreTracks.Count; j++)
                        {
                            if (genreTracks[i].PopularityScore < genreTracks[j].PopularityScore)
                            {
                                var temp = genreTracks[i];
                                genreTracks[i] = genreTracks[j];
                                genreTracks[j] = temp;
                            }
                        }
                    }
                    int remaining = 10 - recommendedTracks.Count;
                    int count = Math.Min(remaining, genreTracks.Count);
                    for (int i = 0; i < count; i++)
                    {
                        recommendedTracks.Add(genreTracks[i]);
                    }
                }
                if (recommendedTracks.Count < 10)
                {
                    List<Track> popularTracks = new List<Track>();
                    foreach (var track in _dbContext.Tracks)
                    {
                        track.Artist = _dbContext.Artists.FirstOrDefault(a => a.ArtistId == track.ArtistId);

                        bool isRated = false;
                        foreach (var ratedId in ratedTrackIds)
                        {
                            if (track.TrackId == ratedId)
                            {
                                isRated = true;
                                break;
                            }
                        }

                        bool alreadyRecommended = false;
                        foreach (var recTrack in recommendedTracks)
                        {
                            if (track.TrackId == recTrack.TrackId)
                            {
                                alreadyRecommended = true;
                                break;
                            }
                        }

                        if (!isRated && !alreadyRecommended)
                        {
                            popularTracks.Add(track);
                        }
                    }
                    for (int i = 0; i < popularTracks.Count - 1; i++)
                    {
                        for (int j = i + 1; j < popularTracks.Count; j++)
                        {
                            if (popularTracks[i].PopularityScore < popularTracks[j].PopularityScore)
                            {
                                var temp = popularTracks[i];
                                popularTracks[i] = popularTracks[j];
                                popularTracks[j] = temp;
                            }
                        }
                    }
                    int remaining = 10 - recommendedTracks.Count;
                    int count = Math.Min(remaining, popularTracks.Count);
                    for (int i = 0; i < count; i++)
                    {
                        recommendedTracks.Add(popularTracks[i]);
                    }
                }
                bool hasRecommendations = false;
                foreach (var track in recommendedTracks)
                {
                    hasRecommendations = true;
                    break;
                }
                if (!hasRecommendations)
                {
                    recommendedTracks.Clear();
                    int count = 0;
                    foreach (var track in _dbContext.Tracks)
                    {
                        track.Artist = _dbContext.Artists.FirstOrDefault(a => a.ArtistId == track.ArtistId);
                        recommendedTracks.Add(track);
                        count++;
                        if (count >= 10)
                        {
                            break;
                        }
                    }
                }
                hasRecommendations = false;
                foreach (var track in recommendedTracks)
                {
                    hasRecommendations = true;
                    break;
                }
                if (!hasRecommendations)
                {
                    return GetDefaultTracks();
                }
                List<Track> shuffledTracks = new List<Track>();
                while (recommendedTracks.Count > 0)
                {
                    int index = _random.Next(recommendedTracks.Count);
                    shuffledTracks.Add(recommendedTracks[index]);
                    recommendedTracks.RemoveAt(index);
                }

                return shuffledTracks;
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
                List<Track> tracks = new List<Track>();
                foreach (var track in _dbContext.Tracks)
                {
                    track.Artist = _dbContext.Artists.FirstOrDefault(a => a.ArtistId == track.ArtistId);
                    tracks.Add(track);
                }

                // Sort by PopularityScore descending
                for (int i = 0; i < tracks.Count - 1; i++)
                {
                    for (int j = i + 1; j < tracks.Count; j++)
                    {
                        if (tracks[i].PopularityScore < tracks[j].PopularityScore)
                        {
                            var temp = tracks[i];
                            tracks[i] = tracks[j];
                            tracks[j] = temp;
                        }
                    }
                }
                List<Track> result = new List<Track>();
                int maxCount = Math.Min(count, tracks.Count);
                for (int i = 0; i < maxCount; i++)
                {
                    result.Add(tracks[i]);
                }

                bool hasTracks = false;
                foreach (var track in result)
                {
                    hasTracks = true;
                    break;
                }
                return hasTracks ? result : GetDefaultTracks();
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
                bool hasTracks = false;
                foreach (var track in _dbContext.Tracks)
                {
                    hasTracks = true;
                    break;
                }

                if (!hasTracks)
                {
                    List<Track> defaultTracks = new List<Track>();
                    defaultTracks.Add(new Track
                    {
                        TrackId = -1,
                        Title = "Нет доступных треков",
                        Artist = new Artist { ArtistId = -1, Name = "Неизвестный исполнитель" },
                        AlbumArtPath = "C:\\Users\\Admin\\Pictures\\треки\\1473685252276134903.jpg",
                        PopularityScore = 50
                    });
                    return defaultTracks;
                }

                List<Track> result = new List<Track>();
                int count = 0;
                foreach (var track in _dbContext.Tracks)
                {
                    track.Artist = _dbContext.Artists.FirstOrDefault(a => a.ArtistId == track.ArtistId);
                    result.Add(track);
                    count++;
                    if (count >= 10)
                    {
                        break;
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                List<Track> errorTracks = new List<Track>();
                errorTracks.Add(new Track
                {
                    TrackId = -1,
                    Title = "Ошибка загрузки",
                    Artist = new Artist { ArtistId = -1, Name = "Неизвестный исполнитель" },
                    AlbumArtPath = "C:\\Users\\Admin\\Pictures\\треки\\1473685252276134903.jpg",
                    PopularityScore = 50
                });
                return errorTracks;
            }
        }

        public void AddToFavorites(int userId, int trackId)
        {
            try
            {
                bool userExists = false;
                foreach (var user in _dbContext.Users)
                {
                    if (user.UserId == userId)
                    {
                        userExists = true;
                        break;
                    }
                }

                if (!userExists)
                {
                    throw new InvalidOperationException($"Пользователь с ID {userId} не найден.");
                }

                bool trackExists = false;
                foreach (var track in _dbContext.Tracks)
                {
                    if (track.TrackId == trackId)
                    {
                        trackExists = true;
                        break;
                    }
                }
                if (!trackExists)
                {
                    throw new InvalidOperationException($"Трек с ID {trackId} не найден.");
                }

                bool alreadyRated = false;
                foreach (var rating in _dbContext.TrackRatings)
                {
                    if (rating.UserId == userId && rating.TrackId == trackId)
                    {
                        alreadyRated = true;
                        break;
                    }
                }

                if (alreadyRated)
                {
                    return;
                }

                _dbContext.TrackRatings.Add(new TrackRating
                {
                    UserId = userId,
                    TrackId = trackId,
                    Rating = 1
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
                TrackRating favorite = null;
                foreach (var rating in _dbContext.TrackRatings)
                {
                    if (rating.UserId == userId && rating.TrackId == trackId)
                    {
                        favorite = rating;
                        break;
                    }
                }

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
                List<Track> favoriteTracks = new List<Track>();
                List<int> addedTrackIds = new List<int>(); 

                foreach (var rating in _dbContext.TrackRatings)
                {
                    if (rating.UserId == userId && rating.Rating == 1)
                    {
                        Track track = null;
                        foreach (var t in _dbContext.Tracks)
                        {
                            if (t.TrackId == rating.TrackId)
                            {
                                track = t;
                                break;
                            }
                        }

                        if (track != null)
                        {
                            track.Artist = _dbContext.Artists.FirstOrDefault(a => a.ArtistId == track.ArtistId);

                            bool alreadyAdded = false;
                            foreach (var addedId in addedTrackIds)
                            {
                                if (addedId == track.TrackId)
                                {
                                    alreadyAdded = true;
                                    break;
                                }
                            }

                            if (!alreadyAdded)
                            {
                                favoriteTracks.Add(track);
                                addedTrackIds.Add(track.TrackId);
                            }
                        }
                    }
                }


                List<Track> filteredTracks = new List<Track>();
                foreach (var track in favoriteTracks)
                {
                    if (track != null)
                    {
                        filteredTracks.Add(track);
                    }
                }

                return filteredTracks;
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
                List<UserPreference> existingPreferences = new List<UserPreference>();
                foreach (var pref in _dbContext.UserPreferences)
                {
                    if (pref.UserId == userId)
                    {
                        existingPreferences.Add(pref);
                    }
                }

                foreach (var pref in existingPreferences)
                {
                    _dbContext.UserPreferences.Remove(pref);
                }

                if (genreIds == null)
                {
                    genreIds = new List<int>();
                }

                foreach (var genreId in genreIds)
                {
                    _dbContext.UserPreferences.Add(new UserPreference
                    {
                        UserId = userId,
                        GenreId = genreId,
                        ArtistId = null
                    });
                }

                if (artistIds == null)
                {
                    artistIds = new List<int>();
                }

                foreach (var artistId in artistIds)
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