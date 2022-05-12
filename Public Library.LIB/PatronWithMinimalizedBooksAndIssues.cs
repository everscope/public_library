using Public_Library.LIB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Public_Library.LIB
{
    public class PatronWithMinimalizedBooksAndIssues
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public ICollection<IssueDisplayModel> Issues { get; set; } = new List<IssueDisplayModel>();
        public ICollection<MinimalizedBookModel> Books { get; set; } = new List<MinimalizedBookModel>();
    }
}
