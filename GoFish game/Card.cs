using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoFish_game
{
    class Card: IComparable<Card>
    {
        public Values value { get; private set; }
        public Suits suit { get; private set; }
        public Card(Suits suit, Values value)
        {
            this.value = value;
            this.suit = suit;
        }
        public string ShowCard
        {
            get
            {
                return value.ToString() + " of " + suit.ToString();
            }
        }
        public override string ToString()
        {
            return ShowCard;
        }

        public int CompareTo(Card other)
        {
            if (this.value > other.value)
                return 1;
            else if (this.value < other.value)
                return -1;
            else if (this.suit > other.suit)
                return 1;
            else if (this.suit < other.suit)
                return -1;
            else return 0;
        }
        public static string Plural(Values value)
        {
            if (value == Values.six)
                return "Sixes";
            else
                return value.ToString() + "s";
        }
    }
}
