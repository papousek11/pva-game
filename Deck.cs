using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;
using Microsoft.VisualBasic;
using Raylib_cs;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace main;

//předem se omlouvam že musíte známkovat tyhle špagety
class DeckManager
{
    public List<string> Deck = new List<string> { };


    public void IniDeck()
    {
        string json = File.ReadAllText("cards.json");
        var list_holder = JsonConvert.DeserializeObject<List<string>>(json);
        if(list_holder != null)
        {
            Deck = list_holder;
            Deck.ForEach(Console.WriteLine);
        }
        
    }

    
    
}