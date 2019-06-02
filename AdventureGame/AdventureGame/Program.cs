using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace AdventureGame
{
    class Program
    {
        public struct Murderer
        {
            public string murderer;
            public string murderWeapon;
            public string murderRoom;
            public string[] inventory;
        }

        public struct Commands
        {
            public string USE;

        }

        public static void Start()
        {
            //Setting up variables and arrays
            Random rand = new Random();
            Murderer[] MurderCards = new Murderer[1];

            string[] suspectArray = { "Peter Plum", "Miss Scarlet", "Miss White", "Mr. Green", "Colonel Mustard", "Alfred Gray" };
            string[] weaponArray  = { "Candlestick", "Dagger", "Lead Pipe", "Revolver", "Rope", "Spanner", "Poison" };
            string[] roomArray    = { "Kitchen", "Ballroom", "Conservatory", "Billiard Room", "Library", "Cellar", "Dining Room", "Lounge", "Hall", "Study" };

            //Initilising the cards used in the murder
            for (int i = 0; i < MurderCards.Length; i++)
            {
                MurderCards[i].murderer     = suspectArray[rand.Next(0, 5)];
                MurderCards[i].murderWeapon = weaponArray[rand.Next(0, 6)];
                MurderCards[i].murderRoom   = roomArray[rand.Next(0, 9)];
            }

        }
        public static void Game()
        {
            string temp;

            Console.WriteLine($"The buzz of your cellphone interrupts a friendly game of croquchet. The sun beams through the tall pine trees, a sweet smell in the air. As chief police investigator a murder scene isn't unknown to you.\n");
            Thread.Sleep(2000);
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
            Console.Clear();

            Console.WriteLine($"Will you discover the murderer? Or will they escape your grasp? \n");
            Thread.Sleep(2000);
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
            Console.Clear();

            Console.WriteLine();
            Console.WriteLine("Loading...");
            Thread.Sleep(4000);
            Console.Clear();

            Console.WriteLine("Arriving on the police cordoned scene you discover six suspects and a large masion.The dirty marble pillars outside hold the pearly white roof up.You escort the suspects into the cellar in the middle of the mansion, down the stairs and onto a old wooden bench.You begin thequestioning.");
            Thread.Sleep(2000);
            Console.ReadLine();

            Console.Clear();

            Console.WriteLine("Interact or leave"); // this is the first menu that will ask the player what they want to do 
            temp = Console.ReadLine();
            // if interact, go to a list of the suspects. if leave, exit to the mansion.
            
        }

        static void Main()
        {
            Start();
            Game();
        }
    }
}
