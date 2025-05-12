using Microsoft.EntityFrameworkCore;
using Vibe.Forms;
using Vibe.Core.Entities;
using System;
using System.Windows.Forms;
using System.Text;
using System.Security.Cryptography;

namespace Vibe
{
    public partial class Entrance : Form
    {
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
                /*Добавление экземпляров
                _dbContext.Genres.Add(new Genre { Name = "Электронная музыка" });
                _dbContext.SaveChanges();
                */

                // Получение логина и пароля ( нужно будет для хранения и проверки в бд!!)
                string login = txtLogin.Text;
                string password = txtPassword.Text;
                // хэшируем введённый пароль и сравниваем с сохранённым
                string hashedPassword = HashPassword(password);

                var user = _dbContext.Users.FirstOrDefault(u => u.Login == login);
                if (user == null)
                {
                    MessageBox.Show("Пользователь не найден!");
                    return;
                }
                // Проверка пароля
                if (user.PasswordHash != hashedPassword)
                {

                    MessageBox.Show("Неверный пароль!");
                    return;
                }
                // Успешный вход
                MessageBox.Show("Вход выполнен успешно!");
                this.Close();
                SongForm songForm = new SongForm();
                songForm.Show();
            }
        }
        
        private void btnRegister_Click(object sender, EventArgs e)
        {
            this.Hide();
            Registration registration = new Registration();
            registration.Show();
        }
    }
}
