using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;

namespace Aura.Game.Drawables;

public partial class LayerOverview : CompositeDrawable
{
    private Box background;
    private Box side;
    private EditableText spriteText;

    public ToggleButton Solo;
    public ToggleButton Enabled;

    public Bindable<string> Text = new Bindable<string>("Layer");

    private const int width = 209;
    private const int height = 73;

    [BackgroundDependencyLoader]
    private void load()
    {
        Text.ValueChanged += @event =>
        {
            spriteText.Text.Value = @event.NewValue;
        };

        Width = width;
        Height = height;
        InternalChildren = new[]
        {
            new Container()
            {
                RelativeSizeAxes = Axes.Both,
                Masking = true,
                CornerRadius = 7,
                Children = new Drawable[]
                {
                    new FillFlowContainer()
                    {
                        RelativeSizeAxes = Axes.Both,
                        Direction = FillDirection.Horizontal,

                        Children = new Drawable[]
                        {
                            new Container()
                            {
                                RelativeSizeAxes = Axes.Y,
                                Width = 10,
                                Masking = true,
                                CornerRadius = 6,

                                Children = new Drawable[]
                                {
                                    new Box()
                                    {
                                        RelativeSizeAxes = Axes.Both,
                                        Colour = Colors.Overlay
                                    },
                                    side = new Box()
                                    {
                                        RelativeSizeAxes = Axes.Both,
                                        Colour = Colors.Mint
                                    },
                                }
                            },

                            background = new Box
                            {
                                RelativeSizeAxes = Axes.Both,
                                Colour = Colors.Overlay
                            }
                        }
                    },
                    new FillFlowContainer()
                    {
                        RelativeSizeAxes = Axes.Both,
                        Direction = FillDirection.Vertical,
                        Margin = new MarginPadding { Left = 15, Top = 5 },
                        Children = new Drawable[]
                        {
                            spriteText = new EditableText(Text.Value)
                            {
                            },
                        }
                    }
                }
            }
        };
    }
}
