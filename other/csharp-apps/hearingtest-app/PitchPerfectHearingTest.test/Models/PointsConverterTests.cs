using Microsoft.VisualStudio.TestTools.UnitTesting;
using PitchPerfectHearingTest.Models;
using System.Windows;
using System.Windows.Media;

namespace PitchPerfectHearingTest.test.Models
{
    [TestClass]
    public class PointsConverterTests
    {
        [TestMethod]
        public void PointsConverter_Convert_StreamBoundsAreEmpty()
        {
            var obj = string.Empty;
            var converter = new PointsConverter();
            var stream = converter.Convert(obj, null, null, null);

            Assert.AreEqual(((StreamGeometry)stream).Bounds, Rect.Empty);
        }

        [TestMethod]
        public void PointsConverter_Convert_StreamBoundsAreNotEmpty()
        {
            var collection = new PointCollection() { new Point(1, 1) };
            var converter = new PointsConverter();
            var stream = converter.Convert(collection, null, null, null);

            Assert.AreNotEqual(((StreamGeometry)stream).Bounds, Rect.Empty);
        }
    }
}