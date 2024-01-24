using Library.Entities;

namespace Library.Models
{
    public class BookViewModel
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public int PagesNumber { get; set; }
        public int DomainId { get; set; }
        public int WriterId { get; set; }

    }
}
