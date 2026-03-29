using System.Drawing;
using Silk.NET.OpenGL;
using TAGL.Meshes;
using TAGL.OpenGlWindow;

namespace TAGL.Components;

public class Renderer
{
    private readonly GL _gl;
    private readonly Camera _camera;

    public Renderer(GL gl, Camera camera)
    {
        _gl = gl;
        _camera = camera;
    }

    public void Resize(int width, int height)
    {
        _gl.Viewport(0, 0, (uint)width, (uint)height);
        _camera.Resize(width, height);
    }

    public void Clear(Color color)
    {
        _gl.Enable(GLEnum.DepthTest);
        _gl.ClearColor(color);
        _gl.Clear((uint)(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit));
    }

    public void Draw(IMesh mesh, RenderMode mode)
    {
        mesh.Draw(
            _camera.GetViewMatrix(),
            _camera.GetProjectionMatrix(),
            mode);
    }
}