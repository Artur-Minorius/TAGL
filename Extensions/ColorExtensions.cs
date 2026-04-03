using System.Drawing;
using System.Numerics;

namespace TAGL.Extensions;

public static class ColorExtensions
{
    public static Vector3 ToVector(this Color c) =>
        new(c.R / 255f, c.G / 255f, c.B / 255f);
}
