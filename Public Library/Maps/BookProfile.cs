using AutoMapper;
using Public_Library.LIB;

namespace Public_Library.Maps
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<BookInputModel, Book>();
            CreateMap<Book, BookDisplayModel>();
        }
    }
}
