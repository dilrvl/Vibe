using Vibe.Service;
using Vibe.Core.Entities;
using NLog;


namespace Vibe.Forms
{
    /// <summary>
    ///  класс избранных(который почему-то не работает...)
    /// </summary>
    public partial class FavoriteSong : Form
    {

        private readonly User _currentUser;
        private readonly RecommendationService _recommendationService;

        private readonly FlowLayoutPanel flowLayoutPanelTracks;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public FavoriteSong(User user)
        {
            InitializeComponent();

            _currentUser = user;
            _recommendationService = new RecommendationService(new ApplicationDbContext());

            flowLayoutPanelTracks = new FlowLayoutPanel
            {
                Location = new Point(0, 0),
                Size = panelTracks.Size,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                BackColor = SystemColors.ActiveCaptionText 
            };
            panelTracks.Controls.Add(flowLayoutPanelTracks);
            // Загружаем избранные треки
            LoadFavorites();
        }
        private void LoadFavorites()
        {            
            var favoriteTracks = _recommendationService.GetFavoriteTracks(_currentUser.UserId);            
            flowLayoutPanelTracks.Controls.Clear();           
            foreach (var track in favoriteTracks)
            {
               var trackPanel = new Panel
               {
                   Size = new Size(300, 60),
                   BackColor = Color.FromArgb(50, 50, 50), 
                   Margin = new Padding(5)
               };
                var titleLabel = new Label
                {
                    Text = track.Title ?? "Без названия",
                    AutoSize = true,
                    Location = new Point(10, 10),
                    ForeColor = Color.White,
                    Font = new Font("Arial", 10F, FontStyle.Bold)
                };
                trackPanel.Controls.Add(titleLabel);
                var artistLabel = new Label
                {
                    Text = track.Artist?.Name ?? "Неизвестный исполнитель",
                    AutoSize = true,
                    Location = new Point(10, 35),
                    ForeColor = Color.Silver,
                    Font = new Font("Arial", 8F)
                };
                trackPanel.Controls.Add(artistLabel);
                trackPanel.Click += (sender, e) => TrackPanel_Click(sender, e, track);                        
                flowLayoutPanelTracks.Controls.Add(trackPanel);
                Logger.Info($"Загружено {favoriteTracks.Count} избранных треков");
            }
                          
        }
        private void TrackPanel_Click(object sender, EventArgs e, Track track)
        {
            if (sender is Panel panel)
            {
                var result = MessageBox.Show(
                    $"Удалить '{track.Title}'",
                    "Подтверждение удаления",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                Logger.Info($"Трек '{track.Title}' удален из избранного");


            }
        }
        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
