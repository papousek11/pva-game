using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace main;

//předem se omlouvam že musíte známkovat tyhle špagety
class DeckManager
{
    public static List<string> Deck = new List<string> { };
    List<string> HolderDeck = new List<string>{};

    public static void IniDeck()
    {
        Deck.Clear();
        string json = File.ReadAllText("cards.json");
        var list_holder = JsonConvert.DeserializeObject<List<string>>(json);
        if(list_holder != null)
        {
            Deck = list_holder;
            
        }
        
        
    }
    public void SplitDeck()
    {
        HolderDeck.Clear();
        Random rnd = new Random();
        int random  = rnd.Next(23, 26);
        for(int i = 0; i < random; i++)
        {
            HolderDeck.Add(Deck[0]);
            Deck.RemoveAt(0);
            Deck.Add(HolderDeck[0]);
            HolderDeck.RemoveAt(0);
        }
    }
    public  void ShuffleDeck()
    {
        
        Random rnd = new Random();
        int n = Deck.Count;
        while (n > 1) {
            n--;
            int k = rnd.Next(n + 1);
            (Deck[n], Deck[k]) = (Deck[k], Deck[n]);
        }
    }
    public void DebugDeck()
    {
        Deck.ForEach(Console.WriteLine);
    }

    
    
}