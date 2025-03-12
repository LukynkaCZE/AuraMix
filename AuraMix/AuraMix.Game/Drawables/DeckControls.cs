using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osuTK;
using osuTK.Graphics;

namespace Aura.Game.Drawables;

public partial class DeckControls : FillFlowContainer
{
    public ToggleButton Quantize;
    public ToggleButton MasterTempo;
    public ToggleButton Slip;

    [BackgroundDependencyLoader]
    private void load()
    {
        Direction = FillDirection.Vertical;
        AutoSizeAxes = Axes.Both;
        Spacing = new Vector2(10f);

        InternalChildren =
        [
            new FillFlowContainer<ToggleButton>
            {
                Direction = FillDirection.Horizontal,
                Spacing = new Vector2(2, 0),
                AutoSizeAxes = Axes.Both,
                Children =
                [
                    Quantize = new ToggleButton(false, "Q", false)
                    {
                        Origin = Anchor.Centre,
                        Anchor = Anchor.Centre,
                    },
                    MasterTempo = new ToggleButton(true, "MT", false)
                    {
                        Origin = Anchor.Centre,
                        Anchor = Anchor.Centre,
                    },
                    Slip = new ToggleButton(true, "Slip", false)
                    {
                        Origin = Anchor.Centre,
                        Anchor = Anchor.Centre,
                    }
                ]
            },

            new Container()
            {
                RelativeSizeAxes = Axes.X,
                AutoSizeAxes = Axes.Y,
                Children =
                [
                    new FillFlowContainer<BigButton>
                    {
                        Direction = FillDirection.Horizontal,
                        AutoSizeAxes = Axes.Both,
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Spacing = new Vector2(10f),
                        Children =
                        [
                            new BigButton("PLAY", false, false, Colors.Mint, FontAwesome.Solid.Play)
                            {
                                Anchor = Anchor.Centre,
                                Origin = Anchor.CentreLeft,
                            },
                            new BigButton("CUE", false, false, Color4.Yellow, FontAwesome.Solid.FastBackward)
                            {
                                Anchor = Anchor.Centre,
                                Origin = Anchor.CentreRight,
                            },
                        ]
                    }
                ]
            },
        ];
    }
}
