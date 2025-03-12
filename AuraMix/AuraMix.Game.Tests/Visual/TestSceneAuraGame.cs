using osu.Framework.Allocation;
using osu.Framework.Platform;
using NUnit.Framework;

namespace Aura.Game.Tests.Visual
{
    [TestFixture]
    public partial class TestSceneAuraGame : AuraTestScene
    {
        // Add visual tests to ensure correct behaviour of your game: https://github.com/ppy/osu-framework/wiki/Development-and-Testing
        // You can make changes to classes associated with the tests and they will recompile and update immediately.

        private AuraMixMixGame mixMixGame;

        [BackgroundDependencyLoader]
        private void load(GameHost host)
        {
            mixMixGame = new AuraMixMixGame();
            mixMixGame.SetHost(host);

            AddGame(mixMixGame);
        }
    }
}