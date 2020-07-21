using System;
using System.Drawing;

namespace AvGen
{
    public class AvGenerator
    {
        public int Width { get; }

        public int WidthInBlocks { get; }

        public int Margin { get; }

        public Color Background { get; }

        public HashType HashType { get; }

        public AvGenerator(int width = 600, int widthInBlocks = 5, int margin = 50, HashType hashType = HashType.Sha512)
            : this(Color.WhiteSmoke, width, widthInBlocks, margin, hashType)
        {
        }

        public AvGenerator(Color background,
                           int width = 600,
                           int widthInBlocks = 5,
                           int margin = 50,
                           HashType hashType = HashType.Sha512)
        {
            if (width <= 0)
                throw new ArgumentException("Must be greater than 0.", nameof(width));

            if (widthInBlocks <= 0)
                throw new ArgumentException("Must be greater than 0.", nameof(widthInBlocks));

            if (margin < 0)
                throw new ArgumentException("Cannot be less than 0.", nameof(margin));

            if (width < widthInBlocks)
                throw new ArgumentException($"{nameof(widthInBlocks)} cannot be greater than {nameof(width)}.", nameof(widthInBlocks));

            if (2 * margin > width - widthInBlocks)
                throw new ArgumentException($"2 * {nameof(margin)} cannot be greater than ({nameof(width)} - {nameof(widthInBlocks)}).", nameof(margin));

            Width = width;
            WidthInBlocks = widthInBlocks;
            Margin = margin;
            HashType = hashType;
            Background = background;
        }

        public Bitmap Generate(string source) => CreateBitmap(GenerateTemplate(source));

        public AvTemplate GenerateTemplate(string source)
        {
            var hash = Hasher.ComputeHash(HashType, source);
            var template = new bool[WidthInBlocks, WidthInBlocks];
            var height = WidthInBlocks;
            var width = (WidthInBlocks / 2) + (WidthInBlocks % 2);

            for (var i = 0; i < height; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    if (hash[hash.Length - i - j - 1] > 127)
                        continue;

                    template[i, j] = true;
                    template[i, WidthInBlocks - j - 1] = true;
                }
            }

            return new AvTemplate { Color = Color.FromArgb(hash[0], hash[1], hash[2]), Template = template };
        }

        public Bitmap CreateBitmap(AvTemplate template)
        {
            var bitmap = new Bitmap(Width, Width);
            var leftMargin = Margin;
            var rightMargin = Width - Margin;
            var blockWidth = (Width - (2 * Margin)) / WidthInBlocks;

            for (var i = 0; i < Width; i++)
            {
                for (var j = 0; j < Width; j++)
                {
                    if (i < leftMargin || i >= rightMargin || j < leftMargin || j >= rightMargin)
                    {
                        bitmap.SetPixel(j, i, Background);
                        continue;
                    }

                    var pixel = template[(i - Margin) / blockWidth, (j - Margin) / blockWidth] ? template.Color : Background;
                    bitmap.SetPixel(j, i, pixel);
                }
            }

            return bitmap;
        }
    }
}
