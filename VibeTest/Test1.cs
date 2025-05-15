using Vibe.Core.Entities;
namespace VibeTest
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void ArtistId_Should_Be_Set_And_Get()
        {
            var artist = new Artist();
            artist.ArtistId = 1; 
            Assert.AreEqual(1, artist.ArtistId);
        }
        [TestMethod]
        public void ArtistName_Should_Accept_Value()
        {
            var artist = new Artist();
            var name = "Дима Билан";
            artist.Name = name;
            Assert.AreEqual(name, artist.Name);
        }
        [TestMethod]
        public void ArtistName_Is_Not_Empty()
        {
            var artist = new Artist();

            Assert.ThrowsException<ArgumentException>(() => ValidateArtist(artist));
        }
        private void ValidateArtist(Artist artist)
        {
            if (string.IsNullOrWhiteSpace(artist.Name))
            {
                throw new ArgumentException("Строка с именем артиста не может быть пустой");
            }
        }
        [TestMethod]
        public void GenreId_Should_Be_Set_And_Get()
        {
            var genre = new Genre();
            genre.GenreId = 1;
            Assert.AreEqual(1, genre.GenreId);
        }
        [TestMethod]
        public void GenreName_Should_Accept_Value()
        {
            var genre = new Genre();
            var name = "Поп";
            genre.Name = name;
            Assert.AreEqual(name, genre.Name);
        }
        [TestMethod]
        public void GenreName_Is_Not_Empty()
        {
            var genre = new Genre();

            Assert.ThrowsException<ArgumentException>(() => ValidateGenre(genre));
        }
        private void ValidateGenre(Genre genre)
        {
            if (string.IsNullOrWhiteSpace(genre.Name))
            {
                throw new ArgumentException("Строка с названием жанра не может быть пустой");
            }
        }
        [TestMethod]
        public void TrackId_Should_Be_Set_And_Get()
        {
            var track = new Track();
            track.TrackId = 1;
            Assert.AreEqual(1, track.TrackId);
        }
        [TestMethod]
        public void TrackTitle_Should_Accept_Value()
        {
            var track = new Track();
            var title = "Я твой номер один";
            track.Title = title;
            Assert.AreEqual(title, track.Title);
        }
        [TestMethod]
        public void TrackTitle_Is_Not_Empty()
        {
            var track = new Track();

            Assert.ThrowsException<ArgumentException>(() => ValidateTrack(track));
        }
        private void ValidateTrack(Track track)
        {
            if (string.IsNullOrWhiteSpace(track.Title))
            {
                throw new ArgumentException("Строка с названием трека не может быть пустой");
            }
        }
        [TestMethod]
        public void UserId_Should_Be_Set_And_Get()
        {
            var user = new User();
            user.UserId = 1;
            Assert.AreEqual(1, user.UserId);
        }
        [TestMethod]
        public void UserLogin_Should_Accept_Value()
        {
            var user = new User();
            var login = "логин";
            user.Login = login;
            Assert.AreEqual(login, user.Login);
        }
        [TestMethod]
        public void UserLogin_Is_Not_Empty()
        {
            var user = new User();
            Assert.ThrowsException<ArgumentException>(() => ValidateUser(user));
        }
        [TestMethod]
        public void UserPasswordHash_Should_Accept_Value()
        {
            var user = new User();
            var passwordHash = "пароль";
            user.PasswordHash = passwordHash;
            Assert.AreEqual(passwordHash, user.PasswordHash);
        }
        [TestMethod]
        public void PasswordHash_Is_Not_Empty()
        {
            var user = new User();
            user.PasswordHash = "";
            Assert.ThrowsException<ArgumentException>(() => ValidateUser(user));
        }
        private void ValidateUser(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Login))
            {
                throw new ArgumentException("Строка с логином не может быть пустой");
            }
            if (string.IsNullOrWhiteSpace(user.PasswordHash))
            {
                throw new ArgumentException("Строка с паролем не может быть пустой");
            }
        }
        [TestMethod]
        public void PreferenceId_Should_Be_Set_And_Get()
        {
            var preference = new UserPreference();
            preference.PreferenceId = 1 ;
            Assert.AreEqual(1, preference.PreferenceId);
        }
    }
}
