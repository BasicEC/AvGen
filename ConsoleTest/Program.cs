using System;
using System.Drawing;
using System.Drawing.Imaging;
using AvGen;

namespace ConsoleTest
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            const string first = "gallowsCalibrator";
            const string second = "carcinoGeneticist";

            var generator = new VerticalSymmetryAvGenerator(Color.WhiteSmoke, 600, 10, 50);

            var fTemplate = generator.GenerateTemplate(first);
            var sTemplate = generator.GenerateTemplate(second);
            var fImage = generator.CreateBitmap(fTemplate);
            var sImage = generator.CreateBitmap(sTemplate);

            fImage.Save($"{first}.png", ImageFormat.Png);
            sImage.Save($"{second}.png", ImageFormat.Png);

            var colorDistance = Distance(fTemplate.Color, sTemplate.Color);
            var colorPercent = colorDistance / Math.Sqrt(255 * 255 * 3) * 100;
            var templateDistance = Distance(fTemplate.Template, sTemplate.Template);
            var templatePercent = templateDistance / fTemplate.Template.Length * 100;

            Console.WriteLine($"First Value:\t{first};\tColor: #{fTemplate.Color.R:X2}{fTemplate.Color.G:X2}{fTemplate.Color.B:X2};\n" +
                              $"Second Value:\t{second};\tColor: #{sTemplate.Color.R:X2}{sTemplate.Color.G:X2}{sTemplate.Color.B:X2};\n\n" +
                              $"Max Color Distance:\t{Math.Sqrt(255 * 255 * 3):F2};\n" +
                              $"Color Distance:\t\t{colorDistance:F2};\n" +
                              $"Color Match:\t\t{100 - colorPercent:F2}%;\n\n" +
                              $"Max Template Distance:\t{fTemplate.Template.Length};\n" +
                              $"Template Distance:\t{templateDistance};\n" +
                              $"Template Match:\t\t{100 - templatePercent:F2}%;\n");
        }

        public static double Distance(Color first, Color second)
        {
            var r = first.R - second.R;
            var g = first.G - second.G;
            var b = first.B - second.B;
            return Math.Sqrt((r * r) + (g * g) + (b * b));
        }

        public static double Distance(bool[,] first, bool[,] second)
        {
            var result = 0d;
            for (var i = 0; i < first.GetLength(0); i++)
            {
                for (var j = 0; j < first.GetLength(1); j++)
                {
                    result += first[i, j] == second[i, j] ? 0 : 1;
                }
            }

            return result;
        }
    }
}
