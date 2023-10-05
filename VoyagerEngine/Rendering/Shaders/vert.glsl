#version 330 core

layout (location = 0) in vec2 vert;
uniform vec4 camera;
uniform vec4 window;
uniform vec2 position;
uniform vec4 color;

out vec4 out_color;

void main()
{
    gl_Position = vec4((vert.x-camera.x) / window.x - 0.5,
                        (vert.y-camera.y) / window.y - 0.5,
                        0, 1.0);
    out_color = color;
}