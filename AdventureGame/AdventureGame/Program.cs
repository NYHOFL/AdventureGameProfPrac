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
            public string[] inventory;
        }

        //List of commands going to be used.
        public struct Commands
        {
            public string USE; //Use certain items
            public string OPEN; //Open doors
            public string INVENTORY; //Used for looking at inventory
            public string WALK;   //Walking to rooms e.g WALK study
            public string ASK;    //Used for asking NPC characters
            public string EXAMINE;//Examine items like the body 
            public string TAKE;   //Command used for taking items such as keys
            public string GUESS;  //Murderer first, weapon, and then room.

        }

        static void Main()
        {
            userInput();
        }
        public static void InitVariables()
        {
            //Setting up variables and arrays
            Random rand = new Random();
            MurderItems[] MurderCards = new MurderItems[1];

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
        }
        public static void userInput()
        {
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
                if (userArray.Contains("GoTo"))
                {
                    MovingRoom(userArray);
                }
            }
        }
        public static void MovingRoom(string[] userArray)
        {
            if (userArray.Contains("Ballroom"))
            {
                Console.WriteLine("Ballroom");
                Console.ReadLine();
            }
        }

    }
}
