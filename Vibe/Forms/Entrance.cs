using Vibe.Forms;
using Vibe.Core.Entities;
using System.Text;
using System.Security.Cryptography;
using Vibe.Service;
using NLog;
using Microsoft.VisualBasic.Logging;

namespace Vibe
{
    /// <summary>
    ///  форма входа
    /// </summary>
    public partial class Entrance : Form
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public Entrance()
        {
            InitializeComponent();

        }
        //метод для хэширования паролей
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in hashedBytes)
                {
                    builder.AppendFormat("{0:x2}", b);
                }
                return builder.ToString();
            }
        }
        
        private void btnLogin_Click(object sender, EventArgs e)
        {
            
            using (var _dbContext = new ApplicationDbContext())
                {
                    var login = txtLogin.Text;
                    Logger.Info($"Пользователь начал попытку входа. Логин: {login}");
                    var password = txtPassword.Text;
                    var hashedPassword = HashPassword(password);

                    var user = _dbContext.Users.FirstOrDefault(u => u.Login == login);
                    if (user == null)
                    {
                         Logger.Warn($"Пользователь '{login}' не найден.");
                         MessageBox.Show("Пользователь не найден!");
                         return;
                    }

                    if (user.PasswordHash != hashedPassword)
                    {
                          Logger.Warn($"Неверный пароль для пользователя '{login}'.");
                          MessageBox.Show("Неверный пароль!");
                          return;
                    }
                    
                     Logger.Info($"Пользователь '{login}' успешно вошёл в систему.");
                    MessageBox.Show("Вход выполнен успешно!");

                    
                    

                    OpenSongForm(user);
            }
            
            
        }
        private void OpenSongForm(User user)
        {
            
                using (var dbContext = new ApplicationDbContext())
                {
                    var recommendationService = new RecommendationService(dbContext);
                    var recommendedTracks = recommendationService.GetRecommendedSongs(user.UserId);

                Track firstTrack;
                  if (recommendedTracks != null && recommendedTracks.Count > 0)
                  {
                    firstTrack = recommendedTracks[0];
                  }
                  else
                  {
                    firstTrack = new Track
                    {
                        Title = "Нет рекомендаций",
                        Artist = new Artist { Name = "Неизвестный исполнитель" },
                        AlbumArtPath = @"C:\Users\Admin\Pictures\треки\1473685252276134903.jpg"
                    };
                  }

                    this.Hide();
                    SongForm songForm = new SongForm(firstTrack, user);
                    songForm.FormClosed += (s, args) => this.Show(); // лямбда-выражение
                    songForm.Show();
                }
            
            
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            Logger.Info("Открытие формы регистрации.");
            this.Hide();
            var registration = new Registration();
            registration.FormClosed += (s, args) => this.Show(); //тоже
            registration.Show();

        }
        
    }
}
