using System.Numerics;
using Silk.NET.OpenGL;
using TAGL.Shaders;

public class Shader : IDisposable
{
    private readonly GL _gl;
    public uint Handle { get; }

    public Shader(GL gl) : this(
    gl,
    File.ReadAllText(ShaderPath.DefaultVertexPath),
    File.ReadAllText(ShaderPath.DefaultFragmentPath))
    {
    }

    public Shader(GL gl, string vertexSource, string fragmentSource)
    {
        _gl = gl;

        uint vertex = _gl.CreateShader(ShaderType.VertexShader);
        _gl.ShaderSource(vertex, vertexSource);
        _gl.CompileShader(vertex);
        CheckShader(vertex);

        uint fragment = _gl.CreateShader(ShaderType.FragmentShader);
        _gl.ShaderSource(fragment, fragmentSource);
        _gl.CompileShader(fragment);
        CheckShader(fragment);

        _gl.GetShader(fragment, ShaderParameterName.CompileStatus, out int fragmentStatus);

        Handle = _gl.CreateProgram();
        _gl.AttachShader(Handle, vertex);
        _gl.AttachShader(Handle, fragment);
        _gl.LinkProgram(Handle);

        _gl.GetProgram(Handle, ProgramPropertyARB.LinkStatus, out int status);
        if (status == 0)
            throw new Exception(_gl.GetProgramInfoLog(Handle));

        _gl.DeleteShader(vertex);
        _gl.DeleteShader(fragment);
    }

    public void Use()
    {
        _gl.UseProgram(Handle);
    }

    public void SetMatrix4(string name, Matrix4x4 matrix)
    {
        int location = _gl.GetUniformLocation(Handle, name);
        unsafe
        {
            _gl.UniformMatrix4(location, 1, false, (float*)&matrix);
        }
    }

    public void SetVector3(string name, Vector3 value)
    {
        int location = _gl.GetUniformLocation(Handle, name);
        _gl.Uniform3(location, value.X, value.Y, value.Z);
    }

    public void SetInt(string name, int value)
    {
        int location = _gl.GetUniformLocation(Handle, name);
        _gl.Uniform1(location, value);
    }

    private void CheckShader(uint shader)
    {
        _gl.GetShader(shader, ShaderParameterName.CompileStatus, out int status);
        if (status == 0)
            throw new Exception(_gl.GetShaderInfoLog(shader));
    }

    public void Dispose()
    {
        _gl.DeleteProgram(Handle);
    }
}