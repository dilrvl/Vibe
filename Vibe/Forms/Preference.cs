using Vibe.Core.Entities;
using Vibe.Service;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using NLog;

namespace Vibe.Forms
{
    /// <summary>
    ///  форма с предпочтениями пользователя
    /// </summary>
    public partial class Preference : Form
    {
        private readonly ApplicationDbContext _dbContext;
        private User _currentUser;
        private List<int> selectedGenres = new List<int>();
        private Dictionary<int, int> artistIndexToId = new Dictionary<int, int>();
        private readonly RecommendationService _recommendationService;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        //для хранения соответствия между картинкой и айди жанра
        private Dictionary<PictureBox, int> genrePictureBoxes = new Dictionary<PictureBox, int>();
        
        private Color selectedColor = Color.FromArgb(255, 100, 200); 
        private Color defaultColor = Color.Violet; // Обычный цвет плиток

        // Соответствие между картинкой и айди жанров из базы данных
        private Dictionary<string, int> genreIdMapping = new Dictionary<string, int>()
        {
            { "Pop", 2 },           // Поп
            { "InRap", 7 },         // Иностранный рэп
            { "Indi", 8 },          // Инди
            { "RapHipHop", 9 },     // Рэп и хип-хоп
            { "World", 10 },        // Музыка мира
            { "Rock", 1 },          // Рок
            { "Electronic", 11 },   // Электроника
            { "Metal", 12 },        // Метал
            { "Alternativ", 13 }    // Альтернатива
        };


        public Preference(User user)
        {
            InitializeComponent();
            _dbContext = new ApplicationDbContext();
            _currentUser = user;
            _recommendationService = new RecommendationService(_dbContext);
            
            genrePictureBoxes = new Dictionary<PictureBox, int>();
            
            InitializeGenrePictureBoxes();

            // Загружаем данные
            MapGenresToPictureBoxes();
            LoadArtists();
            buttonRegistration.Click += btnSave_Click;
        }    
        private void InitializeGenrePictureBoxes()
        {
            // связывает с обработчиком события Click
            pictureBoxPop.Click += Genre_Click;
            pictureBoxInRap.Click += Genre_Click;
            pictureBoxIndi.Click += Genre_Click;
            pictureBoxRapHipHop.Click += Genre_Click;
            pictureBoxWorld.Click += Genre_Click;
            pictureBoxRock.Click += Genre_Click;
            pictureBoxElectronic.Click += Genre_Click;
            pictureBoxMetal.Click += Genre_Click;
            pictureBoxAlternativ.Click += Genre_Click;

            //обработчики для эффекта при наведении
            pictureBoxPop.MouseEnter += PictureBox_MouseEnter;
            pictureBoxInRap.MouseEnter += PictureBox_MouseEnter;
            pictureBoxIndi.MouseEnter += PictureBox_MouseEnter;
            pictureBoxRapHipHop.MouseEnter += PictureBox_MouseEnter;
            pictureBoxWorld.MouseEnter += PictureBox_MouseEnter;
            pictureBoxRock.MouseEnter += PictureBox_MouseEnter;
            pictureBoxElectronic.MouseEnter += PictureBox_MouseEnter;
            pictureBoxMetal.MouseEnter += PictureBox_MouseEnter;
            pictureBoxAlternativ.MouseEnter += PictureBox_MouseEnter;

            pictureBoxPop.MouseLeave += PictureBox_MouseLeave;
            pictureBoxInRap.MouseLeave += PictureBox_MouseLeave;
            pictureBoxIndi.MouseLeave += PictureBox_MouseLeave;
            pictureBoxRapHipHop.MouseLeave += PictureBox_MouseLeave;
            pictureBoxWorld.MouseLeave += PictureBox_MouseLeave;
            pictureBoxRock.MouseLeave += PictureBox_MouseLeave;
            pictureBoxElectronic.MouseLeave += PictureBox_MouseLeave;
            pictureBoxMetal.MouseLeave += PictureBox_MouseLeave;
            pictureBoxAlternativ.MouseLeave += PictureBox_MouseLeave;
       
        }
        private void MapGenresToPictureBoxes()
        {
                // Связываем пиктрбоксы с айди жанров из словаря
                genrePictureBoxes[pictureBoxPop] = genreIdMapping["Pop"];
                genrePictureBoxes[pictureBoxInRap] = genreIdMapping["InRap"];
                genrePictureBoxes[pictureBoxIndi] = genreIdMapping["Indi"];
                genrePictureBoxes[pictureBoxRapHipHop] = genreIdMapping["RapHipHop"];
                genrePictureBoxes[pictureBoxWorld] = genreIdMapping["World"];
                genrePictureBoxes[pictureBoxRock] = genreIdMapping["Rock"];
                genrePictureBoxes[pictureBoxElectronic] = genreIdMapping["Electronic"];
                genrePictureBoxes[pictureBoxMetal] = genreIdMapping["Metal"];
                genrePictureBoxes[pictureBoxAlternativ] = genreIdMapping["Alternativ"];

                // тут устанаваливается 
                foreach (var pair in genrePictureBoxes)
                {
                    pair.Key.Tag = pair.Value;
                }           
        }
        // Эффект при наведении 
        private void PictureBox_MouseEnter(object sender, EventArgs e)
        {
            if (sender is PictureBox box && box.Tag != null)
            {
                var genreId = (int)box.Tag;
                if (!selectedGenres.Contains(genreId))
                {
                    box.BackColor = Color.FromArgb(245, 180, 235); 
                }
            }
        }
        // Возврат к обычному цвету при уходе курсора
        private void PictureBox_MouseLeave(object sender, EventArgs e)
        {
            if (sender is PictureBox box && box.Tag != null)
            {
                var genreId = (int)box.Tag;
                if (!selectedGenres.Contains(genreId))
                {
                    box.BackColor = defaultColor; // Возвращаем обычный цвет
                }
            }
        }

