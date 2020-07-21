using System.Drawing;

namespace AvGen
{
    public class AvTemplate
    {
        public Color Color { get; set; }

        public bool[,] Template { get; set; }

        public bool this[int i, int j] => Template[i, j];
    }
}
