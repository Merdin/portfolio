using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MuzikantenApi.Models
{
    public class Instrument
    {
        public int InstrumentId { get; set; }
        [Required]
        [MaxLength(40, ErrorMessage = "De naam max maximaal 40 karakters bevatten")]
        public string Naam { get; set; }
        [MaxLength(30, ErrorMessage = "De type max maximaal 30 karakters bevatten")]
        public string? Type { get; set; }
        [MaxLength(30, ErrorMessage = "De bijnaam max maximaal 30 karakters bevatten")]
        public string? Bijnaam { get; set; }
        public int Waarde { get; set; }
        public Muzikant Muzikant { get; set; }
        public int MuzikantId { get; set; }

        public Instrument()
        {

        }

        public Instrument(string naam, string type, string bijnaam, int waarde, int muzikantId)
        {
            Naam = naam;
            Type = type;
            Bijnaam = bijnaam;
            Waarde = waarde;
            MuzikantId = muzikantId;
        }
    }
}
