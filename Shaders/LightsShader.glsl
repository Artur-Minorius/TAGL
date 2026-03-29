#define MAX_LIGHTS 8

struct PointLight {
    vec3 position;
    vec3 color;
};

uniform PointLight pointLights[MAX_LIGHTS];
uniform int lightCount;