        private void LoadArtists()
        {
            
                var artists = _dbContext.Artists.ToList();

                
                checkedListBoxArtist.Items.Clear();
                artistIndexToId.Clear();

                for (int i = 0; i < artists.Count; i++)
                {
                    var artist = artists[i];
                    checkedListBoxArtist.Items.Add(artist.Name);
                    artistIndexToId[i] = artist.ArtistId;
                    Logger.Trace($"Добавлен исполнитель '{artist.Name}' с ID {artist.ArtistId}");
                }
           
        }


        // Обработка клика по жанру
        private void Genre_Click(object sender, EventArgs e)
        {
            if (sender is PictureBox clickedBox && clickedBox.Tag != null)
            {
                var genreId = (int)clickedBox.Tag;

                if (selectedGenres.Contains(genreId))
                {
                    // убираем выделение
                    clickedBox.BackColor = defaultColor;
                    clickedBox.BorderStyle = BorderStyle.None;
                    selectedGenres.Remove(genreId);
                    Logger.Info($"Жанр {genreId} удалён из выбора");
                }
                else
                {
                    //  добавляем выделение
                    clickedBox.BackColor = selectedColor;
                    clickedBox.BorderStyle = BorderStyle.FixedSingle; // Добавляем рамку
                    selectedGenres.Add(genreId);
                    Logger.Info($"Жанр  {genreId} добавлен в выбор");
                }
            }
        }
        // Получение первого трека для пользователя
        private Track GetFirstTrackForUser(int userId)
        {
            try
            {
                var recommendedTracks = _recommendationService.GetRecommendedSongs(userId);

                if (recommendedTracks != null && recommendedTracks.Count > 0)
                {
                    Logger.Info($"Получен рекомендованный трек: {recommendedTracks[0].Title}");
                    return recommendedTracks[0];
                }
                else
                {
                    // Если рекомендаций нет, возвращаем трек по умолчанию
                    return new Track
                    {
                        Title = "Нет рекомендаций",
                        Artist = new Artist { Name = "Неизвестный исполнитель" },
                        AlbumArtPath = "C:\\Users\\Admin\\Pictures\\треки\\1473685252276134903.jpg"
                    };
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении рекомендаций: {ex.Message}");

                // В случае ошибки возвращаем трек по умолчанию
                return new Track
                {
                    Title = "Ошибка рекомендаций",
                    Artist = new Artist { Name = "Неизвестный исполнитель" },
                    AlbumArtPath = "C:\\Users\\Admin\\Pictures\\треки\\1473685252276134903.jpg"
                };
            }
        }


        // Сохранение предпочтений пользователя
        private void btnSave_Click(object sender, EventArgs e)
        {
            Logger.Info("[btnSave_Click] Начало сохранения предпочтений...");

                if (selectedGenres.Count == 0)
                {
                    MessageBox.Show("Пожалуйста, выберите хотя бы один жанр");
                    return;
                }
  
                var user = _dbContext.Users.Find(_currentUser.UserId);
                if (user == null)
                {
                    MessageBox.Show("Ошибка: Пользователь не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Сохраненяем выбранные жанры
                foreach (var genreId in selectedGenres)
                {
                    _dbContext.UserPreferences.Add(new UserPreference
                    {
                        UserId = _currentUser.UserId,
                        GenreId = genreId
                    });
                }
                // сохраненяем выбранных исполнителей
                foreach (int index in checkedListBoxArtist.CheckedIndices)
                {
                    
                    if (artistIndexToId.TryGetValue(index, out int artistId))
                    {
                        _dbContext.UserPreferences.Add(new UserPreference
                        {
                            UserId = _currentUser.UserId,
                            ArtistId = artistId
                        });
                        Logger.Debug($"[btnSave_Click] Добавлен исполнитель  {artistId}");
                      }
                }

                _dbContext.SaveChanges();
                MessageBox.Show("Предпочтения успешно сохранены!");
                Logger.Info("[btnSave_Click] Предпочтения успешно сохранены");
                Track firstTrack = GetFirstTrackForUser(_currentUser.UserId);

                SongForm songForm = new SongForm(firstTrack, user);
                this.Hide();
                songForm.Show();
            
           
        }
    }
}
