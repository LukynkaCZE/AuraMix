using System;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK;
using osuTK.Graphics;

namespace Aura.Game.Drawables;

public partial class BpmDisplay : CompositeDrawable
{

    public readonly Bindable<double> Bpm = new Bindable<double>(0.0);
    public readonly Bindable<double> Offset = new Bindable<double>(0.0);
    public readonly Bindable<int> OffsetType = new Bindable<int>(16);

    private SpriteText bpmText;
    private SpriteText offsetText;
    private SpriteText offsetTypeText;

    private const int width = 157;
    private const int height = 54;

    private const int bpm_text_height = 27;

    private const int small_text_width = 76;
    private const int small_text_height = 18;

    [BackgroundDependencyLoader]
    private void load()
    {
        Width = width;
        Height = height;

        InternalChildren =
        [
            new Container()
            {
                RelativeSizeAxes = Axes.Both,
                Masking = true,
                CornerRadius = 7,
                BorderColour = Color4.White.Opacity(0.10f),
                BorderThickness = 1,
                Child = new Box()
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Colors.Mantle,
                }
            },

            new FillFlowContainer
            {
                RelativeSizeAxes = Axes.Both,
                Direction = FillDirection.Vertical,
                Children =
                [
                    new Container()
                    {
                        RelativeSizeAxes = Axes.X,
                        Height = bpm_text_height,
                        Child = bpmText = new SpriteText
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Text = "",
                            Colour = Colors.Text
                        }
                    },
                    new Container()
                    {
                        RelativeSizeAxes = Axes.X,
                        Height = small_text_height,
                        Child = new FillFlowContainer()
                        {
                            RelativeSizeAxes = Axes.Both,
                            Direction = FillDirection.Horizontal,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Children =
                            [
                                new Container()
                                {
                                    AutoSizeAxes = Axes.Y,
                                    Width = small_text_width,
                                    Child = offsetText = new SpriteText()
                                    {
                                        Anchor = Anchor.Centre,
                                        Origin = Anchor.Centre,
                                        Text = "",
                                        Scale = new Vector2(0.8f),
                                        Colour = Colors.Text,
                                    },
                                },

                                new Container()
                                {
                                    AutoSizeAxes = Axes.Y,
                                    Width = small_text_width,
                                    Child = offsetTypeText = new SpriteText()
                                    {
                                        Anchor = Anchor.Centre,
                                        Origin = Anchor.Centre,
                                        Text = "",
                                        Scale = new Vector2(0.8f),
                                        Colour = Colors.Text,
                                    },
                                },
                            ]
                        }
                    }
                ]
            },
        ];
    }

    protected override void LoadComplete()
    {
        base.LoadComplete();

        Bpm.BindValueChanged(v => bpmText.Text = $"{v.NewValue} BPM", true);
        Offset.BindValueChanged(v => offsetText.Text = $"{v.NewValue}%", true);
        OffsetType.BindValueChanged(v => offsetTypeText.Text = $"\u00b1{v.NewValue}", true);
    }
}
