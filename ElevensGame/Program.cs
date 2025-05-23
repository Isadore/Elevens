class Game
{
    private int pairvalue;
    private Deck deck;
    private List<Card> CardTable;
    private List<(Card, int)> SelectedCards; // (Card, CardTable Index)
    public Game(int pairvalue = 11)
    {
        this.pairvalue = pairvalue;
    }

    public void GameLoop() { }
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
    
    

}