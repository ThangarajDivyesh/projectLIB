using milestone3library.Dto;
using milestone3library.Entity;

namespace milestone3library.Interface
{
    public interface IMemberService
    {
        Task<IEnumerable<Member>> GetAllMembersAsync();
        Task<Member> GetMemberByIdAsync(int id);
        Task AddMemberAsync(MemberRequestDto memberRequest);
        Task UpdateMemberAsync(int id, MemberRequestDto memberRequest);
        Task DeleteMemberAsync(int id);
        Task<Member> AuthenticateAsync(string nic, string password);
       

    }
}
