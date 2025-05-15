using System.Security.Cryptography;
using System.Text;
using Vibe.Core.Entities;

namespace Vibe.Forms
{
    /// <summary>
    ///  форма регистрации
    /// </summary>
    public partial class Registration : Form
    {
        private readonly ApplicationDbContext _dbContext;
        /// <summary>
        ///  конструктор формы
        /// </summary>
        public Registration()
        {
            InitializeComponent();
            _dbContext = new ApplicationDbContext();
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

        private void button2_Click(object sender, EventArgs e)
        {
            using (var _dbContext = new ApplicationDbContext())
            {
                string login = txtLogin.Text;
                string password = txtPassword.Text;
                string confirmPassword = txtPassword2.Text;
                if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
                {
                    MessageBox.Show("Введите логин и пароль!");
                    return;
                }
                // Проверка совпадения паролей
                if (password != confirmPassword)
                {
                    MessageBox.Show("Пароли не совпадают!");
                    return;
                }
                var user = new User
                {
                    Login = login,
                    PasswordHash = HashPassword(password)
                };

                // Добавляем пользователя в базу данных
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
                this.Hide();
                Preference preference = new Preference(user);
                preference.FormClosed += (s, args) => this.Close();
                preference.Show();
            }

        }
        //Метод для скрытия пароля и наоборот 
        private void PasswordVisibility(TextBox textBox, Button button)
        {
            if (txtPassword.PasswordChar == '*' || txtPassword2.PasswordChar == '*')
            {
                textBox.PasswordChar = '\0'; // Показать пароль
            }
            else
            {
                textBox.PasswordChar = '*'; // Скрыть пароль
            }
        }
        private void btn1_Click(object sender, EventArgs e)
        {
            PasswordVisibility(txtPassword, btn1);
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            PasswordVisibility(txtPassword2, btn2);
        }
    }
}
