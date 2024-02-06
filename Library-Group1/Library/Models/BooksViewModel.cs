using Library.Entities;
using LibraryCore.Enums;

namespace Library.Models
{
    public class BooksViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int PagesNumber { get; set; }
        public int DomainId { get; set; }
        public int WriterId { get; set; }

        public string DomainName { get; set; }
        public string WriterName { get; set; }
        public BookState State {  get; set; }

    }
}
