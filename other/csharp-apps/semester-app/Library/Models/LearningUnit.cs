using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using FluentEmail.Core;

namespace Library.Models
{
    public enum UnitType
    {
        ExploringIt,
        Profielkeuze,
        VerdiependeNiveau3,
        VerbredendeNiveau3,
        Overig,
        Reperatiesemester,
    }

    public class LearningUnit
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public UnitType Type { get; set; }
        [Required]
        public string Description { get; set; }
        public string Period { get; set; }
        public User ContactPerson { get; set; }
        public bool Visible { get; set; }
        public ModuleStatus Status { get; set; }
        [Required]
        public DateTime Expiration { get; set; }
        public ICollection<LearningUnitModule> LearningUnitModules { get; set; }

        public async Task SendUpdateMail(IFluentEmail mail)
        {
            IFluentEmail email = mail
                .To("s.leeflang@hotmail.com", "Opleidingsverantwoordelijke HBO-ICT") // ToDo: Opleidingsverantwoordelijke probably should be configurable
                .Subject("Semester is aangepast")
                .Body("Content"); // ToDo: Actually render a proper mail with a link for the opleidingsverantwoordelijke to review the changes and make a decision once logged in.

            await email.SendAsync();
        }
    }
}