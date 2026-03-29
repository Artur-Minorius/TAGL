using System.Numerics;
using TAGL.OpenGlWindow;

namespace TAGL.Meshes;

public interface IMesh : IDisposable
{
    Shader Shader { get; }
    Transform Transform { get; }
    void Draw(Matrix4x4 view, Matrix4x4 projection, RenderMode mode);
}