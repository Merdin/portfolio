using System.ComponentModel.DataAnnotations;

namespace Library.Models
{

    public enum StudyPlanStatus
    {
        Draft,
        Submitted,
        CheckedByStudyCareerCounselor,
        Approved,
        Denied
    }
    public class StudyPlan
    {
        public int Id { get; set; }
        public User User { get; set; }
        public StudyPlanStatus Status { get; set; }

        [Required]
        public LearningUnit ExploringIt { get; set; }
        [Required]
        public LearningUnit ProfileChoiceOne { get; set; }
        [Required]
        public LearningUnit ProfileChoiceTwo { get; set; }
        [Required]
        public LearningUnit FreeChoiceOne { get; set; }
        [Required]
        public LearningUnit FreeChoiceTwo { get; set; }
        [Required]
        public LearningUnit FreeChoiceThree { get; set; }
    }
}
