namespace milestone3library.Entity
{
    public class BookTransaction
    {
        public int Id { get; set; }  // Unique transaction ID
        public int BookId { get; set; }  // Foreign Key to Book
        public Book Book { get; set; }  // Navigation property

        public int MemberId { get; set; }  // Foreign Key to Member
        public Member Member { get; set; }  // Navigation property


        public decimal? FineAmount { get; set; } // Fine amount if returned late
        public bool FinePaid { get; set; }       // Indicates if the fine is paid

        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
