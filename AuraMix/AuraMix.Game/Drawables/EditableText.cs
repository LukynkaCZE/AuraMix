using osu.Framework.Allocation;
using osu.Framework.Audio.Sample;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input;
using osu.Framework.Input.Events;
using osu.Framework.Logging;

namespace Aura.Game.Drawables;

public partial class EditableText(string defaultText): CompositeDrawable
{

    private Box editingBackground;
    private SpriteText spriteText;
    private Container mainContainer;
    private BasicTextBox textBox;

    public Bindable<string> Text = new Bindable<string>(defaultText);
    public BindableBool Editing = new BindableBool(false);

    private const int width = 180;
    private const int height = 25;


    [BackgroundDependencyLoader]
    private void load(ISampleStore sampleStore)
    {
        Width = width;
        Height = height;

        Text.ValueChanged += @event =>
        {
            spriteText.Text = @event.NewValue;
        };

        Editing.ValueChanged += @event =>
        {
            updateState();
        };



        InternalChildren = new Drawable[]
        {

            spriteText = new SpriteText()
            {
                RelativeSizeAxes = Axes.Both,
                Font = new FontUsage("Torus-Regular", 30),
                Text = Text.Value,
                Colour = Colors.Text,
            },
            textBox = new BasicTextBox()
            {
                RelativeSizeAxes = Axes.Both,
                Alpha = 0f
            }
        };

        textBox.CommitOnFocusLost = true;
        textBox.FadeOut(500, Easing.OutQuart);
        textBox.OnCommit += (sender, text) =>
        {
            Editing.Toggle();
            Text.Value = sender.Text;
        };
    }

    protected override bool OnClick(ClickEvent e)
    {
        Editing.Toggle();
        return base.OnClick(e);
    }

    private void updateState()
    {
        if (Editing.Value)
        {
            spriteText.FadeOut(50, Easing.OutQuart);
            textBox.FadeIn(50, Easing.InQuart).Finally(_ =>
            {
                AuraMixMixGame.FocusManager.ChangeFocus(textBox);
            });
        }
        else
        {
            spriteText.FadeIn(100, Easing.InQuart);
            textBox.FadeOut(100, Easing.OutQuart);
            AuraMixMixGame.FocusManager.ChangeFocus(null);
        }
    }
}
