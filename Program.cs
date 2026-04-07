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
        DeckManager deckManager = new DeckManager();
        GLstuffClass gLstuffClass = new GLstuffClass();
        Management management = new Management();

        //call window
        //gLstuffClass.OpenWindow();

        deckManager.IniDeck();
        deckManager.ShuffleDeck();
        deckManager.SplitDeck();

        deckManager.DebugDeck();

        management.HandCards();

        


    }
    
}