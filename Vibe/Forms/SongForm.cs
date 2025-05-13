using Microsoft.EntityFrameworkCore;
using System;
using System.Windows.Forms;
using Vibe.Core.Entities;
using Vibe.Service;

namespace Vibe.Forms
{
    public partial class SongForm : Form
    {
        private Track currentTrack; // Текущий трек
        private User CurrentUser;
        private readonly ApplicationDbContext _dbContext;
        private readonly RecommendationService _recommendationService; // Сервис рекомендаций                                                                    
        public SongForm(Track track)
        {
            currentTrack = track;
            UpdateTrackInfo(currentTrack);
            InitializeComponent();
            _dbContext = new ApplicationDbContext();
            _recommendationService = new RecommendationService(_dbContext);
        }
        //обновление проинрывания трека
        private void UpdateTrackInfo(Track track)
        {
            currentTrack = track;
            pctrBox.ImageLocation = track.AlbumArtPath;
            lblname.Text = track.Title;
            lblartist.Text = track.Artist?.Name ?? "Неизвестный исполнитель";
        }

        private void btnLike_Click(object sender, EventArgs e)
        {
            using (var db = new ApplicationDbContext())
            {
                var rating = new TrackRating
                {
                    UserId = CurrentUser.UserId,
                    TrackId = currentTrack.TrackId,
                    Rating = 1 // 1 - нравится
                };
                db.TrackRatings.Add(rating);
                // увеличиваем популярность трека
                var trackToUpdate = db.Tracks.Find(currentTrack.TrackId);
                if (trackToUpdate != null)
                {
                    trackToUpdate.PopularityScore += 5;
                    
                }
                db.SaveChanges();

            }
            MessageBox.Show("Трек добавлен в понравившиеся!");
        }
        private void LoadNextTrack()
        {
            int userId = CurrentUser.UserId;

            // получаем рекомендации
            var recommendedTracks = _recommendationService.GetRecommendedSongs(userId);

            recommendedTracks = recommendedTracks.FindAll(t => t.TrackId != currentTrack.TrackId);
            // Берём первый подходящий трек
            if (recommendedTracks.Count > 0)
            {
                var nextTrack = recommendedTracks[0];
                UpdateTrackInfo(nextTrack);
            }
            else
            {
                MessageBox.Show("Больше нет рекомендаций.");
            }
        }

        private void btnDislike_Click(object sender, EventArgs e)
        {
            using (var db = new ApplicationDbContext())
            {
                var rating = new TrackRating
                {
                    UserId = CurrentUser.UserId,
                    TrackId = currentTrack.TrackId,
                    Rating = 0 // 0 - не нравится
                };
                db.TrackRatings.Add(rating);
                // Уменьшаем популярность трека
                var trackToUpdate = db.Tracks.Find(currentTrack.TrackId);
                if (trackToUpdate != null && trackToUpdate.PopularityScore > 0)
                {
                    trackToUpdate.PopularityScore -= 2;
                }
                db.SaveChanges();
            }
            LoadNextTrack();


        }

        private void btnnext_Click(object sender, EventArgs e)
        {
            LoadNextTrack();
        }

        private void btnfavoriteSong_Click(object sender, EventArgs e)
        {

            this.Hide();
            FavoriteSong favoriteSong = new FavoriteSong();
            favoriteSong.Show();
        }
    }
}
