namespace SearchEngine.Web.Domain.Entites
{
    public class UserEntity : BaseEntity
    {
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Role { get; set; } = null!;
        public DateTime? LastLogin { get; set; }
    }
}
