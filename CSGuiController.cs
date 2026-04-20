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

        ImGui.NewFrame();
        frameBegun = true;
    }

    public void Resize(int w, int h)
    {
        width = w;
        height = h;
    }

    public void Update(float deltaTime)
    {
        if (frameBegun)
            ImGui.Render();

        var io = ImGui.GetIO();
        io.DisplaySize = new Vector2(width, height);
        io.DeltaTime = deltaTime;

        ImGui.NewFrame();
        frameBegun = true;
    }

    public void Render()
    {
        if (!frameBegun) return;

        ImGui.Render();
        frameBegun = false;

        // NOTE:
        // This minimal version DOES NOT draw via OpenGL yet.
        // It just proves ImGui runs without crashing.
    }
}