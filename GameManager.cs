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



    public void HandCards()
    {
        AssingDealer();
        int frongus = FindByDealer();
        int globus = frongus+1;
        for(int i = 0; i < IniPlayers.Inventories.Count-1; i++)
        {
            if(globus > IniPlayers.Inventories.Count-1)
            {
                globus = 0;
            }
            if (IniPlayers.Inventories[globus].IsIn)
            {
                for(int y = 2; y > 0; i--)
                {
                    IniPlayers.Inventories[globus].PlayerCards.Add(DeckManager.Deck[DeckManager.Deck.Count-1]);
                    DeckManager.Deck.RemoveAt(DeckManager.Deck.Count-1);
                }
            }

            globus++;
        }
    }

    public void Blinds()
    {



        int frongus = FindByDealer();
        int globus = frongus + 1;

        bool BigBlind = false;
        bool SmallBlind = false;
        while (SmallBlind == false)
        {
            if (globus > IniPlayers.Inventories.Count - 1)
            {
                globus = 0;
            }
            if (IniPlayers.Inventories[globus].IsIn && IniPlayers.Inventories[globus].IsDealer == false)
            {
                if (IniPlayers.Inventories[globus].Money < IniPlayers.SmallBlindValue)
                {
                    IniPlayers.Inventories[globus].GivenToPot = IniPlayers.Inventories[globus].GivenToPot + IniPlayers.Inventories[globus].Money;
                    IniPlayers.Inventories[globus].Money = 0;
                    SmallBlind = true;
                }
                else
                {
                    IniPlayers.Inventories[globus].Money = IniPlayers.Inventories[globus].Money - IniPlayers.SmallBlindValue;
                    IniPlayers.Inventories[globus].GivenToPot = IniPlayers.Inventories[globus].GivenToPot + IniPlayers.SmallBlindValue;
                    SmallBlind = true;
                }
            }
            globus++;
        }
        while (BigBlind == false)
        {
            if (globus > IniPlayers.Inventories.Count - 1)
            {
                globus = 0;
            }
            if (IniPlayers.Inventories[globus].IsIn)
            {
                if (IniPlayers.Inventories[globus].Money < IniPlayers.BigBlindValue)
                {
                    IniPlayers.Inventories[globus].GivenToPot = IniPlayers.Inventories[globus].GivenToPot + IniPlayers.Inventories[globus].Money;
                    IniPlayers.Inventories[globus].Money = 0;
                    BigBlind = true;
                }
                else
                {
                    IniPlayers.Inventories[globus].Money = IniPlayers.Inventories[globus].Money - IniPlayers.BigBlindValue;
                    IniPlayers.Inventories[globus].GivenToPot = IniPlayers.Inventories[globus].GivenToPot + IniPlayers.BigBlindValue;
                    BigBlind = true;
                }
            }
            globus++;
        }


    }
    public void Actions()
    {
        
    }
    public void ResetGivenToPot()
    {
        for (int i = 0; i < IniPlayers.Inventories.Count - 1; i++)
        {
            IniPlayers.Inventories[i].GivenToPot = 0;
        }
    }
    public void FirstRound()
    {
        ResetGivenToPot();
        ResetPasses();
        HandCards();
        Blinds();
    }
    public void ResetPasses()
    {
        for (int i = 0; i < IniPlayers.Inventories.Count - 1; i++)
        {
            IniPlayers.Inventories[i].HasPassed = false;
        }
    }
    public void ClearInbeetweenTurns()
    {
        deckManager.SplitDeck();
        for(int i = 0; i < IniPlayers.Inventories.Count-1; i++)
        {
            IniPlayers.Inventories[i].IsDealer = false;
        }
    }
    public void RestartGame()
    {


        DeckManager.IniDeck();
        deckManager.ShuffleDeck();



        //restart player stats
        for(int i = 0; i < IniPlayers.Inventories.Count-1; i++)
        {
            IniPlayers.Inventories[i].IsDealer = false;
            IniPlayers.Inventories[i].IsIn = true;
            IniPlayers.Inventories[i].Money = 10000;
            IniPlayers.Inventories[i].PlayerCards.Clear();
        }
    }

    public void AssingDealer()
    {
        if (IsDealerGame())
        {
            int DealHold = FindByDealer();
            int SecondHolder = DealHold+1;
            
            
            for(int y= 0; y < IniPlayers.Inventories.Count-1; y++)
            {
                if(SecondHolder> IniPlayers.Inventories.Count-1)
                {
                   SecondHolder = 0;
                }
                if(IniPlayers.Inventories[SecondHolder].IsDealer = false && IniPlayers.Inventories[SecondHolder].IsIn)
                {
                    IniPlayers.Inventories[SecondHolder].IsDealer = true;
                    break;
                }
                SecondHolder++;
            }

            IniPlayers.Inventories[DealHold].IsDealer = false;
            
        }
        else
        {
            for(int i = 0; i < IniPlayers.Inventories.Count-1; i++)
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
        for(int i = 0; i < IniPlayers.Inventories.Count-1; i++)
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