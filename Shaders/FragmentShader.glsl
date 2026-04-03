#version 330 core

in vec3 FragPos;
in vec3 Normal;

#define MAX_LIGHTS 8

struct PointLight {
    vec3 position;
    vec3 color;
};

uniform PointLight pointLights[MAX_LIGHTS];
uniform int lightCount;
uniform vec3 objectColor;
uniform vec3 viewPos;

out vec4 FragColor;

void main()
{
    vec3 result = vec3(0.0);

    for(int i = 0; i < lightCount; i++)
    {
        vec3 lightDir = normalize(pointLights[i].position - FragPos);

        vec3 ambient = 0.15 * pointLights[i].color;

        vec3 norm = normalize(Normal);
        float diff = max(dot(norm, lightDir), 0.0);
        vec3 diffuse = diff * pointLights[i].color;

        vec3 viewDir = normalize(viewPos - FragPos);
        vec3 reflectDir = reflect(-lightDir, norm);
        float spec = pow(max(dot(viewDir, reflectDir), 0.0), 32);
        vec3 specular = 0.5 * spec * pointLights[i].color;

        result += (ambient + diffuse + specular);
    }

    FragColor = vec4(result * objectColor, 1.0);
}