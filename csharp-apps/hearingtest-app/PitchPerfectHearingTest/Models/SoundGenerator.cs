using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace PitchPerfectHearingTest.Models
{
    public sealed class SoundGenerator
    {
        private SignalGenerator _sound;
        private WaveOutEvent _wo;

        public SoundGenerator()
        {
            _wo = new WaveOutEvent();
            _sound = new SignalGenerator()
            {
                Gain = 0.2,
                Type = SignalGeneratorType.Sin
            };
        }

        public void PlaySound(SoundLevel sound)
        {
            _sound.Frequency = (int)sound.Frequency;
            _wo.Volume = System.Math.Abs(((float)sound.Decibel / 150));
            _wo.Init(_sound);
            _wo.Play();
        }

        public void StopSound()
        {
            _wo.Stop();
        }
    }
}