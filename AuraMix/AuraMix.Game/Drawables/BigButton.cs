using osu.Framework.Allocation;
using osu.Framework.Audio.Sample;
using osu.Framework.Bindables;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osuTK;
using osuTK.Graphics;

namespace Aura.Game.Drawables;

public partial class BigButton(string initialText, bool initialDisabled, bool initialToggled, Color4 initialColor, IconUsage initialIcon) : CompositeDrawable
{
    private const int width = 72;
    private const int height = 60;
    private const int button_height = 40;
    private const int text_height = 20;

    public Bindable<string> Text = new Bindable<string>(initialText);
    public readonly BindableBool Toggled = new BindableBool(initialToggled);
    public readonly BindableBool Disabled = new BindableBool(initialDisabled);
    public readonly BindableBool Hovered = new BindableBool(false);
    public readonly Bindable<Color4> Color = new Bindable<Color4>(initialColor);
    public readonly Bindable<IconUsage> Icon = new Bindable<IconUsage>(initialIcon);

    private Box buttonBackground;
    private SpriteText buttonText;
    private Container buttonContainer;
    private SpriteIcon buttonIcon;

    private SoundSample hoverSample;
    private SoundSample checkOnSample;
    private SoundSample checkOffSample;

    [BackgroundDependencyLoader]
    private void load(ISampleStore sampleStore)
    {
        hoverSample = new SoundSample(sampleStore.Get("UI/button-hover"));
        checkOnSample = new SoundSample(sampleStore.Get("UI/check-on"));
        checkOffSample = new SoundSample(sampleStore.Get("UI/check-off"));

        Width = width;
        Height = height;

        InternalChild = new Container
        {
            RelativeSizeAxes = Axes.Both,
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            Masking = true,
            CornerRadius = 7,
            Children =
            [
                new Box()
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Colour = Colors.Overlay,
                    RelativeSizeAxes = Axes.Both
                },
                new FillFlowContainer()
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Direction = FillDirection.Vertical,
                    Children =
                    [
                        new Container()
                        {
                            RelativeSizeAxes = Axes.X,
                            Height = button_height,
                            Children =
                            [
                                buttonContainer = new Container()
                                {
                                    RelativeSizeAxes = Axes.Both,
                                    Anchor = Anchor.TopCentre,
                                    Origin = Anchor.TopCentre,
                                    Masking = true,
                                    CornerRadius = 7,
                                    Children =
                                    [
                                        buttonBackground = new Box
                                        {
                                            RelativeSizeAxes = Axes.Both,
                                            Colour = Colors.Mint
                                        },
                                        buttonIcon = new SpriteIcon()
                                        {
                                            Anchor = Anchor.Centre,
                                            Origin = Anchor.Centre,
                                            Icon = FontAwesome.Solid.Play,
                                            RelativeSizeAxes = Axes.Both,
                                            Colour = Colors.Dark,
                                            Scale = new Vector2(0.6f),
                                        },
                                    ]
                                }
                            ]
                        },
                        new Container()
                        {
                            RelativeSizeAxes = Axes.X,
                            Height = text_height,
                            Children =
                            [
                                buttonText = new SpriteText
                                {
                                    Origin = Anchor.Centre,
                                    Anchor = Anchor.Centre,
                                    Scale = new Vector2(0.8f),
                                    Text = "Play",
                                    Colour = Colors.Text,
                                    Margin = new MarginPadding() { Bottom = 2 }
                                }
                            ]
                        },
                    ]
                },
            ]
        };
    }

    protected override bool OnMouseDown(MouseDownEvent e)
    {
        if (Disabled.Value) return false;
        buttonContainer.ScaleTo(new Vector2(0.9f), 1000, Easing.OutQuint);
        return true;
    }

    protected override void OnMouseUp(MouseUpEvent e)
    {
        if (Disabled.Value) return;
        if (Toggled.Value) checkOffSample.Play();
        else checkOnSample.Play();

        // Toggled.Toggle();
        buttonContainer.ScaleTo(new Vector2(1.0f), 100, Easing.OutQuint);
        checkOffSample.Play();
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

    protected override void LoadComplete()
    {
        base.LoadComplete();
        Toggled.BindValueChanged(_ => updateState());
        Hovered.BindValueChanged(_ => updateState());
        Disabled.BindValueChanged(_ => updateState());
        Text.BindValueChanged(_ => updateState());
        Color.BindValueChanged(_ => updateState());
        Icon.BindValueChanged(_ => updateState());
        updateState();
    }

    private void updateState()
    {
        this.FadeTo(Disabled.Value ? 0.5f : 1f, 100, Easing.OutQuint);
        var newColor = getBackgroundColor(Hovered.Value, Toggled.Value);
        buttonBackground.Colour = newColor;
        buttonIcon.Icon = Icon.Value;
        buttonText.Text = Text.Value;
    }

    private Color4 getBackgroundColor(bool hovered, bool toggled)
    {
        // var color = toggled ? Color.Value.Lighten(0.35f) : Color.Value;
        var color = Color.Value;
        if (hovered) color = color.Lighten(0.1f);
        return color;
    }
}
