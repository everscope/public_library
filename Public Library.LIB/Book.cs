namespace Public_Library.LIB
{
    public class Book
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string? Placement { get; set; }
        public string? Description { get; set; }

        public bool IsBorrowed { get; set; } = false;
        public BookState BookState { get; set; }
        public Patron? Patron { get; set; }
    }
}