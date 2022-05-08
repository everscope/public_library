using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Public_Library.LIB.Interfaces
{
    public interface IDatabaseReader
    {
        public void AddPatron(Patron patron);
        public void DeletePatron(Patron patron);
        public void AddBook(Book book);
        public void MoveBook();
        public void AddIssue();
    }
}
