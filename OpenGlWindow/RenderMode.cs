namespace TAGL.OpenGlWindow;

[Flags]
public enum RenderMode
{
    Fill = 1,
    Outline = 2,
    FillWithOutline = Fill | Outline
}