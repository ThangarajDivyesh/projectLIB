using Microsoft.EntityFrameworkCore;
using milestone3library.Data;
using milestone3library.Entity;
using milestone3library.Interface;

namespace milestone3library.Repository
{
    public class MemberRepository: IMemberRepository
    {

    private readonly libraryDbcontext _context;
        
        public MemberRepository(libraryDbcontext context)
        {
            _context = context;
        }

        public interface IMemberRepository
        {
            Task<IEnumerable<Member>> GetAllMembersAsync();
            Task<Member> GetMemberByIdAsync(int id);
            Task AddMemberAsync(Member member);
            Task UpdateMemberAsync(Member member);
            Task DeleteMemberAsync(int id);
        }

        public async Task<IEnumerable<Member>> GetAllMembersAsync() =>
       await _context.Members.ToListAsync();

        public async Task<Member> GetMemberByIdAsync(int id) =>
            await _context.Members.FindAsync(id);

        public async Task AddMemberAsync(Member member)
        {
            await _context.Members.AddAsync(member);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMemberAsync(Member member)
        {
            _context.Members.Update(member);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMemberAsync(int id)
        {
            var member = await GetMemberByIdAsync(id);
            if (member != null)
            {
                _context.Members.Remove(member);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Member> Login(string nic)
        {
            return await _context.Members.SingleOrDefaultAsync(m => m.NicNumber == nic);
        }

    }
}
