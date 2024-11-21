namespace milestone3library.Entity
{
    public class Book
    {

        public int Id { get; set; }

        public int ISBN { get; set; }
        public string Title { get; set; }
        

        public DateTime PublicationDate { get; set; }
        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; }

        public string AuthorName {  get; set; }

        public string GenreName { get; set; }
        


        

    }
}

