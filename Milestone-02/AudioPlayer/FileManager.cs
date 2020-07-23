using System.IO;

namespace AudioPlayer
{
    internal static class FileManager
    {
        public static void CreateFile(byte[] content)
        {
            File.WriteAllBytes(Constants.AudioFileName, content);
        }
    }
}
