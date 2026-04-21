using System;
using System.Numerics;
using ImGuiNET;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace main;

public class ImGuiController
{
    int _width, _height;

    int _vertexArray;
    int _vertexBuffer;
    int _indexBuffer;

    int _shader;
    int _fontTexture;

    bool _frameBegun;

    public ImGuiController(int width, int height)
    {
        _width = width;
        _height = height;

        ImGui.CreateContext();
        var io = ImGui.GetIO();
        io.Fonts.AddFontDefault();

        CreateFontTexture();
        CreateDeviceResources();

        _frameBegun = false;
    }

    // ---------------------------
    // PUBLIC API
    // ---------------------------

    public void WindowResized(int width, int height)
    {
        _width = width;
        _height = height;
    }

    public void Update(float deltaTime)
    {
        var io = ImGui.GetIO();

        io.DisplaySize = new System.Numerics.Vector2(Math.Max(_width, 1), Math.Max(_height, 1));
        io.DeltaTime = Math.Max(deltaTime, 1f / 60f);

        ImGui.NewFrame();
        _frameBegun = true;
    }

    public void Render()
    {
        if (!_frameBegun)
            return;

        ImGui.Render();
        RenderDrawData(ImGui.GetDrawData());

        _frameBegun = false;
    }

    // ---------------------------
    // OPENGL SETUP
    // ---------------------------

    void CreateDeviceResources()
    {
        _vertexArray = GL.GenVertexArray();
        _vertexBuffer = GL.GenBuffer();
        _indexBuffer = GL.GenBuffer();

        GL.BindVertexArray(_vertexArray);

        GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBuffer);
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, _indexBuffer);

        int stride = 20; // ImDrawVert size

        GL.EnableVertexAttribArray(0);
        GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, stride, 0);

        GL.EnableVertexAttribArray(1);
        GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, stride, 8);

        GL.EnableVertexAttribArray(2);
        GL.VertexAttribPointer(2, 4, VertexAttribPointerType.UnsignedByte, true, stride, 16);

        CreateShader();

        GL.BindVertexArray(0);
    }

    void CreateShader()
    {
        string vertexSrc = @"#version 330 core
layout(location=0) in vec2 aPos;
layout(location=1) in vec2 aUV;
layout(location=2) in vec4 aColor;

uniform mat4 projection;

out vec2 UV;
out vec4 Color;

void main()
{
    UV = aUV;
    Color = aColor;
    gl_Position = projection * vec4(aPos, 0, 1);
}";

        string fragmentSrc = @"#version 330 core
in vec2 UV;
in vec4 Color;

uniform sampler2D tex;

out vec4 FragColor;

void main()
{
    FragColor = Color * texture(tex, UV);
}";

        int v = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(v, vertexSrc);
        GL.CompileShader(v);

        int f = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(f, fragmentSrc);
        GL.CompileShader(f);

        _shader = GL.CreateProgram();
        GL.AttachShader(_shader, v);
        GL.AttachShader(_shader, f);
        GL.LinkProgram(_shader);

        GL.DeleteShader(v);
        GL.DeleteShader(f);
    }

    void CreateFontTexture()
    {
        var io = ImGui.GetIO();

        io.Fonts.GetTexDataAsRGBA32(out IntPtr pixels, out int w, out int h, out _);

        _fontTexture = GL.GenTexture();
        GL.BindTexture(TextureTarget.Texture2D, _fontTexture);

        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba,
            w, h, 0, PixelFormat.Rgba, PixelType.UnsignedByte, pixels);

        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

        io.Fonts.SetTexID((IntPtr)_fontTexture);
        io.Fonts.ClearTexData();
    }

    // ---------------------------
    // RENDERING
    // ---------------------------

    void RenderDrawData(ImDrawDataPtr drawData)
    {
        GL.UseProgram(_shader);

        Matrix4 projection = Matrix4.CreateOrthographicOffCenter(
            0, _width,
            _height, 0,
            -1, 1);

        int loc = GL.GetUniformLocation(_shader, "projection");
        GL.UniformMatrix4(loc, false, ref projection);

        GL.BindVertexArray(_vertexArray);

        GL.Enable(EnableCap.Blend);
        GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        GL.Disable(EnableCap.DepthTest);
        GL.Disable(EnableCap.CullFace);

        for (int n = 0; n < drawData.CmdListsCount; n++)
        {
            var cmdList = drawData.CmdLists[n];

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBuffer);
            GL.BufferData(BufferTarget.ArrayBuffer,
                cmdList.VtxBuffer.Size * 20,
                cmdList.VtxBuffer.Data,
                BufferUsageHint.StreamDraw);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _indexBuffer);
            GL.BufferData(BufferTarget.ElementArrayBuffer,
                cmdList.IdxBuffer.Size * sizeof(ushort),
                cmdList.IdxBuffer.Data,
                BufferUsageHint.StreamDraw);

            int idxOffset = 0;

            for (int i = 0; i < cmdList.CmdBuffer.Size; i++)
            {
                var cmd = cmdList.CmdBuffer[i];

                GL.BindTexture(TextureTarget.Texture2D, (int)cmd.TextureId);

                var clip = cmd.ClipRect;
                GL.Scissor(
                    (int)clip.X,
                    (int)(_height - clip.W),
                    (int)(clip.Z - clip.X),
                    (int)(clip.W - clip.Y));

                GL.DrawElements(
                    PrimitiveType.Triangles,
                    (int)cmd.ElemCount,
                    DrawElementsType.UnsignedShort,
                    idxOffset * sizeof(ushort));

                idxOffset += (int)cmd.ElemCount;
            }
        }

        GL.BindVertexArray(0);
    }
}