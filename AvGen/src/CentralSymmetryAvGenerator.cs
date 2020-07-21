using System;
using System.Drawing;

namespace AvGen
{
    public class CentralSymmetryAvGenerator : HashAvGeneratorBase
    {
        private readonly int _quarter;

        public CentralSymmetryAvGenerator(Color background,
                                          int width,
                                          int widthInBlocks,
                                          int margin,
                                          HashType hashType = HashType.Sha512)
            : base(background, width, widthInBlocks, margin, hashType)
        {
            _quarter = (WidthInBlocks / 2) + (WidthInBlocks % 2);
            var hashLength = Hasher.GetLength(hashType);
            if (hashLength < _quarter * _quarter)
            {
                throw new ArgumentOutOfRangeException(nameof(widthInBlocks),
                    widthInBlocks,
                    $"((WidthInBlocks / 2) + (WidthInBlocks % 2)) ^ 2 must be less that hash length ({hashLength}).");
            }
        }

        protected override bool[,] GenerateTemplate(byte[] hash)
        {
            var template = new bool[WidthInBlocks, WidthInBlocks];
            for (var i = 0; i < _quarter; i++)
            {
                for (var j = 0; j < _quarter; j++)
                {
                    var cell = GetTemplateCell(hash, i, j);
                    template[i, j] = cell;
                    template[i, WidthInBlocks - j - 1] = cell;
                    template[WidthInBlocks - i - 1, j] = cell;
                    template[WidthInBlocks - i - 1, WidthInBlocks - j - 1] = cell;
                }
            }

            return template;
        }

        private bool GetTemplateCell(byte[] hash, int i, int j) => hash[(i * _quarter) + j] > 127;
    }
}
