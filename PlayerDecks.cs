using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using Newtonsoft.Json;
using OpenTK.Graphics.ES20;

namespace main;


class PlayerInventory
{
    
    //ruka
    public List<string> DeckPlayerPlayer = new List<string>{};
    public List<string> DeckPlayerAI1 = new List<string>{};
    public List<string> DeckPlayerAI2 = new List<string>{};
    public List<string> DeckPlayerAI3 = new List<string>{};
    public List<string> DeckPlayerAI4 = new List<string>{};


    //money
    public int PlayerPlayerMoney;
    public int PlayerAI1Money;
    public int PlayerAI2Money;
    public int PlayerAI3Money;
    public int PlayerAI4Money;

    //vypad
    public bool PlayerPlayerIN;
    public bool PlayerAI1IN;
    public bool PlayerAI2IN;
    public bool PlayerAI3IN;
    public bool PlayerAI4IN;

    
    //dealer
    public bool PlayerPlayerIsDealer;
    public bool PlayerAI1IsDealer;
    public bool PlayerAI2IsDealer;
    public bool PlayerAI3IsDealer;
    public bool PlayerAI4IsDealer;

}