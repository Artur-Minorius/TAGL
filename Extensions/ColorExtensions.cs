using System.Drawing;
using System.Numerics;

namespace TAGL.Extensions;

public static class ColorExtensions
{
    public static Vector3 ToVector(this Color color)
    {
        var vector = Vector3.Zero;

        vector.X = color.R;
        vector.Y = color.G;
        vector.Z = color.B;

        return Vector3.Normalize(vector);
    }
}
