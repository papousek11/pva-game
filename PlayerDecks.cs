using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace main;


class PlayerInventory
{
    
    //ruka
    public static List<string> DeckPlayerPlayer = new List<string>{};
    public static List<string> DeckPlayerAI1 = new List<string>{};
    public static List<string> DeckPlayerAI2 = new List<string>{};
    public static List<string> DeckPlayerAI3 = new List<string>{};
    public static List<string> DeckPlayerAI4 = new List<string>{};


    //money
    public static int PlayerPlayerMoney;
    public static int PlayerAI1Money;
    public static int PlayerAI2Money;
    public static int PlayerAI3Money;
    public static int PlayerAI4Money;

    //vypad
    public static bool PlayerPlayerIN = true;
    public static bool PlayerAI1IN = false;
    public static bool PlayerAI2IN = true;
    public  static bool PlayerAI3IN;
    public static bool PlayerAI4IN;

    
    //dealer
    public static bool PlayerPlayerIsDealer;
    public static bool PlayerAI1IsDealer;
    public static  bool PlayerAI2IsDealer;
    public static bool PlayerAI3IsDealer;
    public static bool PlayerAI4IsDealer;

}