using System.Drawing;

namespace TAGL.OpenGlWindow;

public static class GraphicsHelper
{
    public static Color ToColor(int r, int g, int b, float a = 1.0f)
    {
        r = Math.Clamp(r, 0, 255);
        g = Math.Clamp(g, 0, 255);
        b = Math.Clamp(b, 0, 255);
        a = Math.Clamp(a, 0f, 1f);

        int alpha = (int)(a * 255f);

        return Color.FromArgb(alpha, r, g, b);
    }
}
