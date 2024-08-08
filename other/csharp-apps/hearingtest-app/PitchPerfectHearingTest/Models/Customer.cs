using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PitchPerfectHearingTest.Models
{
    [Table("Customers")]
    public class Customer : Entity
    {
        [Key]
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }

        public Customer() { }

        public Customer(string name, string email, int age)
        {
            Name = name;
            Email = email;
            Age = age;
        }

        public Customer(int id,string name, string email, int age)
        {
            CustomerId = id;
            Name = name;
            Email = email;
            Age = age;
        }
    }
}
