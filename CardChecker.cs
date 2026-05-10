using main;




namespace main;

class CardChecking()
{
    public int CheckValue(int globus)
    {
         //join player hand and table
    List<string> allCards = new List<string>();

    allCards.AddRange(IniPlayers.Inventories[globus].PlayerCards);
    allCards.AddRange(IniPlayers.Table);

    List<int> values = new List<int>();
    List<string> suits = new List<string>();

    //split card int readable datA
    foreach (string card in allCards)
    {
        
        // S_Black_A

        string[] split = card.Split('_');

        string suit = split[0];
        string valueString = split[2];

        suits.Add(suit);

        int value = 0;

        if (valueString == "2")
        {
            value = 2;
        }
        else if (valueString == "3")
        {
            value = 3;
        }
        else if (valueString == "4")
        {
            value = 4;
        }
        else if (valueString == "5")
        {
            value = 5;
        }
        else if (valueString == "6")
        {
            value = 6;
        }
        else if (valueString == "7")
        {
            value = 7;
        }
        else if (valueString == "8")
        {
            value = 8;
        }
        else if (valueString == "9")
        {
            value = 9;
        }
        else if (valueString == "10")
        {
            value = 10;
        }
        else if (valueString == "J")
        {
            value = 11;
        }
        else if (valueString == "Q")
        {
            value = 12;
        }
        else if (valueString == "K")
        {
            value = 13;
        }
        else if (valueString == "A")
        {
            value = 14;
        }

        values.Add(value);
    }

    //sort value of cards in list
    values = values.Distinct().OrderBy(v => v).ToList();


    //Dictionaries are wierd they are basicly list in a overcoat
    // count duplicetes
    Dictionary<int, int> counts = new Dictionary<int, int>();

    foreach (int v in values)
    {
        counts[v] = 0;
    }
    //get values
    foreach (string card in allCards)
    {
        string[] split = card.Split('_');

        string valueString = split[2];

        int value = 0;

        if (valueString == "2")
        {
            value = 2;
        }
        else if (valueString == "3")
        {
            value = 3;
        }
        else if (valueString == "4")
        {
            value = 4;
        }
        else if (valueString == "5")
        {
            value = 5;
        }
        else if (valueString == "6")
        {
            value = 6;
        }
        else if (valueString == "7")
        {
            value = 7;
        }
        else if (valueString == "8")
        {
            value = 8;
        }
        else if (valueString == "9")
        {
            value = 9;
        }
        else if (valueString == "10")
        {
            value = 10;
        }
        else if (valueString == "J")
        {
            value = 11;
        }
        else if (valueString == "Q")
        {
            value = 12;
        }
        else if (valueString == "K")
        {
            value = 13;
        }
        else if (valueString == "A")
        {
            value = 14;
        }

        counts[value]++;
    }

    bool pair = false;
    bool twoPair = false;
    bool three = false;
    bool four = false;

    int pairCount = 0;

    foreach (var PairValue in counts)
    {
        if (PairValue.Value == 2)
        {
            pair = true;
            pairCount++;
        }

        if (PairValue.Value == 3)
        {
            three = true;
        }

        if (PairValue.Value == 4)
        {
            four = true;
        }
    }

    if (pairCount >= 2)
    {
        twoPair = true;
    }

    //this line should explaint itself
    bool flush = false;

    int spades = suits.Count(s => s == "S");
    int hearts = suits.Count(s => s == "H");
    int diamonds = suits.Count(s => s == "D");
    int clubs = suits.Count(s => s == "C");

    if (
        spades >= 5 ||
        hearts >= 5 ||
        diamonds >= 5 ||clubs >= 5
    )
    {
        flush = true;
    }

    // postupka check i cant dig up how is it in english
    bool straight = false;

    for (int i = 0; i < values.Count - 4; i++)
    {
        if (
            values[i] + 1 == values[i + 1] &&
            values[i] + 2 == values[i + 2] &&
            values[i] + 3 == values[i + 3] &&
            values[i] + 4 == values[i + 4]
        )
        {
            straight = true;
            break;
        }
    }

    // Ace low postupka (A 2 3 4 5)
    if (values.Contains(14) && values.Contains(2) && values.Contains(3) && values.Contains(4) &&
        values.Contains(5))
    {
        straight = true;
    }

    // Full House(house md frffr)
    bool fullHouse = false;

    if (three && pair)
    {
        fullHouse = true;
    }

    // Straight Flush
    bool straightFlush = false;

    if (straight && flush)
    {
        straightFlush = true;
    }

    // Royal Flush
    bool royalFlush = false;

    if (
        flush &&
        values.Contains(10) &&
        values.Contains(11) &&
        values.Contains(12) &&
        values.Contains(13) &&
        values.Contains(14)
    )
    {
        royalFlush = true;
    }

    //retunt of the score
    // Higher = better

    if (royalFlush)
    {
        return 1000;
    }
    else if (straightFlush)
    {
        return 900;
    }
    else if (four)
    {
        return 800;
    }
    else if (fullHouse)
    {
        return 700;
    }
    else if (flush)
    {
        return 600;
    }
    else if (straight)
    {
        return 500;
    }
    else if (three)
    {
        return 400;
    }
    else if (twoPair)
    {
        return 300;
    }
    else if (pair)
    {
        return 200;
    }
    else
    {
        //vysoka karta score
        return values.Max();
    }



        
    }
}
