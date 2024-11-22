using milestone3library.Dto;
using milestone3library.Entity;
using milestone3library.Interface;

namespace milestone3library.Service
{
    public class MemberService:IMemberService
    {
        private readonly IMemberRepository _memberRepository;

        public MemberService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }
        public interface IMemberService
        {
            Task<IEnumerable<Member>> GetAllMembersAsync();
            Task<Member> GetMemberByIdAsync(int id);
            Task AddMemberAsync(MemberRequestDto memberRequest);
            Task UpdateMemberAsync(int id, MemberRequestDto memberRequest);
            Task DeleteMemberAsync(int id);
        }
        public async Task<IEnumerable<Member>> GetAllMembersAsync() =>
       await _memberRepository.GetAllMembersAsync();

        public async Task<Member> GetMemberByIdAsync(int id) =>
            await _memberRepository.GetMemberByIdAsync(id);

        public async Task AddMemberAsync(MemberRequestDto memberRequest)
        {
            var member = new Member
            {
                Name = memberRequest.Name,
                NicNumber = memberRequest.NicNumber,
                Email = memberRequest.Email,
                Password = memberRequest.Password,
                Phonenumber = memberRequest.Phonenumber,
              
            
            };
            await _memberRepository.AddMemberAsync(member);
        }

        public async Task UpdateMemberAsync(int id, MemberRequestDto memberRequest)
        {
            var member = new Member
            {
                Id = id,
                Name = memberRequest.Name,
                NicNumber = memberRequest.NicNumber,
                Email = memberRequest.Email,
                Password = memberRequest.Password,
                Phonenumber=memberRequest.Phonenumber,
              
              
            };
            await _memberRepository.UpdateMemberAsync(member);
        }

        public async Task DeleteMemberAsync(int id) =>
            await _memberRepository.DeleteMemberAsync(id);



        public async Task<Member> AuthenticateAsync(string nic, string password)
        {
            // Find the member by NIC
            var member = await _memberRepository.Login(nic);

            if (member == null || member.Password != password) // Directly check password
            {
                return null; // Invalid NIC or password
            }

            return member; // Authentication successful
        }




    }
}

