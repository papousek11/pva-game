using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;
using Raylib_cs;



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