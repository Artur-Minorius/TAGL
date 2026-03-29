using System.Numerics;

namespace TAGL;

public class Transform
{
    public Vector3 Position { get; set; } = Vector3.Zero;
    public Vector3 Rotation { get; set; } = Vector3.Zero;
    public Vector3 Scale { get; set; } = Vector3.One;

    public Matrix4x4 GetModelMatrix()
    {
        var translation = Matrix4x4.CreateTranslation(Position);
        var rotationX = Matrix4x4.CreateRotationX(Rotation.X);
        var rotationY = Matrix4x4.CreateRotationY(Rotation.Y);
        var rotationZ = Matrix4x4.CreateRotationZ(Rotation.Z);
        var scale = Matrix4x4.CreateScale(Scale);

        return scale *
               rotationX *
               rotationY *
               rotationZ *
               translation;
    }
}