using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using FluentEmail.Core;

namespace Library.Models
{
    public enum ModuleStatus
    {
        Draft,
        Checked,
        Invalid
    }
    public class Module
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime Expiration { get; set; }
        public bool Visible { get; set; }
        [Required]
        public ModuleStatus Status { get; set; }
        public virtual ICollection<ModuleConstraint> ModuleConstraints { get; set; }
        public virtual ICollection<LearningUnitModule> LearningUnitModules { get; set; }

        public async Task SendUpdateMail(IFluentEmail mail)
        {
            IFluentEmail email = mail
                .To("s.leeflang@hotmail.com", "Opleidingsverantwoordelijke HBO-ICT") // ToDo: Opleidingsverantwoordelijke probably should be configurable
                .Subject("Module is aangepast")
                .Body("Content"); // ToDo: Actually render a proper mail with a link for the opleidingsverantwoordelijke to review the changes and make a decision once logged in.

            await email.SendAsync();
        }

        public Module(string name, string description, DateTime expiration, bool visible, ModuleStatus status)
        {
            Name = name;
            Description = description;
            Expiration = expiration;
            Visible = visible;
            Status = status;
        }

        public Module() {}
    }
}