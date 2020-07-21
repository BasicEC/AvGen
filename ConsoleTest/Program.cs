using System.Drawing.Imaging;
using AvGen;

namespace ConsoleTest
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            const string source = "basicec";
            var avGen = new AvGenerator();
            var image = avGen.Generate(source);
            image.Save($"{source}.png", ImageFormat.Png);
        }
    }
}
