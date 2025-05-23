public class Card
{
    //Fields, example: Rank rank;
    //check the help documentation for the fields
    Rank rank;
    Suit suit;

    //Card Constructor
    public Card(Rank rank, Suit suit)
    {
        this.rank = rank;
        this.suit = suit;
    }

    //Define properties for all above fields
    //code example: public Suit Suit { get { return suit; } }

    public Rank Rank
    {
        get
        {
            return rank;
        }
    }

    public Suit Suit
    {
        get
        {
            return suit;
        }
    }

    public int getRankNum()
    {
        return ((int)rank) + 1;
    }

    public string SuitSymbol(bool fill = false)
    {
        switch (suit)
        {
            case Suit.Spades:
                return fill ? "♠" : "♤";
            case Suit.Clubs:
                return fill ? "♣" : "♧";
            case Suit.Hearts:
                return fill ? "♥" : "♡";
            case Suit.Diamonds:
                return fill ? "♦" : "♢";
        }
        return null;
    }

    public string RankAbbreviated()
    {
        int num = getRankNum();
        if (num == 1) return "A";
        if (num < 11) return num.ToString();
        return rank.ToString().Substring(0, 1);
    }
}
