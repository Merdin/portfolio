namespace Library.Models
{
    public class LearningUnitModule
    {
        public int LearningUnitId { get; set; }
        public LearningUnit Unit { get; set; }
        public int ModuleId { get; set; }
        public Module Module { get; set; }

    }
}