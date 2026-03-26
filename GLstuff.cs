using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;



namespace main;

class GLstuffClass()
{
    
    public NativeWindowSettings nativeWindowSettings = new NativeWindowSettings()
    {
        Size = new OpenTK.Mathematics.Vector2i(800, 600),
        Title = "Poker Night",
    };
    public void OpenWindow()
    {
        using (var window = new GameWindow(GameWindowSettings.Default, nativeWindowSettings))
        {
            window.Load += () =>
            {
                // okena
                GL.ClearColor(0.1f, 0.2f, 0.3f, 1.0f);
            };
            window.RenderFrame += (FrameEventArgs args) =>
            {
                GL.Clear(ClearBufferMask.ColorBufferBit);
                window.SwapBuffers();
            };

            window.Run();
        }


    }
}