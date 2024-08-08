using Microsoft.VisualStudio.TestTools.UnitTesting;
using PitchPerfectHearingTest.Models;
using PitchPerfectHearingTest.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Data;
using System.Windows.Shapes;

namespace PitchPerfectHearingTest.test
{
    [TestClass]
    public class AudiogramServiceTests
    {
        private List<SoundLevel> _filledScores;
        private List<SoundLevel> _emptyScores;

        [TestInitialize]
        public void Initialize()
        {
            _emptyScores = new List<SoundLevel>();

            _filledScores = new List<SoundLevel>() {
                new SoundLevel(Frequency.Hz250, Decibel.Db30),
                new SoundLevel(Frequency.Hz500, Decibel.Db20),
                new SoundLevel(Frequency.Hz1000, Decibel.Db10),
                new SoundLevel(Frequency.Hz2000, Decibel.Db5),
                new SoundLevel(Frequency.Hz4000, Decibel.Db15),
                new SoundLevel(Frequency.Hz8000, Decibel.Db20),
            };
        }

        [TestCleanup]
        public void CleanUp()
        {
            _emptyScores = null;
            _filledScores = null;
        }

        [TestMethod]
        public void AudiogramService_MapScore_PointCollectionIsEmpty()
        {
            bool? collectionIsEmpty = null;

            var t = new Thread(() => {
                var service = new AudiogramService();
                var collection = service.MapScore(_emptyScores);
                collectionIsEmpty = !collection.Any();
            });

            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();

            Assert.IsTrue(collectionIsEmpty ?? false);
        }

        [TestMethod]
        public void AudiogramService_MapScore_PointCollectionIsNotEmpty()
        {
            bool? collectionHasItems = null;

            var t = new Thread(() => {
                var service = new AudiogramService();
                var collection = service.MapScore(_filledScores);
                collectionHasItems = collection.Any() && collection.Count == _filledScores.Count;
            });

            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();

            Assert.IsTrue(collectionHasItems ?? false);
        }

        [TestMethod]
        public void AudiogramService_Audiogram_ContainsPath()
        {
            bool? canvasContainsPath = null;

            var t = new Thread(() => {
                var service = new AudiogramService();
                var audiogram = service.GenerateAudiogram();
                var children = audiogram.Children.Cast<UIElement>().ToList();
                canvasContainsPath = children.Any(c => c.GetType() == typeof(Path));
            });

            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();

            Assert.IsTrue(canvasContainsPath ?? false);
        }

        [TestMethod]
        public void AudiogramService_Audiogram_PathContainsBinding()
        {
            bool? pathContainsBinding = null;

            var t = new Thread(() => {
                var service = new AudiogramService();
                var audiogram = service.GenerateAudiogram();
                var children = audiogram.Children.Cast<UIElement>().ToList();
                var path = children.FirstOrDefault(c => c.GetType() == typeof(Path));
                pathContainsBinding = BindingOperations.GetBinding(path, Path.DataProperty).Path != null;
            });

            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();

            Assert.IsTrue(pathContainsBinding ?? false);
        }
    }
}