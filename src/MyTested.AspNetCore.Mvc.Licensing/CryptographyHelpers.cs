#if NETSTANDARD1_4
namespace MyTested.AspNetCore.Mvc.Licensing
{
    using System;
    using System.IO;
    using System.Security.Cryptography;

    public static class CryptographyHelpers
    {
        private const int ALG_TYPE_RSA = (2 << 9);
        private const int ALG_CLASS_KEY_EXCHANGE = (5 << 13);
        private const int CALG_RSA_KEYX = (ALG_CLASS_KEY_EXCHANGE | ALG_TYPE_RSA | 0);

        internal static RSAParameters ToRSAParameters(this byte[] cspBlob, bool includePrivateParameters)
        {
            BinaryReader br = new BinaryReader(new MemoryStream(cspBlob));

            byte bType = br.ReadByte(); // BLOBHEADER.bType: Expected to be 0x6 (PUBLICKEYBLOB) or 0x7 (PRIVATEKEYBLOB), though there's no check for backward compat reasons. 
            byte bVersion = br.ReadByte(); // BLOBHEADER.bVersion: Expected to be 0x2, though there's no check for backward compat reasons.
            br.ReadUInt16(); // BLOBHEADER.wReserved
            int algId = br.ReadInt32(); // BLOBHEADER.aiKeyAlg
            if (algId != CALG_RSA_KEYX)
                throw new PlatformNotSupportedException(); // The FCall this code was ported from supports other algid's but we're only porting what we use.

            int magic = br.ReadInt32(); // RSAPubKey.magic: Expected to be 0x31415352 ('RSA1') or 0x32415352 ('RSA2') 
            int bitLen = br.ReadInt32(); // RSAPubKey.bitLen

            int modulusLength = bitLen / 8;
            int halfModulusLength = (modulusLength + 1) / 2;

            uint expAsDword = br.ReadUInt32();

            RSAParameters rsaParameters = new RSAParameters();
            rsaParameters.Exponent = ExponentAsBytes(expAsDword);
            rsaParameters.Modulus = br.ReadReversed(modulusLength);
            if (includePrivateParameters)
            {
                rsaParameters.P = br.ReadReversed(halfModulusLength);
                rsaParameters.Q = br.ReadReversed(halfModulusLength);
                rsaParameters.DP = br.ReadReversed(halfModulusLength);
                rsaParameters.DQ = br.ReadReversed(halfModulusLength);
                rsaParameters.InverseQ = br.ReadReversed(halfModulusLength);
                rsaParameters.D = br.ReadReversed(modulusLength);
            }

            return rsaParameters;
        }

        /// <summary>
        /// Helper for converting a UInt32 exponent to bytes.
        /// </summary>
        private static byte[] ExponentAsBytes(uint exponent)
        {
            if (exponent <= 0xFF)
            {
                return new[] { (byte)exponent };
            }
            if (exponent <= 0xFFFF)
            {
                return new[]
                {
                    (byte) (exponent >> 8),
                    (byte) (exponent)
                };
            }
            if (exponent <= 0xFFFFFF)
            {
                return new[]
                {
                    (byte) (exponent >> 16),
                    (byte) (exponent >> 8),
                    (byte) (exponent)
                };
            }

            return new[]
            {
                (byte) (exponent >> 24),
                (byte) (exponent >> 16),
                (byte) (exponent >> 8),
                (byte) (exponent)
            };
        }

        /// <summary>
        /// Read in a byte array in reverse order.
        /// </summary>
        private static byte[] ReadReversed(this BinaryReader br, int count)
        {
            byte[] data = br.ReadBytes(count);
            Array.Reverse(data);
            return data;
        }
    }
}
#endif