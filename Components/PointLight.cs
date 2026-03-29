using System.Numerics;
using TAGL.Components.Interfaces;

namespace TAGL.Components;

public class PointLight : ILight
{
    public Transform Transform { get; } = new();
    public Vector3 Color { get; set; } = Vector3.One;
    public float Intensity { get; set; } = 1f;
    public void Apply(Shader shader, int index)
    {
        shader.SetVector3($"pointLights[{index}].position", Transform.Position);
        shader.SetVector3($"pointLights[{index}].color", Color * Intensity);
    }
}