Console.Clear();


int money = 100;
int buyIn = 10;
int pot = 0;
BlackJack.Typing("Welcome to Blackjack!");
BlackJack.Typing("Do you want to play? (y/n)");
bool playing = System.Console.ReadLine() == "y";
bool hitting;
bool playerBust;
BlackJack myGame = new BlackJack();
List<string> playerHand = new();
List<string> dealerHand = new();
myGame.BuildDeck();

while (playing)
{
    if(money < buyIn)
    {
        BlackJack.Typing($"You have ${money}, you can't afford to play. Get a job? (y/n)");
        if (System.Console.ReadLine() != "n")
        {
            money += 100;
            BlackJack.Typing($"You worked at Wendys for the day and now have ${money}");
            continue;
        }

        else
        {
            BlackJack.Typing("That's too bad");
            break;
        }
    }
    
    BlackJack.Typing($"You have ${money}. You put in ${buyIn}.");
    money = money - buyIn;
    pot += buyIn*2;

    if(myGame.deck.Count < 10)
    {
        myGame.BuildDeck();
    }
    playerBust = false;
    
    BlackJack.Typing($"There are {myGame.deck.Count} cards in the deck");
    playerHand = myGame.Draw(2);
    dealerHand = myGame.Draw(2);
    int playerScore = BlackJack.GetScore(playerHand,playerHand.Count);
    int dealerScore = BlackJack.GetScore(dealerHand,1);
    BlackJack.Typing($"Current pot is: ${pot}");
    BlackJack.Typing($"Your cards are ", false);
    foreach(string card in playerHand)
    {
        BlackJack.Typing($"{card} ", false);
    }
    System.Console.WriteLine("");
    BlackJack.Typing($"For a value of {playerScore}");
    BlackJack.Typing($"Dealer is showing {dealerHand[0]}");
    BlackJack.Typing("Do you want to hit or stay? (h/s)");
    hitting = System.Console.ReadLine() == "h";
    if (hitting)
    {
        BlackJack.Typing("Double down? (y/n)");
        if(System.Console.ReadLine() != "n")
        {
            money = money - buyIn;
            pot = pot*2;
            BlackJack.Typing($"Current pot is: ${pot}");
        }
    }

    while (hitting)
    {
        playerHand.AddRange(myGame.Draw());
        playerScore = BlackJack.GetScore(playerHand,playerHand.Count);
        BlackJack.Typing("You have ", false);
        foreach(string card in playerHand)
        {
            BlackJack.Typing($"{card} ", false);
        }
        System.Console.WriteLine("");
        BlackJack.Typing($"For a total value of {playerScore}");
        if(playerScore > 21)
        {
            playerBust = true;
            
            BlackJack.Typing($"You bust! Dealer wins! You lost ${pot/2} and have ${money} remaining. Play again? (y/n)");
            if(Console.ReadLine() != "y")
            {
                playing = false;
            }
            
            break;
        }
        else
        {
            BlackJack.Typing("Hit again? (h/s)");
            hitting = Console.ReadLine() == "h";
        }
        
    }
    
    if (playerBust)
    {
        Console.Clear();
        pot=0;
        continue;
    }

    dealerScore = BlackJack.GetScore(dealerHand,dealerHand.Count);
    BlackJack.Typing($"Dealer flips",false);
    BlackJack.Typing("...");


    while (1==1)
    {

        BlackJack.Typing($"Dealer has ",false);
        foreach(string card in dealerHand)
        {
            BlackJack.Typing($"{card} ",false);
        }
        System.Console.WriteLine("");
        BlackJack.Typing($"for a total of {dealerScore}");
        
        
        if(dealerScore > 21)
        {
            BlackJack.Typing($"Dealer busts! You win ${pot}! Play again? (y/n)");
            money += pot;
            playing = Console.ReadLine() == "y";
            pot = 0;
            break;

        }

        else if(dealerScore == 21)
        {
            if(playerScore == 21)
            {
                BlackJack.Typing($"Dealer wins ties! You lost ${pot/2} and have ${money} remaining. Play again? (y/n)");
                playing = Console.ReadLine() == "y";
                pot = 0;
                break;
            }
            else
            {
                BlackJack.Typing($"Dealer has 21! Dealer Wins! You lost ${pot/2} and have ${money} remaining. Play again? (y/n)");
                playing = Console.ReadLine() == "y";
                pot = 0;
                break;
            }
        }
        
        else if(dealerScore == playerScore)
        {
            BlackJack.Typing($"Dealer stays. Dealer wins ties! You lost ${pot/2} and have ${money} remaining. Play again? (y/n)");
            playing = Console.ReadLine() == "y";
            pot = 0;
            break;
        }
        
        else if(dealerScore > playerScore)
        {
            BlackJack.Typing($"Dealer stays. Dealer wins! You lost ${pot/2} and have ${money} remaining. Play again? (y/n)");
            playing = Console.ReadLine() == "y";
            pot = 0;
            break;
        }
        
        
        else
        {
            BlackJack.Typing("Dealer hits");
            dealerHand.AddRange(myGame.Draw());
            dealerScore = BlackJack.GetScore(dealerHand,dealerHand.Count);
        }

    }





    Console.Clear();


}

    if (!playing)
    {
        System.Console.WriteLine("Press any key to exit...");
        System.Console.ReadKey(true);
        
    }

















public class BlackJack
{
    
    List<string> suits = ["♠","♣","♦","♥"];
    List<string> cardValues = ["2","3","4","5","6","7","8","9","10","J","Q","K","A"];

   public List<string> deck = new();

    
    public static void Typing(string text, bool newLine = true)
    {
        
        if(text == "...")
        {
            foreach(char character in text)
            {
            Console.Write(character);
            Thread.Sleep(750);
            }
            if (newLine)
            {
                System.Console.WriteLine();
            }
        }

        else
        {
            foreach(char character in text)
            {
                Console.Write(character);
                Thread.Sleep(30);
            }
            if (newLine)
            {
                System.Console.WriteLine();
            }
        }


    }
    
    
    public void BuildDeck()
    {
        deck.Clear();
        foreach(string suit in suits)
        {
            foreach(string value in cardValues)
            {
                deck.Add(value+suit);
            }
        }
    }


    public List<string> Draw(int n = 1)
    {
        
        List<string> hand = new();
        while(n > 0)
        {
            
            Random rng = new();
            int index = rng.Next(deck.Count -1);
            hand.Add(deck[index]);
            deck.RemoveAt(index);
            n--;
        }
        return hand;

    }


    public static int GetScore(List<string> hand, int numOfCards)
    {
        int score = 0;
        int aceCount = 0;
        List<string> royals = ["J","Q","K"];
        while(numOfCards > 0)
        {
            if (royals.Contains(hand[numOfCards-1][..^1]))
            {
                score+= 10;
            }

            else if(hand[numOfCards-1][..^1] == "A")
            {
                aceCount += 1;
            }

            else
            {
                score += int.Parse(hand[numOfCards-1][..^1]);
            }
            
            numOfCards --;
        }

        while(aceCount > 0)
        {
            if(score > 10)
            {
                score += 1;
            }
            else
            {
                score += 11;
            }
            aceCount --;
        }
        
        return score;
    }

}

