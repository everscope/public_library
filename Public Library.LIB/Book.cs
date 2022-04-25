namespace Public_Library.LIB
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Auther { get; set; }
        public string Placement { get; set; }

        public bool IsBorrowed { get; set; } = false;
        public BookState BookState { get; set; }
        public Patron Patron { get; set; }
    }
}