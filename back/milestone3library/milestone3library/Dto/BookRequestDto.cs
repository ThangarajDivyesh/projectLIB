namespace milestone3library.Dto
{
    public class BookRequestDto
    {
        public int ISBN { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }  // Change to AuthorId (int)
        public string GenreName { get; set; }  // Change to GenreId (int)
        public DateTime PublicationDate { get; set; }
        public int TotalCopies { get; set; }
        //public IFormFile ImageUrl { get; set; }
    }
}
