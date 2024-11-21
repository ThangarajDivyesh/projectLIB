using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace milestone3library.Entity
{
    public class Member
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string NicNumber { get; set; }
        public string Email { get; set; }
  
        public string Password { get; set; }
         public int Phonenumber { get; set; }

        public bool IsRestricted { get; set; }



    }
}
