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
        //GetBookId is array because database can contain more than 1 book copy
        public string[] GetBookId(BookInputModel book);
        public void DeleteBook(string id);
        public void MoveBook();
        public void AddIssue();
    }
}
