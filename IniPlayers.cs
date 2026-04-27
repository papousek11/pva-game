using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace main;


class IniPlayers
{
    public static PlayerInventory Player = new PlayerInventory(10000, true, false, "player");
    public static PlayerInventory AI1 = new PlayerInventory(10000, true, false, "AI1");
    public static PlayerInventory AI2 = new PlayerInventory(10000, true, false, "AI2");
    public static PlayerInventory AI3 = new PlayerInventory(10000, true, false, "AI3");
    public static PlayerInventory AI4 = new PlayerInventory(10000, true, false, "AI4");

    public static List<PlayerInventory> Inventories = new List<PlayerInventory> {Player, AI1, AI2, AI3, AI4 };

    public static int SmallBlindValue = 200;
    public static int BigBlindValue = 300;

    public static int pot = 0;

    public static int Scene = 0;

    public static Management management = new Management();

    public static DeckManager deckManager = new DeckManager();

}