using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;
using Microsoft.VisualBasic;
using System.Collections.Generic;

namespace main;

class AI1G
{
    public  int ShallWePlay()
    {


        Random rnd = new Random();
        if(rnd.Next(1, 13) < 6)
        {
            return 1;
        }
        else if(rnd.Next(1, 13) == 1)
        {
            return 2;
        }
        else
        {
            return 0;
        }
        
    }
}