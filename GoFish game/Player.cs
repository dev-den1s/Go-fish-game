using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoFish_game
{
    class Player
    {
        public string name { get; private set; }
        private Random random;
        private Deck cards;
        private TextBox textBoxOnForm;
        public Player(string name, Random random, TextBox textBox)
        {
            this.name = name;
            this.random = random;
            cards = new Deck(new Card[] { });
            textBoxOnForm = textBox;
            textBoxOnForm.Text = $"{this.name} has just joined the game;" + Environment.NewLine;
        }
        public IEnumerable<Values> PullOutBooks()
        {
            List<Values> books = new List<Values>();
            Values value;
            int howMany ;
            for (int i = 2; i <= 14; i++)
            {
                value = (Values)i;
                howMany = 0;
                for(int card = 0; card < cards.Count; card++)
                {
                    if (cards.Peek(card).value == value)
                        howMany++;
                    if (howMany == 4)
                    {
                        books.Add(value);
                        cards.PullOutValues(value);
                    }
                }
            }
            return books;
        }
        public Values GetRandomValue()
        {
            Card randomCard = cards.Peek(random.Next(cards.Count));
            return randomCard.value;
        }
        public Deck DoYouHaveAny(Values value)
        {
            Deck cardsIHave = cards.PullOutValues(value);
            textBoxOnForm.Text += $"{name} has {cardsIHave.Count} { Card.Plural(value)}" + Environment.NewLine;
            return cardsIHave;
        }
        public void AskForACard(IEnumerable <Player> players, int myIndex, Deck stock)
        {
            if (stock.Count > 0)
            {
                if (cards.Count == 0)
                    cards.Add(stock.Deal());
                Values randomValue = GetRandomValue();
                AskForACard(players, myIndex, stock, randomValue);
            }
        }
        public void AskForACard(IEnumerable <Player> players, int myIndex, Deck stock, Values value)
        {
            textBoxOnForm.Text += $"{name} asks if anyone has a {value}" + Environment.NewLine;
            int totalCardsGiven = 0;
            int index = 0;
            foreach(Player player in players)
            {
                if (index != myIndex)
                {
                    Deck CardsGiven = player.DoYouHaveAny(value);
                    totalCardsGiven += CardsGiven.Count;
                    while (CardsGiven.Count > 0)
                        cards.Add(CardsGiven.Deal());
                }
                index++;
            }
            
            if ((totalCardsGiven == 0) && stock.Count > 0) {
                textBoxOnForm.Text += $"{name} must draw from the stock." +Environment.NewLine;
                cards.Add(stock.Deal());
            }
        }
        public int CardCount { get { return cards.Count; } }
        public void TakeCard(Card card) { cards.Add(card); }
        public IEnumerable<string> GetCardNames() { return cards.GetCardNames(); }
        public Card Peek(int cardNumber) { return cards.Peek(cardNumber); }
        public void SortHand() { cards.SortByValue(); }
    }
}
