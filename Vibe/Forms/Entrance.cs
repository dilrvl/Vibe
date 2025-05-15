using Vibe.Forms;
using Vibe.Core.Entities;
using System.Text;
using System.Security.Cryptography;
using Vibe.Service;

namespace Vibe
{
    /// <summary>
    ///  форма входа
    /// </summary>
    public partial class Entrance : Form
    {
        /// <summary>
        ///  конструктор формы
        /// </summary>
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
                    string login = txtLogin.Text;
                    string password = txtPassword.Text;
                    string hashedPassword = HashPassword(password);

                    var user = _dbContext.Users.FirstOrDefault(u => u.Login == login);
                    if (user == null)
                    {
                        MessageBox.Show("Пользователь не найден!");
                        return;
                    }

                    if (user.PasswordHash != hashedPassword)
                    {
                        MessageBox.Show("Неверный пароль!");
                        return;
                    }

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

                    Track firstTrack = recommendedTracks?.Count > 0
                        ? recommendedTracks[0]
                        : new Track
                        {
                            Title = "Нет рекомендаций",
                            Artist = new Artist { Name = "Неизвестный исполнитель" },
                            AlbumArtPath = "C:\\Users\\Admin\\Pictures\\треки\\1473685252276134903.jpg"
                        };

                    this.Hide();
                    SongForm songForm = new SongForm(firstTrack, user);
                    songForm.FormClosed += (s, args) => this.Show(); // Show Entrance form again
                    songForm.Show();
                }
            
            
        }




        private void btnRegister_Click(object sender, EventArgs e)
        {
            this.Hide();
            Registration registration = new Registration();
            registration.FormClosed += (s, args) => this.Show(); // Show Entrance form again
            registration.Show();
        }
        
    }
}
