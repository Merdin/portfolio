using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Newtonsoft.Json;

namespace Library.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Token { get; set; }
        
        [Range(0,240)]
        public int? Credits { get; set; }
        
        [Range(1,4)]
        public int? Grade { get; set; }
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public bool ReceivesEmailNotifications { get; set; }
        public ICollection<StudyPlan> StudyPlans { get; set; }

        public static User Create(ClaimsIdentity identity)
        {
            return new User
            {
                Token = identity.FindFirst(ClaimTypes.NameIdentifier).Value,
                Email = identity.FindFirst(ClaimTypes.Email).Value,
                Name = identity.FindFirst("name").Value
            };
        }
    }
}