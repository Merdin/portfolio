using PitchPerfectHearingTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PitchPerfectHearingTest.Services
{
    public class AudiogramService
    {
        private Canvas _cnvAudiogram;
        private int _xOrigin = 30;
        private int _xOffset = 100;
        private double _xMax = (Math.Log10(10) * 100 * 3) + (Math.Log10(2) * 100) + 30;
        private int _yOrigin = 25;
        private int _yMax = 275;

        public AudiogramService()
        {
            _cnvAudiogram = new Canvas();
        }

        public PointCollection MapScore(List<SoundLevel> scores)
        {
            var mappedScores = new PointCollection();
            foreach (var level in scores)
            {
                var yPosition = ((int)level.Decibel) * 2.5 + 50;
                var xPosition = 0.0;
                var xOffset100 = (Math.Log10(10) * 100 + 30);
                var xOffset1000 = (Math.Log10(10) * 100 * 2 + 30);

                switch (level.Frequency)
                {
                    case Frequency.Hz250:
                        xPosition = (Math.Log10(2.5) * 100) + xOffset100;
                        break;
                    case Frequency.Hz500:
                        xPosition = (Math.Log10(5) * 100) + xOffset100;
                        break;
                    case Frequency.Hz1000:
                        xPosition = (Math.Log10(10) * 100) + xOffset100;
                        break;
                    case Frequency.Hz2000:
                        xPosition = (Math.Log10(2) * 100) + xOffset1000;
                        break;
                    case Frequency.Hz4000:
                        xPosition = (Math.Log10(4) * 100) + xOffset1000;
                        break;
                    case Frequency.Hz8000:
                        xPosition = (Math.Log10(8) * 100) + xOffset1000;
                        break;
                }

                if (mappedScores.Any())
                {
                    var added = false;
                    var index = 0;

                    while (!added)
                    {
                        if (index == mappedScores.Count || mappedScores[index].X > xPosition)
                        {
                            added = true;
                        }
                        else
                        {
                            index++;
                        }
                    }

                    mappedScores.Insert(index, new Point(xPosition, yPosition));
                }
                else
                {
                    mappedScores.Add(new Point(xPosition, yPosition));
                }
            }

            return mappedScores;
        }

        public Canvas GenerateAudiogram()
        {
            DrawXGrid();
            DrawYGrid();
            DrawAxis();
            DrawExplanation();
            SetBindingElements();
            _cnvAudiogram.Width = _xMax + 50;
            _cnvAudiogram.Height = _yMax + 50;

            return _cnvAudiogram;
        }

        private void DrawXGrid()
        {
            double drawingOffset = _xOrigin;

            for (int i = 1; i < 4; i++)
            {
                for (int j = 2; j < 11; j++)
                {
                    var xValue = Math.Log10(j) * _xOffset + drawingOffset;

                    _cnvAudiogram.Children.Add(new Line()
                    {
                        X1 = xValue,
                        X2 = xValue,
                        Y1 = _yOrigin,
                        Y2 = _yMax,
                        Stroke = Brushes.LightGray,
                        StrokeThickness = 1
                    });

                    if (j < 10)
                    {
                        _cnvAudiogram.Children.Add(new Line()
                        {
                            X1 = xValue,
                            X2 = xValue,
                            Y1 = _yOrigin - 3,
                            Y2 = _yOrigin + 3,
                            Stroke = Brushes.Black,
                            StrokeThickness = 1
                        });
                    }
                    else
                    {
                        _cnvAudiogram.Children.Add(new Line()
                        {
                            X1 = xValue,
                            X2 = xValue,
                            Y1 = _yOrigin - 6,
                            Y2 = _yOrigin + 6,
                            Stroke = Brushes.Black,
                            StrokeThickness = 1
                        });

                        var content = Math.Pow(10, i + 1) >= 1000 ? $"{Math.Pow(10, i + 1) / 1000}KHz" : $"{Math.Pow(10, i + 1)}Hz";
                        _cnvAudiogram.Children.Add(new Label()
                        {
                            Content = content,
                            Margin = new Thickness() { Left = xValue - 5, Top = 5 },
                            FontSize = 8,
                        });
                    }
                }

                drawingOffset = Math.Log10(10) * _xOffset * i + _xOrigin;
            }

            _cnvAudiogram.Children.Add(new Line()
            {
                X1 = _xMax,
                X2 = _xMax,
                Y1 = _yOrigin,
                Y2 = _yMax,
                Stroke = Brushes.LightGray,
                StrokeThickness = 1
            });

            _cnvAudiogram.Children.Add(new Line()
            {
                X1 = _xMax,
                X2 = _xMax,
                Y1 = _yOrigin - 3,
                Y2 = _yOrigin + 3,
                Stroke = Brushes.Black,
                StrokeThickness = 1
            });
        }

        private void DrawYGrid()
        {
            foreach (int decibel in Enum.GetValues(typeof(Decibel)))
            {
                var yValue = (int)(decibel * 2.5) + 50;

                _cnvAudiogram.Children.Add(new Line()
                {
                    X1 = _xOrigin,
                    X2 = _xMax,
                    Y1 = yValue,
                    Y2 = yValue,
                    Stroke = Brushes.LightGray,
                    StrokeThickness = 1
                });

                _cnvAudiogram.Children.Add(new Line()
                {
                    X1 = _xOrigin - 5,
                    X2 = _xOrigin + 5,
                    Y1 = yValue,
                    Y2 = yValue,
                    Stroke = Brushes.Black,
                    StrokeThickness = 1
                });

                _cnvAudiogram.Children.Add(new Label()
                {
                    Content = $"{decibel}dB",
                    Margin = new Thickness() { Top = yValue - 12 },
                    FontSize = 8
                });
            }
        }

        private void DrawAxis()
        {
            _cnvAudiogram.Children.Add(new Line()
            {
                X1 = _xOrigin,
                X2 = _xMax + 3,
                Y1 = _yOrigin,
                Y2 = _yOrigin,
                Stroke = Brushes.Black,
                StrokeThickness = 1
            });

            _cnvAudiogram.Children.Add(new Line()
            {
                X1 = _xOrigin,
                X2 = _xMax + 3,
                Y1 = _yMax,
                Y2 = _yMax,
                Stroke = Brushes.Black,
                StrokeThickness = 1
            });

            _cnvAudiogram.Children.Add(new Line()
            {
                X1 = _xOrigin,
                X2 = _xOrigin,
                Y1 = _yOrigin,
                Y2 = _yMax,
                Stroke = Brushes.Black,
                StrokeThickness = 1
            });

            _cnvAudiogram.Children.Add(new Line()
            {
                X1 = _xMax + 3,
                X2 = _xMax + 3,
                Y1 = _yOrigin,
                Y2 = _yMax,
                Stroke = Brushes.Black,
                StrokeThickness = 1
            });
        }

        private void DrawExplanation()
        {
            var normal = new Rectangle()
            {
                Height = 50,
                Width = _xMax - _xOrigin,
                Fill = new LinearGradientBrush(Colors.Green, Colors.White, 90) { Opacity = 0.4 }
            };

            var warning = new Rectangle()
            {
                Height = 150,
                Width = _xMax - _xOrigin,
                Fill = new LinearGradientBrush(Colors.White, Colors.Red, 90) { Opacity = 0.3 }
            };

            var normalLabel = new Label()
            {
                Content = "Normaal gehoor",
                Margin = new Thickness() { Left = _xMax / 2.3, Top = 60 },
                FontStyle = FontStyles.Italic,
                Foreground = Brushes.Gray
            };

            var warningLabel = new Label()
            {
                Content = "Beschadigd gehoor",
                Margin = new Thickness() { Left = _xMax / 2.4, Top = 200 },
                FontStyle = FontStyles.Italic,
                Foreground = Brushes.Gray
            };

            _cnvAudiogram.Children.Add(normal);
            Canvas.SetTop(normal, _yOrigin + 25);
            Canvas.SetLeft(normal, _xOrigin);

            _cnvAudiogram.Children.Add(warning);
            Canvas.SetTop(warning, _yOrigin + 100);
            Canvas.SetLeft(warning, _xOrigin);

            _cnvAudiogram.Children.Add(normalLabel);
            _cnvAudiogram.Children.Add(warningLabel);
        }

        private void SetBindingElements()
        {
            var line = new Polyline()
            {
                Stroke = Brushes.DarkRed,
                StrokeThickness = 1,
            };

            var lineBinding = new Binding("ResultMapping");
            line.SetBinding(Polyline.PointsProperty, lineBinding);

            var path = new Path()
            {
                Stroke = Brushes.DarkRed,
                StrokeThickness = 5,
                StrokeStartLineCap = PenLineCap.Round,
                StrokeEndLineCap = PenLineCap.Round
            };

            var dataBinding = new Binding("ResultMapping");
            dataBinding.Converter = new PointsConverter();
            path.SetBinding(Path.DataProperty, dataBinding);

            _cnvAudiogram.Children.Add(line);
            _cnvAudiogram.Children.Add(path);
        }
    }
}