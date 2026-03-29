using System.Drawing;
using System.Numerics;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace TAGL.OpenGlWindow;

public class OpenGLWindow : IDisposable, IOpenGLWindow
{
    protected readonly IWindow _window;
    protected GL _graphics;
    protected IInputContext _inputContext;
    public bool IsClosing => _window?.IsClosing ?? false;
    public GL Graphics => _graphics;
    public Vector2D<int> Size => _window.Size;
    public OpenGLWindow()
    {
        var shadersFolder = Path.Combine();
        _window = Window.Create(WindowOptions.Default);

        uint program = 0;

        _window.Load += () =>
        {
            _graphics = _window.CreateOpenGL();

            _inputContext = _window.CreateInput();
        };

        _window.FramebufferResize += size =>
        {
            _graphics?.Viewport(size);


            float ratio = size.X / (float)size.Y;

            var projection = Matrix4x4.CreateOrthographicOffCenter(
                -ratio, ratio,
                -1f, 1f,
                -1f, 1f);

            _graphics?.UseProgram(program);
            int location = _graphics.GetUniformLocation(program, "projection");

            unsafe
            {
                _graphics.UniformMatrix4(location, 1, false, (float*)&projection);
            }

            _graphics.Viewport(0, 0, (uint)size.X, (uint)size.Y);
        };

        _window.Closing += () =>
        {
            _inputContext?.Dispose();

            _graphics?.Dispose();
        };
    }

    public void SetTitle(string title)
    {
        _window.Title = title;
    }

    public void SetSize(int width, int height)
    {
        _window.Size = new Vector2D<int>(width, height);
    }

    public void SetOnClose(Action? action = null)
    {
        _window.Closing += action;
    }

    public void Clear(Color color)
    {
        _graphics.ClearColor(color);
    }

    public virtual void Run()
    {
        _window.Run();
    }

    public void Initialize()
    {
        _window.Initialize();
    }

    public void Update()
    {
        _window.DoEvents();
        _window.DoUpdate();
        _window.DoRender();
    }

    public void AddRender(Action<double> onRender)
    {
        _window.Render += onRender;
    }

    public void AddRender(Action<double, GL> onRender)
    {
        _window.Render += dt => onRender(dt, _graphics);
    }

    public void AddOnResize(Action<Vector2D<int>, GL> onResize)
    {
        _window.FramebufferResize += size => onResize(size, _graphics);
    }
    public void AddOnLoad(Action<GL> onLoad)
    {
        _window.Load += () => onLoad(_graphics);
    }

    public void Dispose()
    {
        _window.Dispose();
    }
}
