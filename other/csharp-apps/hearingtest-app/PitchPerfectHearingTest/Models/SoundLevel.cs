namespace PitchPerfectHearingTest.Models
{
    public class SoundLevel
    {
        public Frequency Frequency { get; }
        public Decibel Decibel { get; set; }

        public SoundLevel(Frequency frequency, Decibel decibel)
        {
            Frequency = frequency;
            Decibel = decibel;
        }

        public override string ToString()
        {
            return $"({Frequency.ToString()}) Klant hoorde geluid bij {Decibel.ToString()}";
        }
    }
}
