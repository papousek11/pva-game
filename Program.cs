using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;



namespace main;

//předem se omlouvam že musíte známkovat tyhle špagety
class EntryClass
{


    
    static void Main(string[] args)
    {
        DeckManager deckManager = new DeckManager();
        deckManager.IniDeck();
        deckManager.ShuffleDeck();
        deckManager.SplitDeck();

        deckManager.DebugDeck();



    }
    
}