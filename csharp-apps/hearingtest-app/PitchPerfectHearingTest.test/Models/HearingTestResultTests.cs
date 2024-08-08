using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PitchPerfectHearingTest.Models.Tests
{
    [TestClass()]
    public class HearingTestResultTests
    {
        private HearingTestResult hearingTestResult;

        [TestInitialize]
        public void TestInitialize()
        {
            hearingTestResult = new HearingTestResult();
        }
        
        [TestMethod()]
        public void GetScoresWithEmptyListTest()
        {
            List<SoundLevel> emptyList = new List<SoundLevel>();
            CollectionAssert.AreEqual(emptyList, hearingTestResult.GetScores());
        }

        [TestMethod()]
        public void GetScoresWithFullListTest()
        {
            List<SoundLevel> fullList = new List<SoundLevel>();

            var soundLevelOne = new SoundLevel(Frequency.Hz250, Decibel.Db10);
            var soundLevelTwo = new SoundLevel(Frequency.Hz500, Decibel.Db10);
            var soundLevelThree = new SoundLevel(Frequency.Hz1000, Decibel.Db10);
            var soundLevelFour = new SoundLevel(Frequency.Hz2000, Decibel.Db10);
            var soundLevelFive= new SoundLevel(Frequency.Hz4000, Decibel.Db10);
            var soundLevelSix = new SoundLevel(Frequency.Hz8000, Decibel.Db10);
            
            fullList.Add(soundLevelOne);
            hearingTestResult.AddSoundLevel(soundLevelOne);
            fullList.Add(soundLevelTwo);
            hearingTestResult.AddSoundLevel(soundLevelTwo);
            fullList.Add(soundLevelThree);
            hearingTestResult.AddSoundLevel(soundLevelThree);
            fullList.Add(soundLevelFour);
            hearingTestResult.AddSoundLevel(soundLevelFour);
            fullList.Add(soundLevelFive);
            hearingTestResult.AddSoundLevel(soundLevelFive);
            fullList.Add(soundLevelSix);
            hearingTestResult.AddSoundLevel(soundLevelSix);

            CollectionAssert.AreEqual(fullList, hearingTestResult.GetScores());
        }

        [TestMethod()]
        public void AddSoundLevelTest()
        {
            hearingTestResult.AddSoundLevel(new SoundLevel(Frequency.Hz250, Decibel.Db10));

            var newSound = new SoundLevel(Frequency.Hz250, Decibel.Db5);
            hearingTestResult.AddSoundLevel(newSound);

            Assert.IsTrue((hearingTestResult.GetScores().FindIndex(soundLevel => soundLevel.Decibel == newSound.Decibel) >= 0));
        }

        [TestMethod()]
        public void AddSoundLevelFailTest()
        {
            hearingTestResult.AddSoundLevel(new SoundLevel(Frequency.Hz250, Decibel.Db10));

            var newSound = new SoundLevel(Frequency.Hz250, Decibel.Db40);
            hearingTestResult.AddSoundLevel(newSound);

            Assert.IsTrue((hearingTestResult.GetScores().FindIndex(soundLevel => soundLevel.Decibel == newSound.Decibel) < 0));
        }

        [TestMethod()]
        public void ClearTest()
        {
            hearingTestResult.AddSoundLevel(new SoundLevel(Frequency.Hz250, Decibel.Db10));
            hearingTestResult.AddSoundLevel(new SoundLevel(Frequency.Hz500, Decibel.Db10));
            hearingTestResult.AddSoundLevel(new SoundLevel(Frequency.Hz1000, Decibel.Db10));

            hearingTestResult.Clear();

            Assert.IsTrue(!hearingTestResult.GetScores().Any());
        }
    }
}