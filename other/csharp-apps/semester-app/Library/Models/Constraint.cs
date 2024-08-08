using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Library.Models
{
    public enum ConstraintType
    {
        MinimumPoints,
        MinimumModule,
    }

    public class Constraint
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set;}
        [Required]
        public ConstraintType Type { get; set; }
        [Required]
        public string Value { get; set; }
        public virtual ICollection<ModuleConstraint> ModuleConstraints { get; set; }
    }
}
