using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vibe.Forms;
using Microsoft.EntityFrameworkCore;
using Vibe.Core.Entities;

namespace Vibe.Service
{
    internal class RecommendationService
    {
        private readonly ApplicationDbContext _dbContext;

        public RecommendationService(ApplicationDbContext context)
        {
            _dbContext = context;
        }
        public List<Track> GetRecommendedSongs(int userId)
        {
            //cоздание списков для хранения данных
            var preferences = new List<UserPreference>();
            var likedTracks = new List<int>();
            var dislikedTracks = new List<int>();
            var preferredArtists = new List<int>();
            var preferredGenres = new List<int>();
            var similarUserIds = new List<int>();
            var songsFromSimilarUsers = new List<int>();
            //получение предпочтений пользователя
            foreach (var pref in _dbContext.UserPreferences)
            {
                if (pref.UserId == userId)
                {
                    preferences.Add(pref);
                    if (pref.ArtistId.HasValue)
                    {
                        preferredArtists.Add(pref.ArtistId.Value);
                    }
                    if (pref.GenreId.HasValue)
                    {
                        preferredGenres.Add(pref.GenreId.Value);
                    }
                }
            }
            //получение лайков и дизлайков пользователя
            foreach (var grade in _dbContext.TrackRatings)
            {
                if (grade.UserId == userId)
                {
                    if (grade.Rating == 1)
                    {
                        likedTracks.Add(grade.TrackId);
                    }
                    if (grade.Rating == 0)
                    {
                        dislikedTracks.Add(grade.TrackId);
                    }
                }
            }
            //поиск похожих пользователей
            foreach (var otherPref in _dbContext.UserPreferences)
            {
                if (otherPref.UserId != userId && (preferredGenres.Contains(otherPref.GenreId ?? 0) || preferredArtists.Contains(otherPref.ArtistId ?? 0)))
                {
                    if (!similarUserIds.Contains(otherPref.UserId))
                    {
                        similarUserIds.Add(otherPref.UserId);
                    }
                }
            }
            //получаем понравившиеся треки у похожих пользователей
            foreach (var grade in _dbContext.TrackRatings)
            {
                if (similarUserIds.Contains(grade.UserId) && grade.Rating == 1)
                {
                    songsFromSimilarUsers.Add(grade.TrackId);
                }
            }
            //формируем список рекомендаций
            var recommendations = new List<Track>();

            foreach (var track in _dbContext.Tracks)
            {
                if (dislikedTracks.Contains(track.TrackId)) continue;

                if (preferredGenres.Contains(track.GenreId) || preferredArtists.Contains(track.ArtistId))
                {
                    recommendations.Add(track);
                }
            }
            //добавляем песни от похожих пользователей
            foreach (var track in _dbContext.Tracks)
            {
                if (songsFromSimilarUsers.Contains(track.TrackId) && !recommendations.Any(s => s.TrackId == track.TrackId))
                {
                    recommendations.Add(track);
                }
            }
            // Сортировка
            return recommendations.OrderByDescending(s => s.PopularityScore).ToList();
        }
    }
}
