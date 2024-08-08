using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PitchPerfectHearingTest.Models
{
    [Table("Users")]
    public class User : Entity
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string EmailAdres { get; set; }
    }
}