using Microsoft.EntityFrameworkCore;
using Vibe.Core.Entities;
using Vibe.Service;
using NLog;

namespace Vibe.Forms
{
    /// <summary>
    ///  форма с песнями 
    /// </summary>
    public partial class SongForm : Form
    {
        private readonly User _currentUser;
        private readonly RecommendationService _recommendationService;
        private List<Track> recommendations;
        private int currentTrackIndex = 0;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public SongForm(Track initialTrack, User user)
        {
            InitializeComponent();
            _currentUser = user;
            _recommendationService = new RecommendationService(new ApplicationDbContext());
            LoadRecommendations();

            if (initialTrack != null && recommendations.Count > 0)
            {
                recommendations[0] = initialTrack; 
                currentTrackIndex = 0;
            }
            else if (recommendations.Count == 0)
            {
                recommendations.Add(initialTrack ?? new Track
                {
                    Title = "Нет рекомендаций",
                    Artist = new Artist { Name = "Неизвестный исполнитель" },
                    AlbumArtPath = "C:\\Users\\Admin\\Pictures\\треки\\1473685252276134903.jpg"
                });
                currentTrackIndex = 0;
            }

            UpdateDisplay();
        }
        
        // Загрузка рекомендаций
        private void LoadRecommendations()
        {
            try
            {
                recommendations = _recommendationService.GetRecommendedSongs(_currentUser.UserId);
                if (recommendations == null || recommendations.Count == 0)
                {
                    recommendations = new List<Track>
                    {
                        new Track
                        {
                            Title = "Нет доступных рекомендаций",
                            Artist = new Artist { Name = "Неизвестный исполнитель" },
                            AlbumArtPath = "C:\\Users\\Admin\\Pictures\\треки\\1473685252276134903.jpg"
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке рекомендаций: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                recommendations = new List<Track>
                {
                    new Track
                    {
                        Title = "Ошибка загрузки",
                        Artist = new Artist { Name = "Неизвестный исполнитель" },
                        AlbumArtPath = "C:\\Users\\Admin\\Pictures\\треки\\1473685252276134903.jpg"
                    }
                };
            }
        }
        private void UpdateDisplay()
        {
            Logger.Debug("Обновление отображения трека");
            if (recommendations == null || recommendations.Count == 0 || currentTrackIndex < 0 || currentTrackIndex >= recommendations.Count)
            {
                lblname.Text = "Ошибка отображения";
                lblartist.Text = "Неизвестный исполнитель";
                pctrBox.Image = null;
                return;
            }

            var currentTrack = recommendations[currentTrackIndex];
            lblname.Text = currentTrack.Title ?? "Без названия";
            lblartist.Text = currentTrack.Artist?.Name ?? "Неизвестный исполнитель";

            if (!string.IsNullOrEmpty(currentTrack.AlbumArtPath) && System.IO.File.Exists(currentTrack.AlbumArtPath))
            {
                pctrBox.Image = System.Drawing.Image.FromFile(currentTrack.AlbumArtPath);
            }
            else
            {
                pctrBox.Image = null; 
            }

            btnprevious.Enabled = currentTrackIndex > 0;
            btnnext.Enabled = currentTrackIndex < recommendations.Count - 1;
        }

        // Обработчик кнопки "Нравится"
        private void btnLike_Click(object sender, EventArgs e)
        {       
            var currentTrack = recommendations[currentTrackIndex];
                
            var isFavorited = _recommendationService.GetFavoriteTracks(_currentUser.UserId)
                    .Any(t => t.TrackId == currentTrack.TrackId);
       
            _recommendationService.AddToFavorites(_currentUser.UserId, currentTrack.TrackId);
            MessageBox.Show("Трек добавлен в избранное!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Logger.Info($"Трек '{currentTrack.Title}' добавлен в избранное");


        }


        // Обработчик кнопки "Не нравится"
        private void btnDislike_Click(object sender, EventArgs e)
        {  
            if (recommendations == null || recommendations.Count == 0 || currentTrackIndex < 0 || currentTrackIndex >= recommendations.Count)
            {
                MessageBox.Show("Нет доступного трека для дизлайка.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var currentTrack = recommendations[currentTrackIndex];

            // Убедимся, что такой трек ещё не добавлен как дизлайк
            var existingRating = _recommendationService.GetFavoriteTracks(_currentUser.UserId)
                .FirstOrDefault(t => t.TrackId == currentTrack.TrackId);

            // Если он был в избранном — удалим его
            if (existingRating != null)
            {
                _recommendationService.RemoveFromFavorites(_currentUser.UserId, currentTrack.TrackId);
            }

            // Добавляем отрицательный рейтинг (или просто помечаем, что пользователь не любит этот трек)
            try
            {
                // Предположим, что Rating = -1 — это дизлайк
                using (var context = new ApplicationDbContext())
                {
                    var existingRatingEntry = context.TrackRatings
                        .FirstOrDefault(r => r.UserId == _currentUser.UserId && r.TrackId == currentTrack.TrackId);

                    if (existingRatingEntry != null)
                    {
                        existingRatingEntry.Rating = -1; // Обновляем существующую запись
                        context.SaveChanges();
                    }
                    else
                    {
                        context.TrackRatings.Add(new TrackRating
                        {
                            UserId = _currentUser.UserId,
                            TrackId = currentTrack.TrackId,
                            Rating = -1
                        });
                        context.SaveChanges();
                    }
                }

                Logger.Info($"Трек '{currentTrack.Title}' помечен как 'Не нравится'");
                MessageBox.Show("Трек пропущен!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Перезагружаем рекомендации без этого трека
                LoadRecommendations();

                // Если после перезагрузки есть рекомендации — начинаем с первой
                if (recommendations.Count > 0)
                {
                    currentTrackIndex = 0;
                    UpdateDisplay();
                }
                else
                {
                    MessageBox.Show("Больше нет рекомендаций.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении дизлайка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Error(ex, "Ошибка при сохранении дизлайка");
            }
        }
        

        private void btnnext_Click(object sender, EventArgs e)
        {
            if (currentTrackIndex < recommendations.Count - 1)
            {
                currentTrackIndex++;
                UpdateDisplay();
            }
        }

        // Обработчик кнопки "Избранное"
        private void btnfavoriteSong_Click(object sender, EventArgs e)
        {
            var favoriteSongs = new FavoriteSong(_currentUser);
            favoriteSongs.ShowDialog();
        }

        private void btnprevious_Click(object sender, EventArgs e)
        {
            if (currentTrackIndex > 0)
            {
                currentTrackIndex--;
                UpdateDisplay();
            }
        }
        
    }
}
