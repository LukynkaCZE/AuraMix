using osu.Framework.Allocation;
using osu.Framework.Audio.Sample;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osuTK;
using osuTK.Graphics;

namespace Aura.Game.Drawables;

public partial class ToggleButton(bool toggledByDefault, string name, bool disabledByDefault): CompositeDrawable
{
    private Box background;
    private SpriteText spriteText;
    private Container mainContainer;

    public Bindable<string> Text = new Bindable<string>(name);
    public readonly BindableBool Toggled = new BindableBool(toggledByDefault);
    public readonly BindableBool Disabled = new BindableBool(disabledByDefault);
    public readonly BindableBool Hovered = new BindableBool(false);

    private const int width = 50;
    private const int height = 20;

    private SoundSample hoverSample;
    private SoundSample checkOnSample;
    private SoundSample checkOffSample;

    [BackgroundDependencyLoader]
    private void load(ISampleStore sampleStore)
    {
        hoverSample = new SoundSample(sampleStore.Get("UI/button-hover"));
        checkOnSample = new SoundSample(sampleStore.Get("UI/check-on"));
        checkOffSample = new SoundSample(sampleStore.Get("UI/check-off"));

        Text.BindValueChanged(text => spriteText.Text = text.NewValue);
        Toggled.BindValueChanged(_ => updateState());
        Hovered.BindValueChanged(_ => updateState());
        Disabled.BindValueChanged(_ => updateState());

        Width = width;
        Height = height;
        InternalChildren = new Drawable[]
        {
            mainContainer = new Container()
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
                Masking = true,
                CornerRadius = 7,
                Children = new Drawable[]
                {
                    background = new Box()
                    {
                        RelativeSizeAxes = Axes.Both,
                        Colour = Colors.Mint
                    },
                    spriteText = new SpriteText()
                    {
                        Text = Text.Value,
                        Font = new FontUsage("Torus-Regular", 13),
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Margin = new MarginPadding() { Bottom = 2 }
                    }
                }
            }
        };
        updateState();
    }

    protected override bool OnHover(HoverEvent e)
    {
        Hovered.Value = true;
        hoverSample.Play();
        return base.OnHover(e);
    }

    protected override void OnHoverLost(HoverLostEvent e)
    {
        Hovered.Value = false;
        base.OnHoverLost(e);
    }

    protected override bool OnMouseDown(MouseDownEvent e)
    {
        if (Disabled.Value) return false;
        mainContainer.ScaleTo(new Vector2(0.9f), 1000, Easing.OutQuint);
        return true;
    }

    protected override void OnMouseUp(MouseUpEvent e)
    {
        if (Disabled.Value) return;
        if (Toggled.Value) checkOffSample.Play(); else checkOnSample.Play();

        Toggled.Toggle();
        mainContainer.ScaleTo(new Vector2(1.0f), 100, Easing.OutQuint);
        checkOffSample.Play();
    }

    private Color4 getBackgroundColor(bool hovered, bool toggled)
    {
        var color = toggled ? Colors.Overlay : Colors.Mint;
        if (hovered) color = color.Lighten(0.1f);
        return color;
    }

    private void updateState()
    {
        this.FadeTo(Disabled.Value ? 0.5f : 1f, 100, Easing.OutQuint);

        var newColor = getBackgroundColor(Hovered.Value, Toggled.Value);
        background.FadeColour(newColor, 100, Easing.OutCubic);
        spriteText.FadeColour(Toggled.Value ? Colors.Text : Color4.Black, 500, Easing.OutQuint);
    }
}
