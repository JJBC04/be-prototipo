namespace be_prototipo.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Fullname { get; set; }
        public string? Genre { get; set; }
        public int? Age { get; set; }

        public string? Email { get; set; }
        public string? MobileNumber { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
