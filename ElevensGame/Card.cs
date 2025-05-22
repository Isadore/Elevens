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
}
