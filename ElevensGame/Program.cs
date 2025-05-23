using System.Text;

public class Game
{
    private int pairvalue;
    private Deck deck;
    private Card?[] CardTable;
    private List<(Card, int)> SelectedCards; // (Card, CardTable Index)
    private int Wins;
    private int Losses;
    private int CardHoveringIndex;
    private bool ShowInstructions;
    public Game(int pairvalue = 11)
    {
        this.pairvalue = pairvalue;
        deck = new();
        CardTable = new Card[9];
        SelectedCards = new();
        deck.Shuffle();
        CardHoveringIndex = 4;
        Console.OutputEncoding = Encoding.UTF8;
        ShowInstructions = true;
    }
    

    public void GameLoop()
    {
        RestartGame();
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Elevens Card Game");
            if (ShowInstructions)
            {
                Console.WriteLine();
                Console.WriteLine("Game Rules:\n\tTo win the game you must successfully remove all cards from the 9 card table and the deck.\n\tYou can remove a pair of cards from Ace to 10 that adds up to 11 or a trio containing a Jack, Queen and King.");
                Console.WriteLine("Game Controls:\n\tNavigate card table - Arrow Keys\n\tSelect Card - Space Bar\n\tCheck Selection - Enter\n\tDeselect All - X\n\tShow/Hide Game Instructions - H\n\tQuit Game - Q");
                Console.WriteLine();
            }
            Console.WriteLine("Wins: " + Wins + " Losses: " + Losses);
            Console.WriteLine("Cards remaining in deck: " + deck.Cards.Count);
            DisplayTable();
            if (CheckForWin())
            {
                Console.WriteLine("You won! Press Enter to continue.");
                Wins++;
                Console.ReadLine();
                RestartGame();
            }
            else if (!TableContainsValidCombo())
            {
                Console.WriteLine("Game over. No valid card combinations remain on the table. Press Enter to continue.");
                Losses++;
                Console.ReadLine();
                RestartGame();
            }
            else
            {
                ReadUserKeyInput();
            }
        }
    }
    public void RestartGame()
    {
        deck = new();
        deck.Shuffle();
        //Set card table initially
        for (int i = 0; i < 9; i++) CardTable[i] = deck.TakeTopCard();
        if (!TableContainsValidCombo())
        {
            RestartGame();
        }
        else
        {
            CardHoveringIndex = 4;
            SelectedCards.Clear();
        }
    }
    public void ReadUserKeyInput()
    {
        ConsoleKey k = Console.ReadKey().Key;
        //Move down on card table
        if (k == ConsoleKey.DownArrow)
        {
            if (CardHoveringIndex < 6) CardHoveringIndex += 3;
        }
        //Move right on card table
        else if (k == ConsoleKey.RightArrow)
        {
            if ((CardHoveringIndex + 1) % 3 != 0) CardHoveringIndex++;
        }
        //Move up on card table
        else if (k == ConsoleKey.UpArrow)
        {
            if (CardHoveringIndex > 2) CardHoveringIndex -= 3;
        }
        //Move left on card table
        else if (k == ConsoleKey.LeftArrow)
        {
            if ((CardHoveringIndex + 1) % 3 != 1) CardHoveringIndex--;
        }
        //Select a card
        else if (k == ConsoleKey.Spacebar)
        {
            if (CardTable[CardHoveringIndex] != null)
            {
                if (!IsCardSelected(CardHoveringIndex))
                {
                    SelectedCards.Add((CardTable[CardHoveringIndex], CardHoveringIndex));
                }
                else
                {
                    SelectedCards.RemoveAll(ci => ci.Item2 == CardHoveringIndex);
                }
            }
        }
        //Evaluate selected cards
        else if (k == ConsoleKey.Enter)
        {
            foreach ((Card, int) ci in SelectedCards) Console.WriteLine(ci.Item2);
            if (CheckSelectedCards())
            {
                int cardstoadd = SelectedCards.Count;
                if (deck.Cards.Count < cardstoadd) cardstoadd = deck.Cards.Count;
                foreach ((Card, int) ci in SelectedCards)
                {
                    if (cardstoadd > 0)
                    {
                        CardTable[ci.Item2] = deck.TakeTopCard();
                    }
                    else
                    {
                        CardTable[ci.Item2] = null;
                    }
                }
                SelectedCards.Clear();
            }
        }
        //Clear selected cards
        else if (k == ConsoleKey.X)
        {
            SelectedCards.Clear();
        }
        else if (k == ConsoleKey.H)
        {
            ShowInstructions = !ShowInstructions;
        }
        //Quit app
        else if (k == ConsoleKey.Q)
        {
            Environment.Exit(0);
        }
    }
    //Prints 9 card table to console
    public void DisplayTable()
    {
        for (int i = 0; i < CardTable.Length; i++)
        {
            if (i > 0 && i % 3 == 0) Console.WriteLine();
            Card? card = CardTable[i];
            if (card != null)
            {
                Console.Write(string.Format("{0, 2} {1} {2}", card.RankAbbreviated(), card.SuitSymbol(IsCardSelected(i)), i == CardHoveringIndex ? "*  " : "   "));
            }
            else
            {
                Console.Write(string.Format("{0, 2} {1} {2}", "", " ", i == CardHoveringIndex ? "*  " : "   "));
            }
        }
        Console.WriteLine();
    }
    //Checks if selected cards are a valid 11 pair or JQK trio
    private bool CheckSelectedCards()
    {
        if (SelectedCards.Count == 2)
        {
            int ranksum = SelectedCards[0].Item1.getRankNum() + SelectedCards[1].Item1.getRankNum();
            return ranksum == pairvalue;
        }
        if (SelectedCards.Count == 3)
        {
            bool jack = false;
            bool queen = false;
            bool king = false;
            foreach ((Card, int) ci in SelectedCards)
            {
                if (ci.Item1.Rank == Rank.Jack) jack = true;
                if (ci.Item1.Rank == Rank.Queen) queen = true;
                if (ci.Item1.Rank == Rank.King) king = true;
            }
            return jack && queen && king;
        }
        return false;
    }
    //Checks if the table contains and valid pairs or trios
    private bool TableContainsValidCombo()
    {
        bool jack = false;
        bool queen = false;
        bool king = false;
        for (int i = 0; i < CardTable.Length; i++)
        {
            Card? c = CardTable[i];
            if (c != null)
            {
                if (c.Rank == Rank.Jack) jack = true;
                else if (c.Rank == Rank.Queen) queen = true;
                else if (c.Rank == Rank.King) king = true;
                else if (i < (CardTable.Length - 1))
                {
                    int currval = c.getRankNum();
                    for (int j = i + 1; j < CardTable.Length; j++)
                    {
                        Card? c1 = CardTable[j];
                        if (c1 != null && c1.getRankNum() + currval == 11) return true;
                    }
                }
            }
            if (jack && queen && king) return true;
        }
        return false;
    }
    //Check if table and deck are empty for user win
    private bool CheckForWin()
    {
        if (deck.Cards.Count == 0)
        {
            foreach (Card? c in CardTable)
            {
                if (c != null) return false;
            }
            return true;
        }
        return false;
    }
    //Check by table index if card is currently selected
    private bool IsCardSelected(int i)
    {

        foreach ((Card, int) ci in SelectedCards)
        {
            if (ci.Item2 == i) return true;
        }
        return false;
    }
}