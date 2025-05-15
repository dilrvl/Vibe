using Microsoft.EntityFrameworkCore;
using Vibe.Core.Entities;
using Vibe.Service;

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
        /// <summary>
        ///  конструктор формы
        /// </summary>
        public SongForm(Track initialTrack, User user)
        {
            InitializeComponent();
            _currentUser = user ?? throw new ArgumentNullException(nameof(user));
            _recommendationService = new RecommendationService(new ApplicationDbContext());

            // Load recommendations
            LoadRecommendations();

            // Set initial track
            if (initialTrack != null && recommendations.Count > 0)
            {
                recommendations[0] = initialTrack; // Replace first track if provided
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

            // Load album art if path exists
            if (!string.IsNullOrEmpty(currentTrack.AlbumArtPath) && System.IO.File.Exists(currentTrack.AlbumArtPath))
            {
                pctrBox.Image = System.Drawing.Image.FromFile(currentTrack.AlbumArtPath);
            }
            else
            {
                pctrBox.Image = null; // Or set a default image
            }

            btnprevious.Enabled = currentTrackIndex > 0;
            btnnext.Enabled = currentTrackIndex < recommendations.Count - 1;
        }




        // Обработчик кнопки "Нравится"
        private void btnLike_Click(object sender, EventArgs e)
        {
            try
            {
                var currentTrack = recommendations[currentTrackIndex];
                
                var isFavorited = _recommendationService.GetFavoriteTracks(_currentUser.UserId)
                    .Any(t => t.TrackId == currentTrack.TrackId);

                if (isFavorited)
                {
                    _recommendationService.RemoveFromFavorites(_currentUser.UserId, currentTrack.TrackId);
                    MessageBox.Show("Трек удалён из избранного!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _recommendationService.AddToFavorites(_currentUser.UserId, currentTrack.TrackId);
                    MessageBox.Show("Трек добавлен в избранное!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                UpdateFavoriteButton();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Обработчик кнопки "Не нравится"
        private void btnDislike_Click(object sender, EventArgs e)
        {
           
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
        private void UpdateFavoriteButton()
        {
            var currentTrack = recommendations[currentTrackIndex];
            var isFavorited = _recommendationService.GetFavoriteTracks(_currentUser.UserId)
                .Any(t => t.TrackId == currentTrack.TrackId);

            btnfavoriteSong.Text = isFavorited ? "Удалить из избранного" : "Добавить в избранное";
        }
    }
}
