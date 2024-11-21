// TransactionService
using milestone3library.Dto;
using milestone3library.Entity;
using milestone3library.Interface;
using NuGet.Protocol.Core.Types;
using System.Net;

namespace milestone3library.Service
{


    public class BookTransactionService : IBookTransactionService
    {
        private readonly IBookTransactionRepository _bookTransactionRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMemberRepository _memberRepository;

        public BookTransactionService(
            IBookTransactionRepository bookTransactionRepository,
            IBookRepository bookRepository,
            IMemberRepository memberRepository)
        {
            _bookTransactionRepository = bookTransactionRepository;
            _bookRepository = bookRepository;
            _memberRepository = memberRepository;
        }


        public async Task<BookTransactionDTO> RentBookAsync(RentBookDTO rentBookDTO)
        {
            var member = await _bookTransactionRepository.GetMemberByIdAsync(rentBookDTO.MemberId);
            if (member == null)
                throw new Exception("Member not found.");

            if (member.IsRestricted)
                throw new Exception("Member is restricted from borrowing books. Please clear pending fines.");

            var book = await _bookTransactionRepository.GetBookByIdAsync(rentBookDTO.BookId);
            if (book == null || book.AvailableCopies <= 0)
                throw new Exception("Book not available.");

            var transaction = new BookTransaction
            {
                BookId = book.Id,
                MemberId = member.Id,
                IssueDate = rentBookDTO.IssueDate,
                DueDate = rentBookDTO.DueDate,
                ReturnDate = null
            };

            book.AvailableCopies--;
            await _bookTransactionRepository.AddTransactionAsync(transaction);

            return new BookTransactionDTO
            {
                Id = transaction.Id,
                BookId = transaction.BookId,
                BookTitle = book.Title,
                MemberId = transaction.MemberId,
                MemberName = member.Name,
                IssueDate = transaction.IssueDate,
                DueDate = transaction.DueDate
            };
        }










        public async Task<BookTransactionDTO> ReturnBookAsync(ReturnBookDTO returnBookDTO)
        {
            // Fetch the transaction details
            var transaction = await _bookTransactionRepository.GetTransactionByIdAsync(returnBookDTO.TransactionId);
            if (transaction == null)
                throw new Exception("Transaction not found.");

            // Fetch the book details
            var book = await _bookTransactionRepository.GetBookByIdAsync(transaction.BookId);
            if (transaction.ReturnDate != null)
                throw new Exception("Book already returned.");

            // Fetch the member details
            var member = await _bookTransactionRepository.GetMemberByIdAsync(transaction.MemberId);

            // Calculate fine if returned late
            if (returnBookDTO.ReturnDate > transaction.DueDate)
            {
                var overdueDays = (returnBookDTO.ReturnDate - transaction.DueDate).Days;
                var fineAmount = overdueDays * 10; // Example: $10 fine per day

                // Mark the transaction as having an unpaid fine
                transaction.ReturnDate = returnBookDTO.ReturnDate;
                transaction.FineAmount = fineAmount; // Assuming `FineAmount` is a new property in the `BookTransaction` entity
                transaction.FinePaid = false;       // Assuming `FinePaid` is a boolean property in the `BookTransaction` entity

                // Restrict future borrowing by setting a flag (or you can check FinePaid status during borrowing)
                member.IsRestricted = true;        // Assuming `IsRestricted` is a new property in the `Member` entity

                // Update transaction and member
                await _bookTransactionRepository.UpdateTransactionAsync(transaction);
                await _bookTransactionRepository.SaveChangesAsync();

                throw new Exception($"Book returned late. Fine imposed: ${fineAmount}. Please pay the fine to borrow books again.");
            }

            // If returned on time
            transaction.ReturnDate = returnBookDTO.ReturnDate;
            book.AvailableCopies++;

            // Update the database
            await _bookTransactionRepository.UpdateTransactionAsync(transaction);
            await _bookTransactionRepository.SaveChangesAsync();

            return new BookTransactionDTO
            {
                Id = transaction.Id,
                BookId = transaction.BookId,
                BookTitle = book.Title,
                MemberId = transaction.MemberId,
                MemberName = member.Name,
                IssueDate = transaction.IssueDate,
                DueDate = transaction.DueDate,
                ReturnDate = transaction.ReturnDate
            };
        }



        public async Task<List<BookTransactionDTO>> GetAllTransactionsAsync()
        {
            var transactions = await _bookTransactionRepository.GetAllTransactionsAsync();
            return transactions.Select(t => new BookTransactionDTO
            {
                Id = t.Id,
                BookId = t.BookId,
                BookTitle = t.Book.Title,
                MemberId = t.MemberId,
                MemberName = t.Member.Name,
                IssueDate = t.IssueDate,
                DueDate = t.DueDate,
                ReturnDate = t.ReturnDate
            }).ToList();
        }

        public async Task<BookTransactionDTO> GetTransactionByIdAsync(int id)
        {
            var transaction = await _bookTransactionRepository.GetTransactionByIdAsync(id);
            if (transaction == null)
                throw new Exception("Transaction not found.");

            return new BookTransactionDTO
            {
                Id = transaction.Id,
                BookId = transaction.BookId,
                BookTitle = transaction.Book.Title,
                MemberId = transaction.MemberId,
                MemberName = transaction.Member.Name,
                IssueDate = transaction.IssueDate,
                DueDate = transaction.DueDate,
                ReturnDate = transaction.ReturnDate
            };
        }



        public async Task ClearFineAsync(int transactionId)
        {
            var transaction = await _bookTransactionRepository.GetTransactionByIdAsync(transactionId);
            if (transaction == null)
                throw new Exception("Transaction not found.");

            if (transaction.FineAmount == null || transaction.FinePaid)
                throw new Exception("No fine to clear.");

            transaction.FinePaid = true;

            // Remove borrowing restriction for the member
            var member = await _bookTransactionRepository.GetMemberByIdAsync(transaction.MemberId);
            member.IsRestricted = false;

            await _bookTransactionRepository.UpdateTransactionAsync(transaction);
            await _bookTransactionRepository.SaveChangesAsync();
        }
















    }
}
