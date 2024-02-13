namespace LibraryAPI.DTO
{
    public class LoanDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Mail { get; set; } 
        public string Title { get; set; }
    }
}
