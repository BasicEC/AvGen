using System;
using System.Drawing;

namespace AvGen
{
    public class VerticalSymmetryAvGenerator : HashAvGeneratorBase
    {
        public VerticalSymmetryAvGenerator(Color background,
                                           int width,
                                           int widthInBlocks,
                                           int margin,
                                           HashType hashType = HashType.Sha512)
            : base(background, width, widthInBlocks, margin, hashType)
        {
            var hashLength = Hasher.GetLength(hashType);
            if (hashLength < WidthInBlocks * ((WidthInBlocks / 2) + (WidthInBlocks % 2)))
            {
                throw new ArgumentOutOfRangeException(nameof(widthInBlocks),
                    widthInBlocks,
                    $"WidthInBlocks * ((WidthInBlocks / 2) + (WidthInBlocks % 2)) must be less that hash length ({hashLength}).");
            }
        }

        protected override bool[,] GenerateTemplate(byte[] hash)
        {
            var template = new bool[WidthInBlocks, WidthInBlocks];
            var height = WidthInBlocks;
            var width = (WidthInBlocks / 2) + (WidthInBlocks % 2);

            for (var i = 0; i < height; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    var cell = GetTemplateCell(hash, i, j);
                    template[i, j] = cell;
                    template[i, WidthInBlocks - j - 1] = cell;
                }
            }

            return template;
        }

        private bool GetTemplateCell(byte[] hash, int i, int j) => hash[(j * WidthInBlocks) + i] > 127;
    }
}
