using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using PitchPerfectHearingTest.DataAccess.Repositories;
using PitchPerfectHearingTest.DataAccess;
using Newtonsoft.Json;
using System.Diagnostics;

namespace PitchPerfectHearingTest.Models
{
    public class HearingTest
    {
        private HearingTestResult _hearingTestResult;
        private SoundGenerator _soundGenerator;
        private static CancellationTokenSource _tokenSource = null;
        private bool _testEnded = false;

        public event EventHandler SoundHeard;
        public event EventHandler TestEnd;
        
        public HearingTestResult Result 
        {
            get { return _hearingTestResult; }
        }

        public bool TestEnded
        { 
            get { return _testEnded; } 
            set
            {
                _testEnded = value;
            }
        }

        public HearingTest()
        {
            _hearingTestResult = new HearingTestResult();
            _soundGenerator = new SoundGenerator();
            _tokenSource = new CancellationTokenSource();
            SoundHeard += OnSoundHeard;
        }

        // Handles the start of the hearing
        public void HandleStartTest()
        {
            Task.Run(() => StartTest()); 
        }

        // Generates a test pattern to test the hearing of the customer. 
        public async void StartTest() 
        {
            List<Frequency> frequencies = ListShuffler(Enum.GetValues(typeof(Frequency)).Cast<Frequency>().ToList());
            foreach(Frequency frequency in frequencies)
            {
                List<SoundLevel> soundLevels = GetAllSoundLevelsFromFrequency(frequency);

                foreach(var soundLevel in soundLevels)
                {
                    CancellationToken token = ResetToken();

                    _soundGenerator.PlaySound(soundLevel);
                    await Wait(token);
                    bool didHearSound = EndSound(token, soundLevel);           
                    Thread.Sleep(800);
                    
                    if (didHearSound)
                    {
                        break;
                    }
                }
            }
            
            List<Decibel> decibels = Enum.GetValues(typeof(Decibel)).Cast<Decibel>().ToList();
            List<SoundLevel> currentHearingTestScore = new List<SoundLevel>(_hearingTestResult.GetScores());
            _hearingTestResult.Clear();
            foreach(SoundLevel soundLevel in currentHearingTestScore)
            {           
                int currentHeardDecibel = decibels.FindIndex(decibel => decibel == soundLevel.Decibel);

                for(int i = currentHeardDecibel -2; i < currentHeardDecibel + 2; i++) 
                {
                    if (i < 0 || i > decibels.Count) 
                    {
                        continue;
                    }

                    SoundLevel sound = new SoundLevel(soundLevel.Frequency, decibels[i]);
                    
                    CancellationToken token = ResetToken();

                    _soundGenerator.PlaySound(sound);
                    await Wait(token);
                    bool didHearSound = EndSound(token, sound);
                    Thread.Sleep(800);

                    if (didHearSound)
                    {
                        break;
                    }
                }
            }

            EndTest();
        }

        // Cancles the token
        protected virtual void OnSoundHeard(object sender, EventArgs e)
        {
            _tokenSource.Cancel();
        }

        private CancellationToken ResetToken()
        {
            _tokenSource.Dispose();
            _tokenSource = new CancellationTokenSource();
            return _tokenSource.Token;
        }

        // Stops the sound from playing
        private bool EndSound(CancellationToken token, SoundLevel soundLevel)
        {
            bool didHearSound = false;
            if (token.IsCancellationRequested)
            {
                _hearingTestResult.AddSoundLevel(soundLevel);
                didHearSound = true;
            }
            _soundGenerator.StopSound();
            return didHearSound;
        }

        // Waits 3 seconds before playing a different sound or until the sound is heard
        private static async Task Wait(CancellationToken token)
        {
            for (int i = 0; i < 1200; i++)
            {
                Thread.Sleep(1);

                if (token.IsCancellationRequested)
                {
                    i = 1200;
                }
            }
        }

        // Sets the sound heard variable to true
        public void HeardSound()
        {
            SoundHeard?.Invoke(this, EventArgs.Empty);
        }
        
        // Stops the tests
        private void EndTest()
        {
            StoreTestResult();
            TestEnded = true;
            TestEnd?.Invoke(this, EventArgs.Empty);
        }

        // Return a shuffled list
        private List<T> ListShuffler<T>(List<T> items)
        {
            Random rand = new Random();

            for (int i = 0; i < items.Count - 1; i++)
            {
                int j = rand.Next(i, items.Count);
                T temp = items[i];
                items[i] = items[j];
                items[j] = temp;
            }
            
            return items;
        }

        // All SoundLevels get generated here with the use of a given frequency
        private List<SoundLevel> GetAllSoundLevelsFromFrequency(Frequency frequency)
        {
            List<SoundLevel> allSounds = new List<SoundLevel>();
            
            foreach(Decibel decibel in Enum.GetValues(typeof(Decibel)))
            {
                SoundLevel sound = new SoundLevel(frequency, decibel);
                allSounds.Add(sound);
            }

            return allSounds;
        }

        private void StoreTestResult()
        {
            var testScores = _hearingTestResult.GetScores();
            _hearingTestResult.Scores = JsonConvert.SerializeObject(testScores, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });

            try
            {
                _hearingTestResult.CreatedOn = DateTime.Now;
                new HearingTestResultRepository(new ApplicationContext()).Create(_hearingTestResult);
                App.Current.Properties.Add("TestResultId", _hearingTestResult.HearingTestResultId);
            }
            catch (Exception excep)
            {
                Debug.WriteLine(excep.Message);
                throw;
            }
        }
    }
}