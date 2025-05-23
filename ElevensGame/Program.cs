using System.Text;

public class Game
{
    private int pairvalue;
    private Deck deck;
    private List<Card> CardTable;
    private List<(Card, int)> SelectedCards; // (Card, CardTable Index)
    public Game(int pairvalue = 11)
    {
        this.pairvalue = pairvalue;
        deck = new();
        CardTable = new();
        SelectedCards = new();
        deck.Shuffle();
    }
    private int CardHoveringIndex = 0;

    public void GameLoop()
    {
        Console.OutputEncoding = Encoding.UTF8;
        for (int i = 0; i < 9; i++) CardTable.Add(deck.TakeTopCard());
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Cards remaining in deck: " + deck.Cards.Count);
            DisplayTable();
            ConsoleKey k = Console.ReadKey().Key;
            if (k == ConsoleKey.DownArrow)
            {
                if (CardHoveringIndex < 6) CardHoveringIndex += 3;
            }
            else if (k == ConsoleKey.RightArrow)
            {
                if ((CardHoveringIndex + 1) % 3 != 0) CardHoveringIndex++;
            }
            else if (k == ConsoleKey.UpArrow)
            {
                if (CardHoveringIndex > 2) CardHoveringIndex -= 3;
            }
            else if (k == ConsoleKey.LeftArrow)
            {
                if ((CardHoveringIndex + 1) % 3 != 1) CardHoveringIndex--;
            }
            else if (k == ConsoleKey.Spacebar)
            {
                if (!isCardSelected(CardHoveringIndex))
                {
                    SelectedCards.Add((CardTable[CardHoveringIndex], CardHoveringIndex));
                }
                else
                {
                    SelectedCards.RemoveAll(ci => ci.Item2 == CardHoveringIndex);
                }
            }
            else if (k == ConsoleKey.Enter)
            {
                if (CheckSelectedCards())
                {
                    foreach ((Card, int) ci in SelectedCards)
                    {
                        
                    }
                }
            }
        }
    }
    public void DisplayTable()
    {
        for (int i = 0; i < CardTable.Count; i++)
        {
            if (i > 0 && i % 3 == 0) Console.WriteLine();
            Card card = CardTable[i];
            Console.Write(string.Format("{0, 2} {1} {2}", card.RankAbbreviated(), card.SuitSymbol(isCardSelected(i)), i == CardHoveringIndex ? "*  " : "   "));
        }
        Console.WriteLine();
    }
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
                if (ci.Item1.Rank == Rank.Queen) queen = true;
            }
            return jack && queen && king;
        }
        return false;
    }

    private bool isCardSelected(int i)
    {

        foreach ((Card, int) ci in SelectedCards)
        {
            if (ci.Item2 == i) return true;
        }
        return false;
    }
}