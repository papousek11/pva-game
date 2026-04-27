using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using System.Net.NetworkInformation;
using OpenTK.Windowing.Common.Input;
using ImGuiNET;
using System.Threading;
using System.Diagnostics;




namespace main;

class GLstuffClass()
{
    
    Thread StartTheGameThread = new Thread(IniPlayers.management.FirstRound);
    
    public void OpenWindow()
    {
         var native = new NativeWindowSettings()
        {
            Size = new OpenTK.Mathematics.Vector2i(800, 600),
            Title = "poker night"
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

            controller.Update(window, (float)args.Time);


            //BASICLY SCENE MANAGER
            switch (IniPlayers.Scene)
            {
                //loading screen
                case 0:

                    ImGui.Begin("Player Stats");
                    ImGui.Text("Player:"+IniPlayers.Player.Money);
                    ImGui.Text("AI1:"+IniPlayers.AI1.Money);
                    ImGui.Text("AI2:"+IniPlayers.AI2.Money);
                    ImGui.Text("AI3:"+IniPlayers.AI3.Money);
                    ImGui.Text("AI4:"+IniPlayers.AI4.Money);
                    ImGui.Text("POT:"+IniPlayers.pot);
                    if(ImGui.Button("nízkoklíčové tlačítko start"))
                    {
                        Console.WriteLine("bfdsugkhbgvhjdbsvj");
                        StartTheGameThread.Start();
                    }
        
                    ImGui.End();


                    ImGui.Begin("Cards");
                   
                    if(IniPlayers.Player.PlayerCards.Count > 0 && IniPlayers.AI1.PlayerCards.Count > 0 
                    && IniPlayers.AI2.PlayerCards.Count > 0 && IniPlayers.AI3.PlayerCards.Count > 0 && IniPlayers.AI4.PlayerCards.Count > 0)
                    {
                        //Console.WriteLine("tonk");
                        foreach (var card in IniPlayers.Player.PlayerCards)
                        {
                            ImGui.Text("player:"+card.ToString());
                        }
                        foreach (var card in IniPlayers.AI1.PlayerCards)
                        {
                            ImGui.Text("AI1:"+card.ToString());
                        }
                        foreach (var card in IniPlayers.AI2.PlayerCards)
                        {
                            ImGui.Text("AI2:"+card.ToString());
                        }
                        foreach (var card in IniPlayers.AI3.PlayerCards)
                        {
                            ImGui.Text("AI3:"+card.ToString());
                        }
                        foreach (var card in IniPlayers.AI4.PlayerCards)
                        {
                            ImGui.Text("AI4:"+card.ToString());
                        }
                    }
                    ImGui.End();

                break;



                //error screen
                case 7895:
                    ImGui.Begin("something fucked");
                    ImGui.Text("some unxpected error restart the game");
                    ImGui.End();



                break;
                    





                   
            }

            

            controller.Render();

            window.SwapBuffers();
        };

       

        window.Run();
       
    }
    public void MolestWindow()
    {
        
    }
}