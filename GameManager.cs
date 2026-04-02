using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;
using main;
using Microsoft.VisualBasic;

class Management
{

    
    public void HandCards()
    {
        int dealer;
        if (CheckIfAll())
        {
            AssingDealer();
            dealer = GetDealerName();
        }
    }   
    public void StartGameWith5Players()
    {
        HandCards();
    }
  
    public int GetDealerName()
    {
        if (inventory.PlayerPlayerIsDealer)
        {
            return 1;
        }
        else if(inventory.PlayerAI1IsDealer)
        {
            return 2;
        }
        else if(inventory.PlayerAI2IsDealer)
        {
            return 3;
        }
        else if(inventory.PlayerAI3IsDealer)
        {
            return 4;
        }
        else if(inventory.PlayerAI4IsDealer)
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
                    inventory.PlayerPlayerIsDealer = false;
                    inventory.PlayerAI1IsDealer = true;

                    break;
                case 2:
                    inventory.PlayerAI1IsDealer = false;
                    inventory.PlayerAI2IsDealer = true;


                    break;
                case 3:
                    inventory.PlayerAI2IsDealer = false;
                    inventory.PlayerAI3IsDealer = true;


                    break;
                case 4:
                    inventory.PlayerAI3IsDealer = false;
                    inventory.PlayerAI4IsDealer = true;


                    break;
                case 5:
                    inventory.PlayerAI4IsDealer = false;
                    inventory.PlayerPlayerIsDealer = true;


                    break;
            }
        }
        else
        {
            inventory.PlayerPlayerIsDealer = true;
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