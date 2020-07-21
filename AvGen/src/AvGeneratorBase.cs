using System;
using System.Drawing;

namespace AvGen
{
    public abstract class AvGeneratorBase
    {
        public int Width { get; }

        public int WidthInBlocks { get; }

        public int Margin { get; }

        public Color Background { get; }

        protected AvGeneratorBase()
            : this(Color.WhiteSmoke, 600, 5, 50)
        {
        }

        protected AvGeneratorBase(Color background, int width, int widthInBlocks, int margin)
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
            Background = background;
        }

        public Bitmap Generate(string source) => CreateBitmap(GenerateTemplate(source));

        public abstract AvTemplate GenerateTemplate(string source);

        public Bitmap CreateBitmap(AvTemplate template)
        {
            var bitmap = new Bitmap(Width, Width);
            var leftMargin = Margin;
            var rightMargin = Width - Margin;
            var blockWidth = (Width - (2 * Margin)) / (double)WidthInBlocks;

            for (var i = 0; i < Width; i++)
            {
                for (var j = 0; j < Width; j++)
                {
                    if (i < leftMargin || i >= rightMargin || j < leftMargin || j >= rightMargin)
                    {
                        bitmap.SetPixel(j, i, Background);
                        continue;
                    }

                    var pixel = template[(int)((i - Margin) / blockWidth), (int)((j - Margin) / blockWidth)]
                        ? template.Color
                        : Background;
                    bitmap.SetPixel(j, i, pixel);
                }
            }

            return bitmap;
        }
    }
}
