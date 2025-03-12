using osu.Framework.Testing;

namespace Aura.Game.Tests.Visual
{
    public partial class AuraTestScene : TestScene
    {
        protected override ITestSceneTestRunner CreateRunner() => new AuraMixTestSceneTestRunner();

        private partial class AuraMixTestSceneTestRunner : AuraMixGameBase, ITestSceneTestRunner
        {
            private TestSceneTestRunner.TestRunner runner;

            protected override void LoadAsyncComplete()
            {
                base.LoadAsyncComplete();
                Add(runner = new TestSceneTestRunner.TestRunner());
            }

            public void RunTestBlocking(TestScene test) => runner.RunTestBlocking(test);
        }
    }
}