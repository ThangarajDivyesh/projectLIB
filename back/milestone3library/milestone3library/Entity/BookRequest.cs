using System.ComponentModel.DataAnnotations;

namespace milestone3library.Entity
{
    public class BookRequest
    {
        [Key]
        public int Id { get; set; }

        public int MemberId { get; set; }

        public Member? member { get; set; }
        public int BookId { get; set; }

        public Book? book { get; set; }
        public string Message { get; set; } // Predefined or custom message
        public DateTime RequestDate { get; set; } = DateTime.Now;
        public bool IsProcessed { get; set; } = false; // Tracks whether the admin responded
    }
}

