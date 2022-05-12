using AutoMapper;
using Public_Library.LIB;

namespace Public_Library.Maps
{
    public class PatronProfile : Profile
    {
        public PatronProfile()
        {
            CreateMap<PatronInputModel, Patron>();
            CreateMap<Patron, MinimalizedPatronModel>();
            CreateMap<Patron, PatronWithMinimalizedBooksAndIssues>();
        }
    }
}
