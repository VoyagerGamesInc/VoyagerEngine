#version 330 core

layout (location = 0) in vec2 vert;
layout (location = 1) in vec4 color;
layout (location = 2) in vec2 position;
uniform vec2 viewport;
uniform vec2 camera;

out vec4 o_color;

void main()
{
    gl_Position = vec4(vert, 0, 1.0);
    o_color = color;
}