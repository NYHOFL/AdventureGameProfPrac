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
        public struct PlayerInventory
        {
            public string Slot1;
            public string Slot2;
            public string Slot3;
            public string Slot4;
            public string Slot5;
        }
        public static void Main()
        {
            
            //Setting up variables and arrays
            Random rand = new Random();
            MurderItems[] MurderCards = new MurderItems[1];
            string[] suspectArray = { "Peter Plum", "Miss Scarlet", "Miss White", "Mr. Green", "Colonel Mustard", "Alfred Gray" };
            string[] weaponArray = { "Candlestick", "Dagger", "Lead Pipe", "Revolver", "Suzuki Swift", "Spanner", "Poison" };
            string[] roomArray = { "Kitchen", "Ballroom", "Billiard", "Library",  "Dining", "Hall", "Study" };

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
            PlayerInventory[] Inventory = new PlayerInventory[1];
            Inventory[0].Slot1 = "Empty";
            Inventory[0].Slot2 = "Empty";
            Inventory[0].Slot3 = "Empty";
            Inventory[0].Slot4 = "Empty";
            Inventory[0].Slot5 = "Empty";


            Console.WriteLine(MurderCards[0].murderRoom);
            bool loop = true;
            while (loop == true)
            {
                Console.Write("Please enter what you want to do: ");
                Console.ForegroundColor = ConsoleColor.Green;
                string userInput = Console.ReadLine();
                Console.WriteLine("***************************************************************************************");
                Console.ForegroundColor = ConsoleColor.White;
                string[] userArray = userInput.Split(' ');

                //GoTo Command
                if (userArray[0] == "goto")
                {
                    MovingRoom(userArray, MurderCards, Inventory);
                }

                //Inventory Command
                else if (userArray[0] == "inventory")
                {
                    Console.WriteLine(Inventory[0].Slot1);
                    Console.WriteLine(Inventory[0].Slot2);
                    Console.WriteLine(Inventory[0].Slot3);
                    Console.WriteLine(Inventory[0].Slot4);
                    Console.WriteLine(Inventory[0].Slot5);
                }

                //Exit Command
                else if (userArray[0] == "exit")
                {
                    Console.WriteLine("You failed to solve the case. You disgrace your academy and are sent back in shame.");
                    Console.ReadLine();
                    Environment.Exit(0);
                    
                }

                //Error Checking
                else
                {
                    Console.Write("Unknown command, please try again: ");
                }
            }
        }
        public static void MovingRoom(string[] userArray, MurderItems[] MurderCards, PlayerInventory[] Inventory)
        {
            //Entering ballroom - Not murder room
            if ((userArray.Contains("Ballroom"))&&((Inventory[0].Slot1 == "Ballroom Key")||(Inventory[0].Slot2 == "Ballroom Key") || (Inventory[0].Slot3 == "Ballroom Key") || (Inventory[0].Slot4 == "Ballroom Key") || (Inventory[0].Slot5 == "Ballroom Key")))
            {
                Console.WriteLine("You have the Ballroom key, collected from one of the suspects.");
                if(MurderCards[0].murderRoom == "Ballroom")
                {
                    Console.WriteLine("You enter the room where the murdered suspect lies");
                }
                else { 
                Console.WriteLine("You enter the ballroom, the room echos with the conversations of distraught guests. No body can be seen.");
                }
                Console.ReadLine();
            }
            
            //Entering Dining Room - Not murder room
            else if (userArray.Contains("Dining"))
            {
               
                if (MurderCards[0].murderRoom == "Dining")
                {
                    Console.WriteLine("You enter the room where the murdered suspect lies");
                }
                else
                {
                    Console.WriteLine("You enter the Dining room, You are dissappointed. No corpse can be seen");
                }
                Console.WriteLine("There is a man standing in the corner, frightened. You ask him if he knows anything. He replies 'I'm Sorry I didn't see anything, but I may of heard something coming from the Ballroom'");
                Console.WriteLine("You thank him, and he gives you the key to the Ballroom.");
                Inventory[0].Slot1 = "Ballroom Key";
            }

            //Entering Billiard Room - Not murder room
            else if (userArray.Contains("Billiard"))
            {
                if (MurderCards[0].murderRoom == "Billiard")
                {
                    Console.WriteLine("You enter the room where the murdered suspect lies");
                }
                else
                {
                    Console.WriteLine("You enter the Billiard room, the room is dimly lit. No body can be seen");
                }

            }

            //Entering Kitchen Room - Not murder room
            else if (userArray.Contains("Kitchen"))
            {
                if (MurderCards[0].murderRoom == "Kitchen")
                {
                    Console.WriteLine("You enter the room where the murdered suspect lies");
                }
                else
                {
                    Console.WriteLine("You enter the kitchen, the smell of the recently cooked turkey dinner looms in the air. No body can be seen.");
                }
                
                Console.ReadLine();

            }

            //Entering Cellar Room - Guessing Room
            else if (userArray.Contains("Cellar"))
            {
                Console.WriteLine("You enter the Cellar and find the cheif investagator. You interupt his conversation confinatly saying you know how the murder went down. ");
                Guessing(MurderCards);

            }
            else
            {
                Console.WriteLine("You dont have the key to enter that room");
            }
        }

        public static void Guessing(MurderItems[] MurderCards)
        {
            int points = 0;
            Console.Write("He asks who you think the murderer was, you reply: ");
            string userGuess = Console.ReadLine();
            if (MurderCards[0].murderer == userGuess)
            {
                points++;
            }
            Console.Write("He asks what weapon you think you think was used: ");
            userGuess = Console.ReadLine();
            if (MurderCards[0].murderWeapon == userGuess)
            {
                points++;
            }
            Console.Write("He asks what room you think you think the murder was commited in: ");
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
