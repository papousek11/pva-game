using System.Numerics;
using System.Reflection.Metadata.Ecma335;
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
    CardChecking cardChecking = new CardChecking();

    public void FirstRound()
    {
        ResetPot();
        ResetGivenToPot();
        ResetPasses();
        HandCards();
        Blinds();
        LetPlayerActionRoundOne();
        if (PassedAway()){ GiveItToTheChoosenOne(); }
        GiveThreeToTheTable();
        RundaSazek();
        if (PassedAway()){ GiveItToTheChoosenOne(); }
        GimmeOneForTheRoad();
        RundaSazek();
        if (PassedAway()){ GiveItToTheChoosenOne(); }
        GimmeOneForTheRoad();
        RundaSazek();
        if (PassedAway()){ GiveItToTheChoosenOne(); }
        GiveItToTheWinner();
        IniPlayers.NextRoundAllowed = true;
        
        
    }
    public void Otherrounds()
    {
        TakeCards();
        IniPlayers.NextRoundAllowed = false;
        ResetPot();
        ResetGivenToPot();
        ResetPasses();
        HandCards();
        Blinds();
        LetPlayerActionRoundOne();
        if (PassedAway()){ GiveItToTheChoosenOne(); }
        GiveThreeToTheTable();
        RundaSazek();
        if (PassedAway()){ GiveItToTheChoosenOne(); }
        GimmeOneForTheRoad();
        RundaSazek();
        if (PassedAway()){ GiveItToTheChoosenOne(); }
        GimmeOneForTheRoad();
        RundaSazek();
        if (PassedAway()){ GiveItToTheChoosenOne(); }
        GiveItToTheWinner();
        IniPlayers.NextRoundAllowed = true;
    }

    public void GiveItToTheChoosenOne()
    {
        for(int y= 0; y < IniPlayers.Inventories.Count; y++)
        {
            if(IniPlayers.Inventories[y].HasPassed == false)
            {
                IniPlayers.Inventories[y].Money = IniPlayers.Inventories[y].Money + IniPlayers.pot;
            }
        }
        ResetPot();
        ResetGivenToPot();
        ResetPasses();
    }

    public void TakeCards()
    {
        List<string> HoldIt = new List<string>{};

        for(int y= 0; y < IniPlayers.Inventories.Count; y++)
        {
            if(IniPlayers.Inventories[y].PlayerCards != null)
            {
                for(int i = 0; i < 2; i++)
                {
                    HoldIt.Add(IniPlayers.Inventories[y].PlayerCards[0]);
                    IniPlayers.Inventories[y].PlayerCards.RemoveAt(0);
                }
            }
        }
        for(int x = 0; x < 5; x++)
        {
            HoldIt.Add(IniPlayers.Table[0]);
            IniPlayers.Table.RemoveAt(0);
        }
        for(int u = 0; u < HoldIt.Count; u++)
        {

            DeckManager.Deck.Add(HoldIt[0]);
            HoldIt.RemoveAt(0);
        }

        
    }
    public bool PassedAway()
    {
        int plus = 0;
        for(int y= 0; y < IniPlayers.Inventories.Count; y++)
        {
            if(IniPlayers.Inventories[y].HasPassed == false)
            {
                plus++;
            }
        }
        if(plus == 1)
        {
            return true;
        }
        else
        {
            return false;
        }

        
    }

    public void GiveItToTheWinner()
    {
        bool DoWeSplit = SplitPot();
        int WinnerPos = WhoGetsItALl();
        IniPlayers.Inventories[WinnerPos].Money = IniPlayers.Inventories[WinnerPos].Money + IniPlayers.pot;
        ResetPot();
        ResetGivenToPot();
        ResetPasses();
    }
    public bool SplitPot()
    {
        List<int> scores = new List<int> {};
        //previous value
        int waltuh = 0;
        bool split = false;
      
        for(int y= 0; y < IniPlayers.Inventories.Count; y++)
        {
            if(IniPlayers.Inventories[y].PlayerCards == null)
            {
                scores.Add(0);
            }
            else
            {
                scores.Add(cardChecking.CheckValue(y));

            }
            
        }
        for(int x= 0; x < scores.Count-1; x++)
        {
            if(waltuh < scores[x])
            {
                waltuh = scores[x];
                
            }
            if(waltuh == scores[x])
            {
                split = true;
            }
        }
        
        
        
        return split;
    }
    public int WhoGetsItALl()
    {
        
        List<int> scores = new List<int> {};
        //previous value
        int waltuh = 0;
        int pos = 0;
        for(int y= 0; y < IniPlayers.Inventories.Count; y++)
        {
            if(IniPlayers.Inventories[y].PlayerCards == null)
            {
                scores.Add(0);
            }
            else
            {
                scores.Add(cardChecking.CheckValue(y));

            }
            
        }
        for(int x= 0; x < scores.Count; x++)
        {
            if(waltuh < scores[x])
            {
                waltuh = scores[x];
                pos = x;
            }
        }
        
        
        Console.WriteLine(pos+"balls");
        return pos;
    }
    ///gives three card to the table
    public void GiveThreeToTheTable()
    {
        for(int i =0; i < 3; i++)
        {
            IniPlayers.Table.Add(DeckManager.Deck[DeckManager.Deck.Count-1]);
            DeckManager.Deck.RemoveAt(DeckManager.Deck.Count-1);
        }
        
    }
    //gives one card to the table
    public void GimmeOneForTheRoad()
    {
         IniPlayers.Table.Add(DeckManager.Deck[DeckManager.Deck.Count-1]);
        DeckManager.Deck.RemoveAt(DeckManager.Deck.Count-1);
    }


    //Player bets that are for all other rounds than the first one
    public void RundaSazek()
    {
        int frongus = FindByDealer();
        int globus = frongus;
        int avlive = NumberAlive();
        int PlayersPlayed = 0;
        bool EveryOneDidSomething = false;
        int choose = 0;
        int holder = 0;
        Console.WriteLine("we actioning");
        while (EveryOneDidSomething == false)
        {
            if (globus == IniPlayers.Inventories.Count)
            {
                globus = 0;
            }
            if(IniPlayers.pot_raised_by - IniPlayers.Inventories[globus].GivenToPot > 0 && PlayersPlayed == avlive)
            {
                if(PlayersPlayed == avlive -1 && IniPlayers.pot_raised_by - IniPlayers.Inventories[globus].GivenToPot == 0)
                {
                        EveryOneDidSomething = true;
                    break;
                }
                else
                {
                    PlayersPlayed = PlayersPlayed -1;
                    Console.WriteLine("hello");
                }
                
                
                    
            }
            
            Thread.Sleep(500);
            if (PlayersPlayed == avlive)
            {
                
                
                
                    EveryOneDidSomething = true;
                    break;
                
                
                
                
                   
                
                
            }
            Console.WriteLine(PlayersPlayed + "played");
            Console.WriteLine(avlive + "played");

            if (IniPlayers.Inventories[globus].IsIn && IniPlayers.Inventories[globus].HasPassed == false)
            {
                Console.WriteLine("we got thourgt the if");
                choose = IniPlayers.interactions[globus]();
                //Console.WriteLine(choose = IniPlayers.interactions[globus]);
                Console.WriteLine(globus);
                switch (choose)
                {
                    //call / check
                    case 0:

                        holder = IniPlayers.pot_raised_by - IniPlayers.Inventories[globus].GivenToPot;
                        Console.WriteLine(IniPlayers.Inventories[globus].GivenToPot);
                        IniPlayers.Inventories[globus].GivenToPot = IniPlayers.Inventories[globus].GivenToPot + holder;
                        IniPlayers.Inventories[globus].Money = IniPlayers.Inventories[globus].Money - holder;
                        IniPlayers.pot = IniPlayers.pot + holder;
                        PlayersPlayed++;
                        globus++;
                        break;
                    //raise
                    case 1:
                        holder = IniPlayers.pot_raised_by - IniPlayers.Inventories[globus].GivenToPot;
                        IniPlayers.pot_raised_by = IniPlayers.pot_raised_by + IniPlayers.BigBlindValue;
                        IniPlayers.Inventories[globus].Money = IniPlayers.Inventories[globus].Money - holder - IniPlayers.BigBlindValue;
                        IniPlayers.Inventories[globus].GivenToPot = IniPlayers.Inventories[globus].GivenToPot + holder + IniPlayers.BigBlindValue;
                        IniPlayers.pot = IniPlayers.pot + holder + IniPlayers.BigBlindValue;;
                        PlayersPlayed++;
                        globus++;
                        break;
                    //fold 
                    case 2:
                         IniPlayers.Inventories[globus].HasPassed = true;
                        PlayersPlayed++;
                        globus++;
                        Console.WriteLine("fold");
                        break;
                }
                
            }
            else
            {
                PlayersPlayed++;
                globus++;
            }
            Console.WriteLine(IniPlayers.pot_raised_by);
            
        }
    }

    //bets for the first round are in a diffrent void because of the blinds
    public void LetPlayerActionRoundOne()
    {
        int frongus = FindByDealer();
        int globus = frongus + 2;
        int avlive = NumberAlive();
        int PlayersPlayed = 0;
        bool EveryOneDidSomething = false;
        int choose = 0;
        int holder = 0;
        Console.WriteLine("we actioning");
        while (EveryOneDidSomething == false)
        {
            if (globus == IniPlayers.Inventories.Count)
            {
                globus = 0;
            }
            if(IniPlayers.pot_raised_by - IniPlayers.Inventories[globus].GivenToPot > 0 && PlayersPlayed == avlive)
            {
                if(PlayersPlayed == avlive -1 && IniPlayers.pot_raised_by - IniPlayers.Inventories[globus].GivenToPot == 0)
                {
                        EveryOneDidSomething = true;
                    break;
                }
                else
                {
                    PlayersPlayed = PlayersPlayed -1;
                    Console.WriteLine("hello");
                }
                
                
                    
            }
            
            Thread.Sleep(500);
            if (PlayersPlayed == avlive)
            {
                
                
                
                    EveryOneDidSomething = true;
                    break;
                
                
                
                
                   
                
                
            }
            

            if (IniPlayers.Inventories[globus].IsIn && IniPlayers.Inventories[globus].HasPassed == false)
            {
                Console.WriteLine("we got thourgt the if");
                choose = IniPlayers.interactions[globus]();
                //Console.WriteLine(choose = IniPlayers.interactions[globus]);
                Console.WriteLine(globus);
                switch (choose)
                {
                    //call / check
                    case 0:

                        holder = IniPlayers.pot_raised_by - IniPlayers.Inventories[globus].GivenToPot;
                        Console.WriteLine(IniPlayers.Inventories[globus].GivenToPot);
                        IniPlayers.Inventories[globus].GivenToPot = IniPlayers.Inventories[globus].GivenToPot + holder;
                        IniPlayers.Inventories[globus].Money = IniPlayers.Inventories[globus].Money - holder;
                        IniPlayers.pot = IniPlayers.pot + holder;
                        PlayersPlayed++;
                        globus++;
                        break;
                    //raise
                    case 1:
                        holder = IniPlayers.pot_raised_by - IniPlayers.Inventories[globus].GivenToPot;
                        IniPlayers.pot_raised_by = IniPlayers.pot_raised_by + IniPlayers.BigBlindValue;
                        IniPlayers.Inventories[globus].Money = IniPlayers.Inventories[globus].Money - holder - IniPlayers.BigBlindValue;
                        IniPlayers.Inventories[globus].GivenToPot = IniPlayers.Inventories[globus].GivenToPot + holder + IniPlayers.BigBlindValue;
                        IniPlayers.pot = IniPlayers.pot + holder + IniPlayers.BigBlindValue;;
                        PlayersPlayed++;
                        globus++;
                        break;
                    //fold 
                    case 2:
                        IniPlayers.Inventories[globus].HasPassed = true;
                        PlayersPlayed++;
                        globus++;
                        Console.WriteLine("fold");
                        break;
                }
                
            }
            else
            {
                PlayersPlayed++;
                globus++;
            }
            Console.WriteLine(IniPlayers.pot_raised_by);
            
        }




    }
    public int NumberAlive()
    {
        int values = 0;
        for (int i = 0; i < IniPlayers.Inventories.Count; i++)
        {
            if (IniPlayers.Inventories[i].IsIn && IniPlayers.Inventories[i].HasPassed == false)
            {
                values++;
            }
        }
        return values;

    }
    public void HandCards()
    {
        int globus = 0;
        AssingDealer();
        int frongus = FindByDealer();
        if (frongus == 0)
        {
            globus = 0;
        }
        else
        {
            globus = frongus + 1;
        }

        for (int i = 0; i < IniPlayers.Inventories.Count; i++)
        {
            if (globus > IniPlayers.Inventories.Count - 1)
            {
                globus = 0;
            }
            if (IniPlayers.Inventories[globus].IsIn)
            {
                for (int y = 2; y > 0; y--)
                {
                    IniPlayers.Inventories[globus].PlayerCards.Add(DeckManager.Deck[DeckManager.Deck.Count - 1]);
                    DeckManager.Deck.RemoveAt(DeckManager.Deck.Count - 1);
                }
            }
            Console.WriteLine("kartus");
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
            if (globus > IniPlayers.Inventories.Count -1)
            {
                globus = 0;
            }
            if (IniPlayers.Inventories[globus].IsIn && IniPlayers.Inventories[globus].IsDealer == false)
            {
                if (IniPlayers.Inventories[globus].Money < IniPlayers.SmallBlindValue)
                {
                    IniPlayers.Inventories[globus].GivenToPot = IniPlayers.Inventories[globus].GivenToPot + IniPlayers.Inventories[globus].Money;
                    IniPlayers.pot = IniPlayers.pot + IniPlayers.Inventories[globus].Money;
                    IniPlayers.Inventories[globus].Money = 0;

                    SmallBlind = true;
                }
                else
                {
                    IniPlayers.Inventories[globus].Money = IniPlayers.Inventories[globus].Money - IniPlayers.SmallBlindValue;
                    IniPlayers.Inventories[globus].GivenToPot = IniPlayers.Inventories[globus].GivenToPot + IniPlayers.SmallBlindValue;
                    IniPlayers.pot = IniPlayers.pot + IniPlayers.SmallBlindValue;
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
                    IniPlayers.pot = IniPlayers.pot + IniPlayers.Inventories[globus].Money;
                    IniPlayers.Inventories[globus].Money = 0;
                    IniPlayers.pot_raised_by = IniPlayers.pot_raised_by + IniPlayers.BigBlindValue;
                    BigBlind = true;
                }
                else
                {
                    IniPlayers.Inventories[globus].Money = IniPlayers.Inventories[globus].Money - IniPlayers.BigBlindValue;
                    IniPlayers.Inventories[globus].GivenToPot = IniPlayers.Inventories[globus].GivenToPot + IniPlayers.BigBlindValue;
                    IniPlayers.pot = IniPlayers.pot + IniPlayers.BigBlindValue;
                    IniPlayers.pot_raised_by = IniPlayers.pot_raised_by + IniPlayers.BigBlindValue;
                    BigBlind = true;
                }
            }
            globus++;
        }


    }

    public void ResetGivenToPot()
    {
        for (int i = 0; i < IniPlayers.Inventories.Count ; i++)
        {
            IniPlayers.Inventories[i].GivenToPot = 0;
        }
    }
    public void ResetPot()
    {
        //jen resetuje pot funce by se měla vysvěttlit sama
        IniPlayers.pot = 0;
    }
    
    public void ResetPasses()
    {
        //reset the status of ,,Passing" of all players
        for (int i = 0; i < IniPlayers.Inventories.Count ; i++)
        {
            if (IniPlayers.Inventories[i].IsIn == true)
            {
            IniPlayers.Inventories[i].HasPassed = false;
           }
            
        }
    }
    public void ClearInbeetweenTurns()
    {
        //Clears dealers from all players + splits the deck just like in the super ultra real game named poker(TM)
        deckManager.SplitDeck();
        for(int i = 0; i < IniPlayers.Inventories.Count; i++)
        {
            IniPlayers.Inventories[i].IsDealer = false;
        }
    }
    public void RestartGame()
    {

        //should explaint itself
        DeckManager.IniDeck();
        deckManager.ShuffleDeck();



        //restart player stats
        for(int i = 0; i < IniPlayers.Inventories.Count; i++)
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
            //not sure if this works 100% will find out later
            int DealHold = FindByDealer();
            int SecondHolder = DealHold+1;
            
            //chci se zabít
            for(int y= 0; y < IniPlayers.Inventories.Count; y++)
            {
                if(SecondHolder> IniPlayers.Inventories.Count-1)
                {
                   SecondHolder = 0;
                }
                if(IniPlayers.Inventories[SecondHolder].IsDealer == false && IniPlayers.Inventories[SecondHolder].IsIn)
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
            //also not sure if this work 100% of times idk
            for (int i = 0; i < IniPlayers.Inventories.Count ; i++)
            {
                if (IniPlayers.Inventories[i].IsIn == true)
                {
                    IniPlayers.Inventories[i].IsDealer = true;
                    break;
                }
            }
        }
    }

    public bool IsDealerGame()
    {
        //the name explains itself
        for (int i = 0; i < IniPlayers.Inventories.Count ; i++)
        {
            if (IniPlayers.Inventories[i].IsDealer == true)
            {
                if (IniPlayers.Inventories[i].IsIn == true)
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
        //najde index objektu hrače podle toho jestli je dealer
        //finds inxed of the player object by scanning for the IsDealer value
        return IniPlayers.Inventories.FindIndex(s => s.IsDealer == true);
    }
  
}