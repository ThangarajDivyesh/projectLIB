namespace milestone3library.Dto
{
    public class BookResponseDto
    {
        public int Id { get; set; }
        public int ISBN { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }  // Change to string
        public string GenreName { get; set; }   // Change to string
        public DateTime PublicationDate { get; set; }
        public int AvailableCopies { get; set; }

        public int TotalCopies { get; set; }
        //public  string ImageUrl { get; set; }
    }
}
