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

        public static void Main()
        {
            //Setting up variables and arrays
            string[] Inventory = {"Empty","Empty","Empty","Empty","Empty"};
            Random rand = new Random();
            MurderItems[] MurderCards = new MurderItems[1];
            string currentRoom = "Outside";
            string[] InnocentCharacter = new string[6];
            string[] suspectArray = { "Peter Plum", "Miss Scarlet", "Miss White", "Mr. Green", "Colonel Mustard", "Alfred Gray", "Anthony Mellon" };
            string[] weaponArray = { "Candlestick", "Dagger", "Lead Pipe", "Revolver", "Spanner", "Poison" };
            string[] roomArray = { "Kitchen", "Ballroom", "Billiard", "Library", "Dining", "Hall", "Study" };

            //Randomising the murderer, weapon and room
            for (int i = 0; i < MurderCards.Length; i++)
            {
                MurderCards[i].murderer = suspectArray[rand.Next(0, 5)];
                MurderCards[i].murderWeapon = weaponArray[rand.Next(0, 5)];
                MurderCards[i].murderRoom = roomArray[rand.Next(0, 5)];
                if (MurderCards[i].murderer == "Anthony Mellon")
                {
                    MurderCards[i].murderWeapon = "2015 Suzuki Swift";
                }
            }

            //error checking for establishing NPCS that are not the murderer
            Console.WriteLine("Murderer: "+MurderCards[0].murderer);
            for (int i = 0; i <= 5; i++)
            {
                bool loop = true;
                while(loop == true) { 
                    if (suspectArray[i] != MurderCards[0].murderer)
                    {
                        InnocentCharacter[i] = suspectArray[i];
                        Console.WriteLine("Innocent: "+InnocentCharacter[i]);
                    }
                    loop = false;
                }
            }
            userInput(MurderCards, Inventory, InnocentCharacter, currentRoom);
        }

        public static void userInput(MurderItems[] MurderCards, string[] Inventory, string[] InnocentCharacter, string currentRoom)
        {
            Console.WriteLine(MurderCards[0].murderRoom);
            bool loop = true;
            while (loop == true)
            {
                Console.Write("Please enter what you want to do: ");
                Console.ForegroundColor = ConsoleColor.Green;
                string userInput = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
                string[] userArray = userInput.Split(' ');

                //GoTo Command
                if (userArray[0] == "goto")
                {
                    MovingRoom(userArray, MurderCards, Inventory, InnocentCharacter, currentRoom);
                }

                //Inventory Command
                else if (userArray[0] == "inventory")
                {
                    for (int i = 0; i < Inventory.Length; i++)
                    {
                        Console.WriteLine("Slot "+i+": "+Inventory[i]); 
                    }
                    
                }

                //Exit Command
                else if (userArray[0] == "exit")
                {
                    Console.WriteLine("You failed to solve the case. You disgrace your academy and are sent back in shame.");
                    Console.ReadLine();
                    Environment.Exit(0);
                }

                //Ask command
                else if (userArray[0] == "ask")
                {
                    Asking(MurderCards, Inventory, InnocentCharacter, currentRoom, userArray);
                }

                //Error Checking
                else
                {
                    Console.Write("Unknown command, please try again: ");
                }

                
            }
        }
        public static void Asking(MurderItems[] MurderCards, string[] Inventory, string[] InnocentCharacter, string currentRoom, string[] userArray)
        {
            Console.WriteLine(currentRoom);
            Console.ReadLine();
            if(currentRoom == "Dining")
            {
                if (userArray.Contains("happened"))
                {
                    Console.WriteLine("I don't know what happened.");
                }
                else if ((userArray.Contains("ballroom"))&&(userArray.Contains("key")))
                {
                    Console.WriteLine("Yes I do, please take it.");

                }
                else
                {
                    Console.WriteLine($"{InnocentCharacter[0]} is confused by that sentence and asks you to repeat it.");
                }
            }
        }

        public static void MovingRoom(string[] userArray, MurderItems[] MurderCards, string[] Inventory, string[] InnocentCharacter,string currentRoom)
        {
            //Entering ballroom - Not murder room
            if ((userArray.Contains("Ballroom")) && (Inventory[0] == "Ballroom Key"))
            {
                Console.WriteLine("You have the Ballroom key, collected from one of the suspects.");
                if (MurderCards[0].murderRoom == "Ballroom")
                {
                    Console.WriteLine("You enter the room where the murdered suspect lies");
                }
                else
                {
                    Console.WriteLine("You enter the ballroom, the room echos with the conversations of distraught guests. No body can be seen.");
                }
                Console.ReadLine();
            }

            //Entering Dining Room - Not murder room
            else if (userArray.Contains("Dining"))
            {
                currentRoom = "Dining";
                if (MurderCards[0].murderRoom == "Dining")
                {
                    Console.WriteLine("You enter the room where the murdered suspect lies");
                }
                else
                {
                    Console.WriteLine("You enter the Dining room, You are dissappointed. No corpse can be seen");
                }
                Console.WriteLine($"You enter the dining room, {InnocentCharacter[0]} is standing in the corner, distraught.");
                Inventory[0] = "Ballroom Key";
            }
        }



        //This is for when the player is ready to guess.
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
