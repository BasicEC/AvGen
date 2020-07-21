using System.Drawing;
using System.Drawing.Imaging;
using AvGen;

namespace ConsoleTest
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            const string source = "basicec";
            var v = new VerticalSymmetryAvGenerator(Color.WhiteSmoke, 600, 10, 50);
            var c = new CentralSymmetryAvGenerator(Color.WhiteSmoke, 600, 16, 50);
            var imageV = v.Generate(source);
            var imageC = c.Generate(source);
            imageV.Save($"{source}_V.png", ImageFormat.Png);
            imageC.Save($"{source}_C.png", ImageFormat.Png);
        }
    }
}
