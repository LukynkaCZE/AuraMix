using osu.Framework.Platform;
using osu.Framework;
using Aura.Game;

namespace Aura.Desktop
{
    public static class Program
    {
        public static void Main()
        {
            using (GameHost host = Host.GetSuitableDesktopHost(@"Aura"))
            using (osu.Framework.Game game = new AuraMixMixGame())
                host.Run(game);
        }
    }
}