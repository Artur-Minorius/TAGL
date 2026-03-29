using System.Numerics;

namespace TAGL.Components;

public class Camera
{
    public Vector3 Position { get; set; } = new(0, 0, 3);
    public Vector3 Target { get; set; } = Vector3.Zero;
    public float Fov { get; set; } = MathF.PI / 4f;
    public float Near { get; set; } = 0.1f;
    public float Far { get; set; } = 100f;

    private float _aspectRatio = 1f;

    public void Resize(int width, int height)
    {
        _aspectRatio = width / (float)height;
    }

    public Matrix4x4 GetViewMatrix()
    {
        var up = GetOrbitAxis();

        return Matrix4x4.CreateLookAt(Position, Target, up);
    }

    public Matrix4x4 GetProjectionMatrix()
    {
        return Matrix4x4.CreatePerspectiveFieldOfView(
            Fov,
            _aspectRatio,
            Near,
            Far);
    }

    public Vector3 GetOrbitAxis()
    {
        var offset = Vector3.Normalize(Target - Position);

        var up = MathF.Abs(Vector3.Dot(offset, Vector3.UnitY)) < 0.999f
            ? Vector3.UnitY
            : Vector3.UnitZ;

        return up;
    }
}