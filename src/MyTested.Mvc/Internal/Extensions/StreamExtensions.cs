namespace MyTested.Mvc.Internal.Extensions
{
    using System.IO;

    public static class StreamExtensions
    {
        public static byte[] ToByteArray(this Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public static void Restart(this Stream stream)
        {
            stream.Position = 0;
        }
    }
}
