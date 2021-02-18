using NetCoreAudio;
using System.Threading.Tasks;

namespace AudioPlayer
{
    internal static class AudioManager
    {
        private static readonly Player player = new Player();

        public static async Task PlayAudio()
        {
            await player.Play(Constants.AudioFileName);
        }
    }
}
