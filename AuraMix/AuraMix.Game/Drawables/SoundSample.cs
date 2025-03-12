using osu.Framework.Audio.Sample;
using osu.Framework.Logging;

namespace Aura.Game.Drawables;

public class SoundSample(Sample sample)
{
    public void Play()
    {
        if (sample == null)
        {
            Logger.Log("sample is null");
            return;
        }
        sample.Volume.Value = 0.10f;
        sample.Play();
    }
}
