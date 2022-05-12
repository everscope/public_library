using AutoMapper;
using Public_Library.LIB;
using Public_Library.LIB.Interfaces;

namespace Public_Library.Maps
{
    public class IssueProfile : Profile
    {
        public IssueProfile()
        {
            CreateMap<Issue, IssueDisplayModel>();
        }
    }
}
