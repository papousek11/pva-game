using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using System.Net.NetworkInformation;
using OpenTK.Windowing.Common.Input;
using ImGuiNET;




namespace main;

class GLstuffClass()
{
    
    
    public void OpenWindow()
    {
         var native = new NativeWindowSettings()
        {
            Size = new OpenTK.Mathematics.Vector2i(800, 600),
            Title = "ImGui Clean Setup"
        };

        using var window = new GameWindow(GameWindowSettings.Default, native);

        ImGuiController controller = null!;

        window.Load += () =>
        {
            GL.ClearColor(0.1f, 0.1f, 0.1f, 1f);
            controller = new ImGuiController(window.Size.X, window.Size.Y);
        };

        window.RenderFrame += args =>
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            controller.Update((float)args.Time);

            ImGui.Begin("Hello");
            ImGui.Text("It works 🎉");
            ImGui.Button("Button");
            ImGui.End();

            controller.Render();

            window.SwapBuffers();
        };

        window.Resize += e =>
        {
            GL.Viewport(0, 0, e.Width, e.Height);
            controller.Resize(e.Width, e.Height);
        };

        window.Run();
       
    }
    public void MolestWindow()
    {
        
    }
}