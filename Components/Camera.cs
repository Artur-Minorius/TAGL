using System.Numerics;

namespace TAGL.Components;

public class Camera
{
    public Vector3 Position { get; set; } = new(0, 0, 3);
    public float Yaw { get; set; } = -MathF.PI / 2f;
    public float Pitch { get; set; } = 0f;
    public float Fov { get; set; } = MathF.PI / 4f;
    public float Near { get; set; } = 0.1f;
    public float Far { get; set; } = 100f;
    private float _aspectRatio = 1f;

    public Vector3 Forward
    {
        get
        {
            return Vector3.Normalize(new Vector3(
                MathF.Cos(Pitch) * MathF.Cos(Yaw),
                MathF.Sin(Pitch),
                MathF.Cos(Pitch) * MathF.Sin(Yaw)
            ));
        }
    }

    public Vector3 Right => Vector3.Normalize(Vector3.Cross(Forward, Vector3.UnitY));
    public Vector3 Up => Vector3.Normalize(Vector3.Cross(Right, Forward));

    public void Resize(int width, int height) =>
        _aspectRatio = width / (float)height;

    public Matrix4x4 GetViewMatrix() =>
        Matrix4x4.CreateLookAt(Position, Position + Forward, Vector3.UnitY);

    public Matrix4x4 GetProjectionMatrix() =>
        Matrix4x4.CreatePerspectiveFieldOfView(Fov, _aspectRatio, Near, Far);
}