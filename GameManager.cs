using System.Numerics;
using System.Runtime.CompilerServices;
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
        int dealer;
        if (CheckIfAll())
        {
            AssingDealer();
            dealer = GetDealerName();
            deckManager.SplitDeck();
            List<List<string>> hraci_inv = new List<List<string>>();
            //fallback safe thingy
            if (dealer == 40) { AssingDealer(); }
            hraci_inv.Add(PlayerInventory.DeckPlayerPlayer);
            hraci_inv.Add(PlayerInventory.DeckPlayerAI1);
            hraci_inv.Add(PlayerInventory.DeckPlayerAI2);
            hraci_inv.Add(PlayerInventory.DeckPlayerAI3);
            hraci_inv.Add(PlayerInventory.DeckPlayerAI4);
            if (dealer == 5)
            {
                for (int e = 0; e < 5; e++)
                {
                    for (int d = 0; d < 2; d++)
                    {
                        hraci_inv[e].Add(DeckManager.Deck[DeckManager.Deck.Count - 1]);
                        DeckManager.Deck.RemoveAt(DeckManager.Deck.Count - 1);
                    }
                }
            }
            else
            {
                int y = 0;
                for (int i = 0; i < 5; i++)
                {
                    if (dealer + i > 4)
                    {
                        for (int x = 0; x < 2; x++)
                        {
                            hraci_inv[y].Add(DeckManager.Deck[DeckManager.Deck.Count - 1]);
                            DeckManager.Deck.RemoveAt(DeckManager.Deck.Count - 1);
                        }

                    }
                    else
                    {
                        for (int j = 0; j < 2; j++)
                        {
                            hraci_inv[dealer + i].Add(DeckManager.Deck[DeckManager.Deck.Count - 1]);
                            DeckManager.Deck.RemoveAt(DeckManager.Deck.Count - 1);
                        }
                        y++;
                    }
                }
            }
            
        }
        else
        {
            
            int NumberOfPlayers = HowManyStillInGame();
            List<bool> holder_list_32 = new List<bool>();
            List<int> kill_list = new List<int>(); 
            holder_list_32.Add(PlayerInventory.PlayerPlayerIN);
            holder_list_32.Add(PlayerInventory.PlayerAI1IN);
            holder_list_32.Add(PlayerInventory.PlayerAI2IN);
            holder_list_32.Add(PlayerInventory.PlayerAI3IN);
            holder_list_32.Add(PlayerInventory.PlayerAI4IN);
            //Console.WriteLine(holder_list_32.Count.ToString());
            int sruz = holder_list_32.Count;
            for(int i = 0; i < sruz;i++)
            {
                //Console.WriteLine("for");

                if (holder_list_32[i] == true)
                {
                    //Console.WriteLine("debug_true");
                }
                else
                {
                    //Console.WriteLine("debug_falseS");
                    kill_list.Add(i);
                }
            }
            kill_list.ForEach(Console.WriteLine);
            for(int y = 0; y < kill_list.Count;y++)
            {
                int z = kill_list[y]-y;
                //Console.WriteLine("z:"+z);
                holder_list_32.RemoveAt(z);
            }
            holder_list_32.ForEach(Console.WriteLine);
            kill_list.Clear();
        }
    }   
    public void StartGameWith5Players()
    {
        HandCards();
    }
    public int HowManyStillInGame()
    {
        List<bool> holder_list_32 = new List<bool>();
        

        int return_value = 0;
        holder_list_32.Add(PlayerInventory.PlayerPlayerIN);
        holder_list_32.Add(PlayerInventory.PlayerAI1IN);
        holder_list_32.Add(PlayerInventory.PlayerAI2IN);
        holder_list_32.Add(PlayerInventory.PlayerAI3IN);
        holder_list_32.Add(PlayerInventory.PlayerAI4IN);
        for(int i = holder_list_32.Count-1; i == 0; i--)
        {
            if (holder_list_32[i])
            {
                return_value++;
            }
        }
        return return_value;

      
    }
  
    public int GetDealerName()
    {
        if (PlayerInventory.PlayerPlayerIsDealer)
        {
            return 1;
        }
        else if(PlayerInventory.PlayerAI1IsDealer)
        {
            return 2;
        }
        else if(PlayerInventory.PlayerAI2IsDealer)
        {
            return 3;
        }
        else if(PlayerInventory.PlayerAI3IsDealer)
        {
            return 4;
        }
        else if(PlayerInventory.PlayerAI4IsDealer)
        {
            return 5;
        }
        else
        {
            return 40;
        }
    }



    public void AssingDealer()
    {
        int dealer = GetDealerName();
        if (CheckIfAll())
        {
            switch (dealer)
            {
                case 1:
                    PlayerInventory.PlayerPlayerIsDealer = false;
                    PlayerInventory.PlayerAI1IsDealer = true;

                    break;
                case 2:
                    PlayerInventory.PlayerAI1IsDealer = false;
                    PlayerInventory.PlayerAI2IsDealer = true;


                    break;
                case 3:
                    PlayerInventory.PlayerAI2IsDealer = false;
                    PlayerInventory.PlayerAI3IsDealer = true;


                    break;
                case 4:
                    PlayerInventory.PlayerAI3IsDealer = false;
                    PlayerInventory.PlayerAI4IsDealer = true;


                    break;
                case 5:
                    PlayerInventory.PlayerAI4IsDealer = false;
                    PlayerInventory.PlayerPlayerIsDealer = true;


                    break;
            }
        }
        else
        {
            PlayerInventory.PlayerPlayerIsDealer = true;
        }
    }
    public bool CheckIfAll()
    {
        if(PlayerInventory.PlayerPlayerIN & PlayerInventory.PlayerAI1IN & PlayerInventory.PlayerAI2IN & PlayerInventory.PlayerAI3IN & PlayerInventory.PlayerAI4IN)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool DoesAnyoneHaveDealerPin()
    {
        if(PlayerInventory.PlayerPlayerIsDealer & PlayerInventory.PlayerAI1IsDealer & PlayerInventory.PlayerAI2IsDealer & PlayerInventory.PlayerAI3IsDealer & PlayerInventory.PlayerAI4IsDealer)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}