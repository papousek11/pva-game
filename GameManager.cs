using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;
using main;
using Microsoft.VisualBasic;

class Management
{
    PlayerInventory inventory = new PlayerInventory();
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
        if(inventory.PlayerPlayerIN & inventory.PlayerAI1IN & inventory.PlayerAI2IN & inventory.PlayerAI3IN & inventory.PlayerAI4IN)
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
        if(inventory.PlayerPlayerIsDealer & inventory.PlayerAI1IsDealer & inventory.PlayerAI2IsDealer & inventory.PlayerAI3IsDealer & inventory.PlayerAI4IsDealer)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}