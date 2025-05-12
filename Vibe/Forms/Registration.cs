using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Vibe.Core.Entities;

namespace Vibe.Forms
{
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
            using (var _dbContext = new ApplicationDbContext())
            {
                LoadArtists(_dbContext);
                LoadGenres(_dbContext);
            }
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
        //Метод для загрузки исполнителей
        private void LoadArtists(ApplicationDbContext db)
        {
            checkedArtists.DataSource = db.Artists.ToList();
            checkedArtists.DisplayMember = "Name";
        }
        //Метод для загрузки жанров
        private void LoadGenres(ApplicationDbContext db)
        {
            checkedGenres.DataSource = db.Genres.ToList();
            checkedGenres.DisplayMember = "Name";
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            using (var _dbContext = new ApplicationDbContext())
            {
                string login = txtLogin.Text;
                string password = txtPassword.Text;

                var user = new User
                {
                    Login = login,
                    PasswordHash = HashPassword(password)
                };
                if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Введите логин и пароль!");
                    return;
                }
                if (checkedArtists.SelectedItems.Count == 0 || checkedGenres.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Выберите хотя бы одного исполнителя и один жанр.");
                    return;
                }
                // Добавляем пользователя в бд
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();

                // Сохраненяем предпочтения
                foreach (Artist selectArtist in checkedArtists.SelectedItems)
                {
                    _dbContext.UserPreferences.Add(new UserPreference
                    {
                        UserId = user.UserId,
                        ArtistId = selectArtist.ArtistId
                    });
                }
                foreach (Genre selectGenre in checkedGenres.SelectedItems)
                {
                    _dbContext.UserPreferences.Add(new UserPreference
                    {
                        UserId = user.UserId,
                        GenreId = selectGenre.GenreId
                    });
                }

                _dbContext.SaveChanges();

                MessageBox.Show("Регистрация прошла успешно!");
                this.Hide();
                SongForm songForm = new SongForm();
                songForm.Show();
            }
        }
    }
}
