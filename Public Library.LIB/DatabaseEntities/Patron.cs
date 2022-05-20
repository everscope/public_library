namespace Public_Library.LIB
{
    public class Patron
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public ICollection<Issue> Issues { get; set; } = new List<Issue>();
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
