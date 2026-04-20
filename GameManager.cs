using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.DataContracts;
using System.Runtime.Serialization.Formatters;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;
using main;
using Microsoft.VisualBasic;

class Management
{
    DeckManager deckManager = new DeckManager();





    public void RestartGame()
    {


        DeckManager.IniDeck();
        deckManager.ShuffleDeck();



        //restart player stats
        for(int i = 0; i < IniPlayers.Inventories.Count; i++)
        {
            IniPlayers.Inventories[i].IsDealer = false;
            IniPlayers.Inventories[i].IsIn = true;
            IniPlayers.Inventories[i].PlayerCards.Clear();
        }
    }

    public void AssingDealer()
    {
        if (IsDealerGame())
        {
            int DealHold = FindByDealer();
            int SecondHolder = DealHold;
            int g = 0;
            
            for(int y= 0; y < IniPlayers.Inventories.Count; y++)
            {
                if(g + DealHold > 4)
                {
                    DealHold = 0;
                    g = 0;
                }
                if(IniPlayers.Inventories[g + DealHold].IsDealer = false && IniPlayers.Inventories[g + DealHold].IsIn)
                {
                    IniPlayers.Inventories[g + DealHold].IsDealer = true;
                    break;
                }
                g++;
            }

            IniPlayers.Inventories[SecondHolder].IsDealer = false;
            
        }
        else
        {
            for(int i = 0; i < IniPlayers.Inventories.Count; i++)
            {
                if(IniPlayers.Inventories[i].IsIn == true)
                {
                    IniPlayers.Inventories[i].IsDealer = true;
                    break;
                }
            }
        }
    }

    public bool IsDealerGame()
    {
        for(int i = 0; i < IniPlayers.Inventories.Count; i++)
        {
            if(IniPlayers.Inventories[i].IsDealer == true)
            {
                if(IniPlayers.Inventories[i].IsIn == true)
                {
                    return true;
                }
                else
                {
                    IniPlayers.Inventories[i].IsDealer = false;
                    return false;
                }
            }
        }
        return false;
    }
    public int FindByDealer()
    {
        return IniPlayers.Inventories.FindIndex(s => s.IsDealer == true);
    }
  
}