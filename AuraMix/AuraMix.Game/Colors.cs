using osu.Framework.Graphics;

namespace Aura.Game;

public static class Colors
{
    public static Colour4 Background = FromHex("#1D1D26");
    public static Colour4 Slate = FromHex("#23232F");
    public static Colour4 Overlay = FromHex("#2C2C3B");
    public static Colour4 Text = FromHex("#D9D5EC");
    public static Colour4 Mint = FromHex("#7EDE93");
    public static Colour4 Mantle = FromHex("#111118");
    public static Colour4 Highlight = FromHex("#625C7D");
    public static Colour4 Dark = FromHex("#1D1D26");



    public static Colour4 FromHex(string hex)
    {
        return Colour4.FromHex(hex);
    }

}
