using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace main;


class PlayerInventory
{
    public bool IsIn;
    public int Money;
    public bool IsDealer;
    public string Name;
    public bool HasPassed;
    public int GivenToPot;
    public List<string> PlayerCards = new List<string> { };
    public PlayerInventory(int money, bool isin, bool isdealer, string name)
    {
        Money = money;
        IsIn = isin;
        IsDealer = isdealer;
        Name = name;
    }

}