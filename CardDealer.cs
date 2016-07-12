using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardPlayer
{
    public class Card{

        private int num;
        private string name;
        private string suit;

        public Card(int number, string suit)
        {
            num = number;
            name = getName(num);
            setSuit(suit);
        }

        public int getNumber()
        {
            return num;
        }

        public string getSuit()
        {
            return suit;
        }

        public string getName()
        {
            return name;
        }

        private void setSuit(string suit)
        {
            if (suit.Equals("heart"))
            {
                this.suit = "♥";
            }
            else if (suit.Equals("diamond"))
            {
                this.suit = "♦";
            }
            else if (suit.Equals("club"))
            {
                this.suit = "♣";
            }
            else if (suit.Equals("spade"))
            {
                this.suit = "♠";
            }
        }

        private string getName(int number)
        {
            if(number == 11)
            {
                return "Jack";
            }else if(number == 12)
            {
                return "Queen";
            }else if(number == 13)
            {
                return "King";
            }else if(number == 14)
            {
                return "Ace";
            }else
            {
                return Convert.ToString(number);
            }
        }

    }

    static class Program
    {
        // online shuffle algorithm
        static Random _random = new Random();
        private static void Shuffle<T>(this List<T> list)
        {
            int n = list.Count;
            while( n > 0)
            {
                n--;
                int k = _random.Next(n + 1);
                T Value = list[k];
                list[k] = list[n];
                list[n] = Value;
            }
        }
        //end online algorithmyes
        

        private static List<Card> createDeck(List<Card> deck)
        {
            string[] suits = { "heart", "diamond", "club", "spade" };
            for(int i = 2; i < 15; i++)
            {
                foreach(string suit in suits){
                    deck.Add(new Card(i, suit));
                }
            }
            return deck;
        }

        private static List<Card> getTopHand(List<Card> deck)
        {
            List<Card> hand = new List<Card>();
            for (int i = 0; i < 5; i++)
            {
                hand.Add(deck.ElementAt(i));
            }
            return hand;
        }

        //Take a shuffled deck for best results
        private static List<List<Card>> get10Hands(List<Card> deck)
        {
            List<List<Card>> allHands = new List<List<Card>>();

            List<Card> h0 = new List<Card>();
            List<Card> h1 = new List<Card>();
            List<Card> h2 = new List<Card>();
            List<Card> h3 = new List<Card>();
            List<Card> h4 = new List<Card>();
            List<Card> h5 = new List<Card>();
            List<Card> h6 = new List<Card>();
            List<Card> h7 = new List<Card>();
            List<Card> h8 = new List<Card>();
            List<Card> h9 = new List<Card>();

            for(int i = 0; i < 50; i = i + 10)
            {
                h0.Add(deck.ElementAt(i));
                h1.Add(deck.ElementAt(i+1));
                h2.Add(deck.ElementAt(i+2));
                h3.Add(deck.ElementAt(i+3));
                h4.Add(deck.ElementAt(i+4));
                h5.Add(deck.ElementAt(i+5));
                h6.Add(deck.ElementAt(i+6));
                h7.Add(deck.ElementAt(i+7));
                h8.Add(deck.ElementAt(i+8));
                h9.Add(deck.ElementAt(i+9));
            }

            allHands.Add(h0);
            allHands.Add(h1);
            allHands.Add(h2);
            allHands.Add(h3);
            allHands.Add(h4);
            allHands.Add(h5);
            allHands.Add(h6);
            allHands.Add(h7);
            allHands.Add(h8);
            allHands.Add(h9);

            return allHands;
        }

        private static bool checkrsf(List<Card> hand)
        {
            string suit = hand.First().getSuit();
            if(hand.Contains(new Card(14, suit)) && hand.Contains(new Card(13, suit)) && hand.Contains(new Card(12, suit)) && hand.Contains(new Card(11, suit)) && hand.Contains(new Card(10, suit)))
            {
                return true;
            }
            return false;
        }

        private static bool checkflush(List<Card> hand)
        {
            string suit = hand.First().getSuit();
            foreach(Card card in hand)
            {
                if (card.getSuit() != suit){
                    return false;
                }
            }
            return true;
        }

        private static void testSingleHand()
        {
            List<Card> deck = new List<Card>();
            createDeck(deck);
            Shuffle(deck);

            int count = 0;
            /*
            foreach (Card card in deck)
            {
                count++;
                Console.WriteLine(string.Format("{0} {1} {2}" , card.getNumber(),  card.getName() , card.getSuit()));
            }*/
            List<Card> hand = new List<Card>();
            hand = getTopHand(deck);
            bool rsf = false;

            while (!rsf)
            {
                Shuffle(deck);
                hand = getTopHand(deck);
                /*foreach (Card card in hand)
                {
                    Console.Write(string.Format("{0}{1} ", card.getName(), card.getSuit()));
                }*/
                bool flush = checkflush(hand);
                if (flush)
                {
                    Console.Write("Flush: ");
                    foreach (Card card in hand)
                    {
                        Console.Write(string.Format("{0}{1} ", card.getName(), card.getSuit()));
                    }
                    Console.Write("\n");
                }

                rsf = checkrsf(hand);
                Console.WriteLine("count = {0}", count);
                count++;
            }

            Console.WriteLine("count = {0}", count);

            foreach (Card card in hand)
            {
                Console.WriteLine(string.Format("Royal Straight Flush: {0} {1}", card.getName(), card.getSuit()));
            }
        }

        private static void display10Hands()
        {
            List<Card> deck = new List<Card>();
            createDeck(deck);
            Shuffle(deck);

            List<List<Card>> hands = get10Hands(deck);
            int count = 0;
            foreach(List<Card> hand in hands)
            {
                sort(hand);
                Console.Write(string.Format("Hand {0}: ", ++count));

                foreach(Card card in hand)
                {
                    Console.Write(string.Format("{0}{1} ", card.getName(), card.getSuit()));
                }
                Console.Write("\n");
            }
        }

        private static void sort(List<Card> hand)
        {
            List<Card> sorted = hand.OrderByDescending(o => o.getNumber()).ToList();
        }

        static void Main(string[] args)
        {
            //testSingleHand();
            display10Hands();
        }
    }
}
