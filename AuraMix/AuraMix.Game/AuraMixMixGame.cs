using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Input;
using osu.Framework.Screens;

namespace Aura.Game
{
    public partial class AuraMixMixGame : AuraMixGameBase
    {
        private ScreenStack screenStack;
        public static IFocusManager FocusManager;

        [BackgroundDependencyLoader]
        private void load()
        {
            Child = screenStack = new ScreenStack { RelativeSizeAxes = Axes.Both };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            FocusManager = GetContainingFocusManager();

            screenStack.Push(new MainScreen());
        }
    }
}
