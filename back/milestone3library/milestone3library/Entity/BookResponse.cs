using System.ComponentModel.DataAnnotations;

namespace milestone3library.Entity
{
    public class BookResponse
    {
        [Key]
        public int Id { get; set; }

        public int RequestId { get; set; }
        public string ResponseMessage { get; set; } // Predefined response
        public DateTime ResponseDate { get; set; } = DateTime.Now;
    }
}
