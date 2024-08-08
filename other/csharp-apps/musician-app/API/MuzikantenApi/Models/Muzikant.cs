using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MuzikantenApi.Models
{
    public class Muzikant
    {
        public int MuzikantId { get; set; }
        [Required]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "De voornaam moet minimaal 3 en mag maximaal 40 karakters bevatten. ")]
        public string Voornaam { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "De achternaam moet minimaal 3 en mag maximaal 50 karakters bevatten. ")]
        public string Achternaam { get; set; }
        [Required]
        [Range(1900, 2022, ErrorMessage = "Het startjaar moet zich bevinden tussen 1900 en 2022")]
        public int StartJaar { get; set; }
        [Range(1900, 2022, ErrorMessage = "Het stopjaar moet zich bevinden tussen 1900 en 2022")]
        public int? StopJaar { get; set; }
        [JsonIgnore] // deze moest ik doen omdat ik anders een foutmelding kreegs
        public ICollection<Instrument> Instrumenten { get; set; }

        public Muzikant()
        {

        }

        public Muzikant(string voornaam, string achternaam, int startJaar, int? stopJaar)
        {
            Voornaam = voornaam;
            Achternaam = achternaam;
            StartJaar = startJaar;
            StopJaar = stopJaar;
        }
    }
}
