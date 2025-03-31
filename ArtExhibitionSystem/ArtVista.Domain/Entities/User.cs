namespace ArtVista.Domain.Entities
{
    public class User
    {
        public string UserId { get; set; } = Guid.NewGuid().ToString();
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public virtual ICollection<FavoriteArtwork> FavoriteArtworks { get; set; }

    }
}
