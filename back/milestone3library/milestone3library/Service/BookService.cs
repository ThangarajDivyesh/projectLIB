using Microsoft.AspNetCore.Hosting;
using Microsoft.OpenApi.Extensions;
using milestone3library.Dto;
using milestone3library.Entity;
using milestone3library.Interface;
using milestone3library.Repository;
using static milestone3library.Repository.MemberRepository;

namespace milestone3library.Service
{
    public class BookService :IBookService
    {

        private readonly IBookRepository _bookRepository;
        private IWebHostEnvironment _webHostEnvironment;
        private readonly milestone3library.Interface.IMemberRepository _memberRepository;
        private readonly IBookTransactionRepository _bookTransactionRepository;
       
        public BookService(IBookRepository bookRepository, IWebHostEnvironment webHostEnvironment, Interface.IMemberRepository memberRepository, IBookTransactionRepository bookTransactionRepository)
        {

            _bookRepository = bookRepository;
            _webHostEnvironment = webHostEnvironment;
            _memberRepository = memberRepository;
            _bookTransactionRepository = bookTransactionRepository;
        }


        // Method to get all books
        public async Task<IEnumerable<BookResponseDto>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllBooksAsync();

            return books.Select(b => new BookResponseDto
            {
                Id = b.Id,
                ISBN = b.ISBN,
                Title = b.Title,
                AuthorName = b.AuthorName ,
                GenreName =b.GenreName,
                AvailableCopies = b.AvailableCopies,
                TotalCopies = b.TotalCopies,
            });
        }


        // Method to get a book by ID
        public async Task<BookResponseDto> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book == null)
                return null;

            return new BookResponseDto
            {
                Id = book.Id,
                ISBN = book.ISBN,
                Title = book.Title,

                AuthorName = book.AuthorName,
                GenreName = book.GenreName,
                //GenreName = book.Genre.Name,     // Map Genre name here
                PublicationDate = book.PublicationDate,
                AvailableCopies = book.AvailableCopies
            };
        }

        // Method to rent a book
        //public async Task RentBookAsync(int id)
        //{
        //    await _bookRepository.RentBookAsync(id);
        //}

        // Method to return a book
        public async Task ReturnBookAsync(int id, int memberId)
        {
            //await _bookRepository.ReturnBookAsync(id);
            var book = await _bookRepository.GetBookByIdAsync(id);
            var member = await _memberRepository.GetMemberByIdAsync(memberId);

            if (book == null || member == null)
            {
                throw new ArgumentException("Book or Member not found.");
            }

            if (book.AvailableCopies <= 0)
            {
                throw new InvalidOperationException("No available copies of the book.");
            }

            // Create a new BookTransaction
            var transaction = new BookTransaction
            {
                BookId = book.Id,
                MemberId = member.Id,
                IssueDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(07),  // Set a due date (e.g., 14 days)
            };

            // Save the transaction
            await _bookTransactionRepository.AddTransactionAsync(transaction);

            // Update the available copies of the book
            book.AvailableCopies--;
            await _bookRepository.SaveChangesAsync();
        }

        // Method to add a new book
        public async Task<BookResponseDto> AddBookAsync(BookRequestDto bookRequest)
        {
            var book = new Book
            {
                Title = bookRequest.Title,
                ISBN = bookRequest.ISBN,
                AuthorName = bookRequest.AuthorName,
                GenreName = bookRequest.GenreName,
                PublicationDate = bookRequest.PublicationDate,
                TotalCopies = bookRequest.TotalCopies,
                AvailableCopies = bookRequest.TotalCopies
            };

            // Save the book using the repository
            await _bookRepository.AddBookAsync(book);

            // Return the created book as a response DTO
            return new BookResponseDto
            {
                Id = book.Id,
                Title = book.Title,
                ISBN = book.ISBN,
                AuthorName = "Author name here if required", // Replace with actual logic if needed
                GenreName = "Genre name here if required",   // Replace with actual logic if needed
                PublicationDate = book.PublicationDate,
                AvailableCopies = book.AvailableCopies
            };
        }

        // Method to update an existing book
        public async Task UpdateBookAsync(int id, BookRequestDto bookRequest)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book != null)
            {
                book.Title = bookRequest.Title;
                book.ISBN = bookRequest.ISBN;
                book.AuthorName = bookRequest.AuthorName;
                book.GenreName = bookRequest.GenreName;
                book.PublicationDate = bookRequest.PublicationDate;
                book.TotalCopies = bookRequest.TotalCopies;
                book.AvailableCopies = bookRequest.TotalCopies;  // Reset available copies

                await _bookRepository.UpdateBookAsync(book);
            }
        }

        // Method to delete a book
        public async Task DeleteBookAsync(int id)
        {
            await _bookRepository.DeleteBookAsync(id);
        }

        // Method to get books by GenreId (with optional filter)
        //public async Task<IEnumerable<BookResponseDto>> GetBooksAsync(int? genreId)
        //{
        //    var books = await _bookRepository.GetAllBooksAsync();

        //    if (genreId.HasValue)
        //    {
        //        books = books.Where(b => b.GenreId == genreId.Value).ToList();  // Filter by GenreId if provided
        //    }

        //    return books.Select(b => new BookResponseDto
        //    {
        //        Id = b.Id,
        //        Title = b.Title,
        //        ISBN = b.ISBN,
        //        AuthorName = b.Author.Name,   // Map Author name here
        //        GenreName = b.Genre.Name,      // Map Genre name here
        //        AvailableCopies = b.AvailableCopies
        //    });
        //}


    }

   
}

       
    

