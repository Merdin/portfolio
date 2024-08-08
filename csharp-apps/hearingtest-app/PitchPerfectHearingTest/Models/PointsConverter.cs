using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace PitchPerfectHearingTest.Models
{
    public class PointsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var geometry = new StreamGeometry();
            var points = value as IEnumerable<Point>;

            if (points != null && points.Any())
            {
                using (var sgc = geometry.Open())
                {
                    foreach (var point in points)
                    {
                        sgc.BeginFigure(point, false, false);
                        sgc.LineTo(point, true, false);
                    }
                }
            }

            return geometry;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
