using Vibe.Core.Entities;
using Vibe.Service;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

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

        // Словарь для хранения соответствия между PictureBox и ID жанра
        private Dictionary<PictureBox, int> genrePictureBoxes = new Dictionary<PictureBox, int>();

        // Цвет для выделенных жанров
        private Color selectedColor = Color.FromArgb(255, 100, 200); // Более яркий розовый для выделения
        private Color defaultColor = Color.Violet; // Обычный цвет плиток

        // Соответствие между PictureBox и ID жанров из базы данных
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

        /// <summary>
        ///  конструктор формы
        /// </summary>
        public Preference(User user)
        {
            InitializeComponent();
            _dbContext = new ApplicationDbContext();

           

            _currentUser = user;
            _recommendationService = new RecommendationService(_dbContext);

            // Инициализируем словарь для PictureBox
            genrePictureBoxes = new Dictionary<PictureBox, int>();

            // Инициализируем обработчики событий для PictureBox
            InitializeGenrePictureBoxes();

            // Загружаем данные
            MapGenresToPictureBoxes();
            LoadArtists();

            // Загружаем существующие предпочтения пользователя
            LoadUserPreferences();

            // Добавляем обработчик для кнопки регистрации
            buttonRegistration.Click += btnSave_Click;
        }

        // Инициализация PictureBox для жанров
        // Инициализация PictureBox для жанров
        private void InitializeGenrePictureBoxes()
        {
            // Связываем каждый PictureBox с обработчиком события Click
            pictureBoxPop.Click += Genre_Click;
            pictureBoxInRap.Click += Genre_Click;
            pictureBoxIndi.Click += Genre_Click;
            pictureBoxRapHipHop.Click += Genre_Click;
            pictureBoxWorld.Click += Genre_Click;
            pictureBoxRock.Click += Genre_Click;
            pictureBoxElectronic.Click += Genre_Click;
            pictureBoxMetal.Click += Genre_Click;
            pictureBoxAlternativ.Click += Genre_Click;

            // Добавляем обработчики для эффекта при наведении
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

            // Устанавливаем курсор для всех PictureBox
            foreach (Control control in panelGenres.Controls)
            {
                if (control is PictureBox)
                {
                    control.Cursor = Cursors.Hand;
                }
            }
        }
        // Связываем PictureBox с ID жанров из базы данных
        private void MapGenresToPictureBoxes()
        {
            try
            {
                // Связываем PictureBox с ID жанров из словаря
                genrePictureBoxes[pictureBoxPop] = genreIdMapping["Pop"];
                genrePictureBoxes[pictureBoxInRap] = genreIdMapping["InRap"];
                genrePictureBoxes[pictureBoxIndi] = genreIdMapping["Indi"];
                genrePictureBoxes[pictureBoxRapHipHop] = genreIdMapping["RapHipHop"];
                genrePictureBoxes[pictureBoxWorld] = genreIdMapping["World"];
                genrePictureBoxes[pictureBoxRock] = genreIdMapping["Rock"];
                genrePictureBoxes[pictureBoxElectronic] = genreIdMapping["Electronic"];
                genrePictureBoxes[pictureBoxMetal] = genreIdMapping["Metal"];
                genrePictureBoxes[pictureBoxAlternativ] = genreIdMapping["Alternativ"];

                // Устанавливаем Tag для каждого PictureBox, чтобы хранить ID жанра
                foreach (var pair in genrePictureBoxes)
                {
                    pair.Key.Tag = pair.Value;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при связывании жанров: {ex.Message}");
            }
        }
        // Эффект при наведении на PictureBox
        private void PictureBox_MouseEnter(object sender, EventArgs e)
        {
            if (sender is PictureBox box && box.Tag != null)
            {
                int genreId = (int)box.Tag;
                if (!selectedGenres.Contains(genreId))
                {
                    box.BackColor = Color.FromArgb(245, 180, 235); // Светлее при наведении
                }
            }
        }
        // Возврат к обычному цвету при уходе курсора
        private void PictureBox_MouseLeave(object sender, EventArgs e)
        {
            if (sender is PictureBox box && box.Tag != null)
            {
                int genreId = (int)box.Tag;
                if (!selectedGenres.Contains(genreId))
                {
                    box.BackColor = defaultColor; // Возвращаем обычный цвет
                }
            }
        }

        // Загрузка исполнителей
        // Загрузка исполнителей
        private void LoadArtists()
        {
            try
            {
                var artists = _dbContext.Artists.ToList();

                // Очищаем список и словарь
                checkedListBoxArtist.Items.Clear();
                artistIndexToId.Clear();

                // Добавляем исполнителей в список
                for (int i = 0; i < artists.Count; i++)
                {
                    var artist = artists[i];
                    checkedListBoxArtist.Items.Add(artist.Name);
                    artistIndexToId[i] = artist.ArtistId;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке исполнителей: {ex.Message}");
            }
        }


        // Обработка клика по жанру
        private void Genre_Click(object sender, EventArgs e)
        {
            if (sender is PictureBox clickedBox && clickedBox.Tag != null)
            {
                int genreId = (int)clickedBox.Tag;

                if (selectedGenres.Contains(genreId))
                {
                    // Если жанр уже выбран, убираем выделение
                    clickedBox.BackColor = defaultColor;
                    clickedBox.BorderStyle = BorderStyle.None;
                    selectedGenres.Remove(genreId);
                }
                else
                {
                    // Если жанр не выбран, добавляем выделение
                    clickedBox.BackColor = selectedColor;
                    clickedBox.BorderStyle = BorderStyle.FixedSingle; // Добавляем рамку
                    selectedGenres.Add(genreId);
                }
            }
        }
        // Добавляем метод для загрузки существующих предпочтений пользователя
        private void LoadUserPreferences()
        {
            try
            {
                // Получаем предпочтения пользователя из базы данных
                var userPreferences = _dbContext.UserPreferences
                    .Where(up => up.UserId == _currentUser.UserId)
                    .ToList();

                // Выделяем жанры, которые уже выбраны пользователем
                foreach (var preference in userPreferences)
                {
                    if (preference.GenreId.HasValue)
                    {
                        // Находим PictureBox для этого жанра
                        var pictureBox = genrePictureBoxes.FirstOrDefault(p => p.Value == preference.GenreId.Value).Key;
                        if (pictureBox != null)
                        {
                            // Выделяем жанр
                            pictureBox.BorderStyle = BorderStyle.FixedSingle;
                            pictureBox.BackColor = selectedColor;
                            selectedGenres.Add(preference.GenreId.Value);
                        }
                    }

                    if (preference.ArtistId.HasValue)
                    {
                        // Находим индекс исполнителя в списке
                        for (int i = 0; i < checkedListBoxArtist.Items.Count; i++)
                        {
                            if (artistIndexToId.TryGetValue(i, out int artistId) && artistId == preference.ArtistId.Value)
                            {
                                checkedListBoxArtist.SetItemChecked(i, true);
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке предпочтений: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            try
            {
                // Проверяем, выбран ли хотя бы один жанр
                if (selectedGenres.Count == 0)
                {
                    MessageBox.Show("Пожалуйста, выберите хотя бы один жанр", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Проверяем существование пользователя
                var user = _dbContext.Users.Find(_currentUser.UserId);
                if (user == null)
                {
                    MessageBox.Show("Ошибка: Пользователь не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Удаляем существующие предпочтения пользователя
                var existingPreferences = _dbContext.UserPreferences.Where(up => up.UserId == _currentUser.UserId).ToList();
                foreach (var pref in existingPreferences)
                {
                    _dbContext.UserPreferences.Remove(pref);
                }

                // Сохранение выбранных жанров
                foreach (int genreId in selectedGenres)
                {
                    // Проверяем существование жанра
                    var genre = _dbContext.Genres.Find(genreId);
                    if (genre == null)
                    {
                        MessageBox.Show($"Ошибка: Жанр с ID {genreId} не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        continue;
                    }

                    _dbContext.UserPreferences.Add(new UserPreference
                    {
                        UserId = _currentUser.UserId,
                        GenreId = genreId
                    });
                }

                // Сохранение выбранных исполнителей
                foreach (int index in checkedListBoxArtist.CheckedIndices)
                {
                    // Получаем ID исполнителя из словаря
                    if (artistIndexToId.TryGetValue(index, out int artistId))
                    {
                        // Проверяем существование исполнителя
                        var artist = _dbContext.Artists.Find(artistId);
                        if (artist == null)
                        {
                            MessageBox.Show($"Ошибка: Исполнитель с ID {artistId} не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            continue;
                        }

                        _dbContext.UserPreferences.Add(new UserPreference
                        {
                            UserId = _currentUser.UserId,
                            ArtistId = artistId
                        });
                    }
                }

                _dbContext.SaveChanges();
                MessageBox.Show("Предпочтения успешно сохранены!");

                // Получаем первый рекомендуемый трек
                Track firstTrack = GetFirstTrackForUser(_currentUser.UserId);

                // Открываем форму SongForm с передачей трека и пользователя
                SongForm songForm = new SongForm(firstTrack, user);
                this.Hide();
                songForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении предпочтений: {ex.Message}\n\nСтек вызовов: {ex.StackTrace}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
