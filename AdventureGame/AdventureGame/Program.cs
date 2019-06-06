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
        public struct MurderItems
        {
            public string murderer;
            public string murderWeapon;
            public string murderRoom;
        }

        //List of commands going to be used.
        public struct Commands
        {
            public string USE;       //Use certain items
            public string OPEN;      //Open doors
            public string INVENTORY; //Used for looking at inventory
            public string GOTO;      //Walking to rooms e.g WALK study
            public string ASK;       //Used for asking NPC characters
            public string EXAMINE;   //Examine items like the body 
            public string TAKE;      //Command used for taking items such as keys
            public string GUESS;     //Murderer first, weapon, and then room.
        }



            public static void Main()
        {
            //Setting up variables and arrays
            Random rand = new Random();
            MurderItems[] MurderCards = new MurderItems[1];
            string[] suspectArray = { "Peter Plum", "Miss Scarlet", "Miss White", "Mr. Green", "Colonel Mustard", "Alfred Gray" };
            string[] weaponArray = { "Candlestick", "Dagger", "Lead Pipe", "Revolver", "Suzuki Swift", "Spanner", "Poison" };
            string[] roomArray = { "Kitchen", "Ballroom", "Billiard Room", "Library", "Cellar", "Dining Room", "Hall", "Study" };

            for (int i = 0; i < MurderCards.Length; i++)
            {
                MurderCards[i].murderer = suspectArray[rand.Next(0, 5)];
                MurderCards[i].murderWeapon = weaponArray[rand.Next(0, 5)];
                MurderCards[i].murderRoom = roomArray[rand.Next(0, 5)];
            }
            userInput(MurderCards);
        }

        public static void userInput(MurderItems[] MurderCards)
        {
            //Setting up commands
            Commands[] command = new Commands[1];
            command[0].ASK = "ask";
            command[0].EXAMINE = "examine";
            command[0].GUESS = "guess";
            command[0].INVENTORY = "inventory";
            command[0].OPEN = "open";
            command[0].GOTO = "goto";


            bool loop = true;
            while (loop == true)
            {
                string userInput = Console.ReadLine();
                string[] userArray = userInput.Split(' ');
                int count = 0;
                foreach (string item in userArray)
                {
                    count++;
                }
                if (userArray[0] == "goto")
                {
                    MovingRoom(userArray, MurderCards);
                }
                if (userArray[0] == "exit")
                {
                    Environment.Exit(0);
                }
            }
        }
        public static void MovingRoom(string[] userArray, MurderItems[] MurderCards)
        {
            if (userArray.Contains("Ballroom"))
            {
                Console.WriteLine("Ballroom");
                Console.ReadLine();
            }

            
                if (userArray.Contains("Dining"))
                {
                    Console.WriteLine("You enter a Conservatory, You are dissappointed");
                    Console.ReadLine();
                }
                if (userArray.Contains("Billiard"))
                {
                    Console.WriteLine("The Billiard Room smells like chalk");
                    Console.ReadLine();

                }
            if (userArray.Contains("Cellar"))
            {
                Guessing(MurderCards);

            }        
        }

        public static void Guessing(MurderItems[] MurderCards)
        {
            int points = 0;
            Console.Write("Please enter who you think the murderer was: ");
            string userGuess = Console.ReadLine();
            if (MurderCards[0].murderer == userGuess)
            {
                points++;
            }
            Console.Write("Please enter what weapon you think was used: ");
            userGuess = Console.ReadLine();
            if (MurderCards[0].murderWeapon == userGuess)
            {
                points++;
            }
            Console.Write("Please enter what room the murder was commited in: ");
            userGuess = Console.ReadLine();
            if (MurderCards[0].murderRoom == userGuess)
            {
                points++;
            }

            if (points == 3)
            {
                Console.WriteLine("You win, you figured out who the murderer was!");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("You lost");
                Console.WriteLine(MurderCards[0].murderer);
                Console.WriteLine(MurderCards[0].murderRoom);
                Console.WriteLine(MurderCards[0].murderWeapon);
                Console.ReadLine();
            }
        }
    }
}
