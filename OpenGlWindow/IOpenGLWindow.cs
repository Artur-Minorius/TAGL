using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Silk.NET.OpenGL;

namespace TAGL.OpenGlWindow;

public interface IOpenGLWindow
{
    void SetTitle(string title);
    void SetSize(int width, int height);
    void SetOnClose(Action? action = null);
    void Clear(Color color);
    void AddRender(Action<double> onRender);
    void AddRender(Action<double, GL> onRender);
}
