using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Public_Library.LIB.Interfaces
{
    public class IssueDisplayModel
    {
        public int Id { get; set; }
        public string BookId { get; set; }
        public MinimalizedBookModel Book { get; set; }
        public string PatronId { get; set; }
        public MinimalizedPatronModel Patron { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public DateTime ReturnDateTime { get; set; }
        public bool isClosed { get; set; }
        public bool isExpired { get; set; }
        public int AllowedDaysAmount { get; set; } = 14;
    }
}
