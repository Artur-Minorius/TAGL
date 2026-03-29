using System.Numerics;

namespace TAGL.Extensions;

public static class VectorExtesions
{
    public static Vector3 RotateAround(this Vector3 point, Vector3 pivot, Vector3 axis, float angle)
    {
        var rotation = Quaternion.CreateFromAxisAngle(Vector3.Normalize(axis), angle);
        return pivot + Vector3.Transform(point - pivot, rotation);
    }

    public static Vector3 RotateAround(this Vector3 point, Vector3 axis, float angle)
    {
        return RotateAround(point, Vector3.Zero, axis, angle);
    }
}
