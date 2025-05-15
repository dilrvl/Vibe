using Vibe.Service;
using Vibe.Core.Entities;


namespace Vibe.Forms
{
    /// <summary>
    ///  класс избранных(который почему-то не работает...)
    /// </summary>
    public partial class FavoriteSong : Form
    {

        private readonly User _currentUser;
        private readonly RecommendationService _recommendationService;

        private readonly FlowLayoutPanel flowLayoutPanelTracks; // Nested FlowLayoutPanel


        /// <summary>
        ///  конструктор формы
        /// </summary>
        public FavoriteSong(User user)
        {
            InitializeComponent();

            _currentUser = user ?? throw new ArgumentNullException(nameof(user));
            _recommendationService = new RecommendationService(new ApplicationDbContext());

            // Initialize nested FlowLayoutPanel inside panelTracks
            flowLayoutPanelTracks = new FlowLayoutPanel
            {
                Location = new Point(0, 0),
                Size = panelTracks.Size,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                BackColor = SystemColors.ActiveCaptionText // Match panelTracks background
            };
            panelTracks.Controls.Add(flowLayoutPanelTracks);
            // Загружаем избранные треки
            LoadFavorites();
        }
        private void LoadFavorites()
        {
            try
            {
                
                var favoriteTracks = _recommendationService.GetFavoriteTracks(_currentUser.UserId);
               
                flowLayoutPanelTracks.Controls.Clear();

                
                
                    foreach (var track in favoriteTracks)
                    {
                        // Create a Panel for each track
                        var trackPanel = new Panel
                        {
                            Size = new Size(300, 60),
                            BackColor = Color.FromArgb(50, 50, 50), // Dark background for contrast
                            Margin = new Padding(5)
                        };

                        // Add track title label
                        var titleLabel = new Label
                        {
                            Text = track.Title ?? "Без названия",
                            AutoSize = true,
                            Location = new Point(10, 10),
                            ForeColor = Color.White,
                            Font = new Font("Arial", 10F, FontStyle.Bold)
                        };
                        trackPanel.Controls.Add(titleLabel);

                        // Add artist label
                        var artistLabel = new Label
                        {
                            Text = track.Artist?.Name ?? "Неизвестный исполнитель",
                            AutoSize = true,
                            Location = new Point(10, 35),
                            ForeColor = Color.Silver,
                            Font = new Font("Arial", 8F)
                        };
                        trackPanel.Controls.Add(artistLabel);
                        // Attach click event
                        trackPanel.Click += (sender, e) => TrackPanel_Click(sender, e, track);

                        // Add the track panel to the FlowLayoutPanel
                        flowLayoutPanelTracks.Controls.Add(trackPanel);
                    }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке избранных треков: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void TrackPanel_Click(object sender, EventArgs e, Track track)
        {
            if (sender is Panel panel)
            {
                var result = MessageBox.Show(
                    $"Удалить трек '{track.Title}' из избранного?",
                    "Подтверждение удаления",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        _recommendationService.RemoveFromFavorites(_currentUser.UserId, track.TrackId);
                        MessageBox.Show("Трек успешно удалён из избранного!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadFavorites(); // Refresh the list
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении трека: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }




        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
