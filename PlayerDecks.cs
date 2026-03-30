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

}