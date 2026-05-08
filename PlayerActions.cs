using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using Newtonsoft.Json;


namespace main;

class PlayerActions
{
    public  int ShallWePlay()
    {
        IniPlayers.TimeToChooseMRFreeman = 0;
        IniPlayers.IsChoosen = true;
        while(IniPlayers.TimeToChooseMRFreeman == 0)
        {
           // Console.WriteLine("WE coosing");
        }
        if(IniPlayers.TimeToChooseMRFreeman == 1)
        {
            IniPlayers.IsChoosen = false;
            return 0;
        }
        else if(IniPlayers.TimeToChooseMRFreeman == 2)
        {
            IniPlayers.IsChoosen = false;
            return 1;
        }
        else
        {
            IniPlayers.IsChoosen = false;
            return 0;
        }
        
        
        
    }
}