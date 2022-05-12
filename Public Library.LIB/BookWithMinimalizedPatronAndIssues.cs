using Public_Library.LIB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Public_Library.LIB
{
    public class BookWithMinimalizedPatronAndIssues
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string? Placement { get; set; }
        public string? Description { get; set; }

        public bool IsBorrowed { get; set; } = false;
        public BookState BookState { get; set; }
        public MinimalizedPatronModel? Patron { get; set; }
        public ICollection<IssueDisplayModel> Issues { get; set; } = new List<IssueDisplayModel>();
    }
}
