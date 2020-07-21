using System.Drawing;

namespace AvGen
{
    public abstract class HashAvGeneratorBase : AvGeneratorBase
    {
        public HashType HashType { get; }

        public HashAvGeneratorBase()
        {
            HashType = HashType.Sha512;
        }

        public HashAvGeneratorBase(Color background,
                               int width,
                               int widthInBlocks,
                               int margin,
                               HashType hashType = HashType.Sha512)
            : base(background, width, widthInBlocks, margin)
        {
            HashType = hashType;
        }

        public override AvTemplate GenerateTemplate(string source)
        {
            var hash = Hasher.ComputeHash(HashType, source);
            return new AvTemplate { Color = GetColor(hash), Template = GenerateTemplate(hash) };
        }

        protected virtual Color GetColor(byte[] hash) => Color.FromArgb(hash[0], hash[1], hash[2]);

        protected abstract bool[,] GenerateTemplate(byte[] hash);
    }
}
