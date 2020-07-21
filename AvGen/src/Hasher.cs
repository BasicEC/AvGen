using System;
using System.Security.Cryptography;
using System.Text;

namespace AvGen
{
    /// <summary>
    /// It is a helper class. It provides some inheritors of a <see cref="HashAlgorithm"/> class
    /// and method to compute specified hash.
    /// </summary>
    public static class Hasher
    {
        private const string UnknownHashTypeMessage = "Unknown hash type.";

        public static SHA1 Sha1 => LazySha1.Value;

        public static SHA256 Sha256 => LazySha256.Value;

        public static SHA384 Sha384 => LazySha384.Value;

        public static SHA512 Sha512 => LazySha512.Value;

        private static Lazy<SHA1> LazySha1 { get; } = new Lazy<SHA1>(SHA1.Create);

        private static Lazy<SHA256> LazySha256 { get; } = new Lazy<SHA256>(SHA256.Create);

        private static Lazy<SHA384> LazySha384 { get; } = new Lazy<SHA384>(SHA384.Create);

        private static Lazy<SHA512> LazySha512 { get; } = new Lazy<SHA512>(SHA512.Create);

        public static byte[] ComputeHash(HashType type, string source)
        {
            var buffer = Encoding.UTF8.GetBytes(source);
            HashAlgorithm algorithm = type switch
            {
                HashType.Sha1 => Sha1,
                HashType.Sha256 => Sha256,
                HashType.Sha384 => Sha384,
                HashType.Sha512 => Sha512,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, UnknownHashTypeMessage)
            };

            return algorithm.ComputeHash(buffer);
        }

        public static int GetLength(HashType type) => type switch
        {
            HashType.Sha1 => 20,
            HashType.Sha256 => 32,
            HashType.Sha384 => 48,
            HashType.Sha512 => 64,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, UnknownHashTypeMessage)
        };
    }
}
