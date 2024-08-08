using Newtonsoft.Json;

namespace Library.Models
{
    public class ModuleConstraint
    {
        public int ModuleId { get; set; }
        public Module Module { get; set; }
        public int ConstraintId { get; set; }
        public Constraint Constraint { get; set; }
    }
}