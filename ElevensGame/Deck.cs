public class Deck
{
    List<Card> cards = new List<Card>();

    //Deck Constructor
    public Deck()
    {
        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            foreach (Rank rank in Enum.GetValues(typeof(Rank)))
            {
               cards.Add(new Card(rank, suit));
            }
        }
    }

    //Implement a property to get Cards
    
    public List<Card> Cards {
        get {return cards;}
    }

    //Takes top card from deck (if deck is not empty, otherwise return null)
    public Card TakeTopCard()
    {
        if(cards.Count > 0) {
            Card top = cards.First();
            cards.RemoveAt(0);
            return top;
        }
        return null;
    }

    //Shuffle Method
    public void Shuffle()
    {
        Random r = new Random();
        List<Card> shuffled = new List<Card>();
        while(cards.Count > 0) {
            int i = r.Next(cards.Count);
            shuffled.Add(cards[i]);
            cards.RemoveAt(i);
        }
        cards = shuffled;
    }

    //Cut method
    public void Cut(int index)
    {
        if(cards.Count > (index+1)) {
            while(index > 0) {
                cards.Add(cards[0]);
                cards.RemoveAt(0);
                index--;
            }
        }
    }

    public void Display() {
        for(int i = 0; i < cards.Count; i++) {
            Card c = cards[i];
            int ranknum = ((int)c.Rank)+1;
            Console.WriteLine($"Card #{i+1}: {c.Rank}({ranknum}) of {c.Suit}");
        }
    }
}

