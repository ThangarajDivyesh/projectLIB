using System.ComponentModel.DataAnnotations;

namespace milestone3library.Entity
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }

        public int MemberId { get; set; }

        public Member? member { get; set; }
        public string NotificationMessage { get; set; }
        public DateTime NotificationDate { get; set; } = DateTime.Now;
    }
}
