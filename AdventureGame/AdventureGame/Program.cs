using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureGame
{
    class Program
    {
        public struct Murderer
        {
            public string murderer;
            public string murderWeapon;
            public string murderRoom;
        }


        public static void Start()
        {
            //Setting up variables and arrays
            Random rand = new Random();
            Murderer[] MurderCards = new Murderer[1];

            string[] suspectArray = { "Peter Plum", "Miss Scarlet", "Miss White", "Mr. Green", "Colonel Mustard", "Alfred Gray" };
            string[] weaponArray = { "Candlestick", "Dagger", "Lead Pipe", "Revolver", "Rope", "Spanner", "Poison" };
            string[] roomArray = { "Kitchen", "Ballroom", "Conservatory", "Billiard Room", "Library", "Cellar", "Dining Room", "Lounge", "Hall", "Study" };

            //Initilising the cards used in the murder
            for (int i = 0; i < MurderCards.Length; i++)
            {
                MurderCards[i].murderer = suspectArray[rand.Next(0, 5)];
                MurderCards[i].murderWeapon = weaponArray[rand.Next(0, 6)];
                MurderCards[i].murderRoom = roomArray[rand.Next(0, 9)];
            }
            Console.ReadLine();

        }

        static void Main()
        {
            Start();
        }
    }
}
