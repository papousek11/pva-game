using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;
using main;
using Microsoft.VisualBasic;

class Management
{

    
    public void HandCards()
    {
        
    }   
    public void StartGameWith5Players()
    {
        HandCards();
    }
    public int GetDealerName()
    {
        
            
    }
    public void AssingDealer()
    {
        
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