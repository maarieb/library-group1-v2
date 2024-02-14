namespace LibraryAPI.DTO
{
    public class LoanDTO
    {
        public int? Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Mail { get; set; } 
        public string Title { get; set; }
    }
}
