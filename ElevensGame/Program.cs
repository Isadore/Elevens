class Game
{
    private int pairvalue;
    private Deck deck;
    private List<Card> CardTable;
    private List<int> SelectedCards;
    public Game(int pairvalue = 11)
    {
        this.pairvalue = pairvalue;
    }

    public void GameLoop() { }
    public void DisplayTable() { }
    public bool CheckSelectedCards()
    {
        if (SelectedCards.Count == 2)
        {
            CardTable.ElementAt(SelectedCards.First());
        } else {}
        return false;
    }
    
    

}