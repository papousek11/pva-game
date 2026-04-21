using ImGuiNET;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Numerics;

namespace main;
public class ImGuiController
{
    int width, height;
    bool frameBegun;

    public ImGuiController(int w, int h)
    {
        width = w;
        height = h;

        ImGui.CreateContext();
        var io = ImGui.GetIO();

        io.Fonts.AddFontDefault();

    
        io.Fonts.GetTexDataAsRGBA32(out IntPtr pixels, out int texWidth, out int texHeight, out int bpp);
        io.Fonts.SetTexID((IntPtr)1);
        io.Fonts.ClearTexData();

        frameBegun = false;
    }
    public void Resize(int w, int h)
    {
        width = w;
        height = h;
    }

   public void Update(float deltaTime)
    {
    var io = ImGui.GetIO();

    // Always give valid size
    int safeWidth = width <= 0 ? 1 : width;
    int safeHeight = height <= 0 ? 1 : height;

    // Finish previous frame if needed
    if (frameBegun)
    {
        ImGui.Render();
        frameBegun = false;
    }

    // Set required data EVERY frame
    io.DisplaySize = new Vector2(safeWidth, safeHeight);
    io.DeltaTime = Math.Max(deltaTime, 1f / 60f);

    // Start new frame (ALWAYS)
    ImGui.NewFrame();
    frameBegun = true;
    }

public void Render()
{
    if (!frameBegun) return;

    ImGui.Render();
    frameBegun = false;
}
}