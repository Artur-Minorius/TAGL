using System.Numerics;
using Silk.NET.OpenGL;
using TAGL.OpenGlWindow;
using TAGL.Shaders;

namespace TAGL.Meshes;

public class CubeMesh: IMesh
{
    private readonly GL _graphics;
    private readonly uint _vao;
    private readonly uint _vbo;
    private readonly Shader _shader;
    public Shader Shader => _shader;
    public Transform Transform { get; } = new Transform();
    public Vector3 Color = new Vector3(0.2f, 0.6f, 1.0f);

    private static readonly float[] _vertices = [
    // positions          // normals
    -0.5f,-0.5f,-0.5f,  0f, 0f,-1f,
     0.5f,-0.5f,-0.5f,  0f, 0f,-1f,
     0.5f, 0.5f,-0.5f,  0f, 0f,-1f,
     0.5f, 0.5f,-0.5f,  0f, 0f,-1f,
    -0.5f, 0.5f,-0.5f,  0f, 0f,-1f,
    -0.5f,-0.5f,-0.5f,  0f, 0f,-1f,

    -0.5f,-0.5f, 0.5f,  0f, 0f, 1f,
     0.5f,-0.5f, 0.5f,  0f, 0f, 1f,
     0.5f, 0.5f, 0.5f,  0f, 0f, 1f,
     0.5f, 0.5f, 0.5f,  0f, 0f, 1f,
    -0.5f, 0.5f, 0.5f,  0f, 0f, 1f,
    -0.5f,-0.5f, 0.5f,  0f, 0f, 1f,

    -0.5f, 0.5f, 0.5f, -1f, 0f, 0f,
    -0.5f, 0.5f,-0.5f, -1f, 0f, 0f,
    -0.5f,-0.5f,-0.5f, -1f, 0f, 0f,
    -0.5f,-0.5f,-0.5f, -1f, 0f, 0f,
    -0.5f,-0.5f, 0.5f, -1f, 0f, 0f,
    -0.5f, 0.5f, 0.5f, -1f, 0f, 0f,

     0.5f, 0.5f, 0.5f,  1f, 0f, 0f,
     0.5f, 0.5f,-0.5f,  1f, 0f, 0f,
     0.5f,-0.5f,-0.5f,  1f, 0f, 0f,
     0.5f,-0.5f,-0.5f,  1f, 0f, 0f,
     0.5f,-0.5f, 0.5f,  1f, 0f, 0f,
     0.5f, 0.5f, 0.5f,  1f, 0f, 0f,

    -0.5f,-0.5f,-0.5f,  0f,-1f, 0f,
     0.5f,-0.5f,-0.5f,  0f,-1f, 0f,
     0.5f,-0.5f, 0.5f,  0f,-1f, 0f,
     0.5f,-0.5f, 0.5f,  0f,-1f, 0f,
    -0.5f,-0.5f, 0.5f,  0f,-1f, 0f,
    -0.5f,-0.5f,-0.5f,  0f,-1f, 0f,

    -0.5f, 0.5f,-0.5f,  0f, 1f, 0f,
     0.5f, 0.5f,-0.5f,  0f, 1f, 0f,
     0.5f, 0.5f, 0.5f,  0f, 1f, 0f,
     0.5f, 0.5f, 0.5f,  0f, 1f, 0f,
    -0.5f, 0.5f, 0.5f,  0f, 1f, 0f,
    -0.5f, 0.5f,-0.5f,  0f, 1f, 0f,
];


    public CubeMesh(GL gl)
    {
        _graphics = gl;
        _shader = new Shader(gl);

        _vao = _graphics.GenVertexArray();
        _vbo = _graphics.GenBuffer();

        _graphics.BindVertexArray(_vao);
        _graphics.BindBuffer(BufferTargetARB.ArrayBuffer, _vbo);

        unsafe
        {
            fixed (float* v = &_vertices[0])
            {
                _graphics.BufferData(
                    BufferTargetARB.ArrayBuffer,
                    (nuint)(_vertices.Length * sizeof(float)),
                    v,
                    BufferUsageARB.StaticDraw);
            }

            _graphics.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), (void*)0);
            _graphics.EnableVertexAttribArray(0);

            _graphics.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), (void*)(3 * sizeof(float)));
            _graphics.EnableVertexAttribArray(1);
        }
    }

    public void Draw(Matrix4x4 view, Matrix4x4 projection, RenderMode mode)
    {
        _shader.Use();

        _shader.SetMatrix4(ShaderNames.Model, Transform.GetModelMatrix());
        _shader.SetMatrix4(ShaderNames.View, view);
        _shader.SetMatrix4(ShaderNames.Projection, projection);

        _graphics.BindVertexArray(_vao);

        if (mode.HasFlag(RenderMode.Fill))
        {
            _graphics.PolygonMode(TriangleFace.FrontAndBack, PolygonMode.Fill);
            _shader.SetVector3(ShaderNames.ObjectColor, Color);
            _graphics.DrawArrays(PrimitiveType.Triangles, 0, 36);
        }

        if (mode.HasFlag(RenderMode.Outline))
        {
            _graphics.Enable(EnableCap.PolygonOffsetLine);
            _graphics.PolygonOffset(-1f, -1f);

            _graphics.PolygonMode(TriangleFace.FrontAndBack, PolygonMode.Line);
            _shader.SetVector3(ShaderNames.ObjectColor, Vector3.Zero);

            _graphics.DrawArrays(PrimitiveType.Triangles, 0, 36);

            _graphics.PolygonMode(TriangleFace.FrontAndBack, PolygonMode.Fill);
        }
    }

    public void Dispose()
    {
        _graphics.DeleteBuffer(_vbo);
        _graphics.DeleteVertexArray(_vao);
    }
}
