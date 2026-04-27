using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;




namespace main;

class EntryClass
{

   
    static void Main(string[] args)
    {
        DeckManager.IniDeck();
        IniPlayers.deckManager.ShuffleDeck();
        GLstuffClass gLstuffClass = new GLstuffClass();
        
      
        gLstuffClass.OpenWindow();
        

    }
    
}