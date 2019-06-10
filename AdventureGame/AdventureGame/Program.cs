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
        public struct TrackedItems
        {
            public string murderer;
            public string murderWeapon;
            public string murderRoom;
            public string currentRoom;
        }

        public static void Main()
        {
            //Setting up variables and arrays
            string[] Inventory = { "Empty", "Empty", "Empty", "Empty", "Empty" };
            Random rand = new Random();
            TrackedItems MurderCards = new TrackedItems();
            string[] InnocentCharacter = new string[6];
            string[] suspectArray = { "Peter Plum", "Miss Scarlet", "Miss White", "Mr. Green", "Colonel Mustard", "Anthony Mellon" };
            string[] weaponArray = { "Candlestick", "Dagger", "Lead Pipe", "Revolver", "Spanner", "Poison" };
            string[] roomArray = { "Kitchen", "Ballroom", "Billiard", "Library", "Dining", "Hall", "Study","Lounge" };

            //Randomising the murderer, weapon and room
            MurderCards.murderer = suspectArray[rand.Next(0, 5)];
            MurderCards.murderWeapon = weaponArray[rand.Next(0, 5)];
            MurderCards.murderRoom = roomArray[rand.Next(0, 5)];
            MurderCards.currentRoom = "outside";
            if (MurderCards.murderer == "Anthony Mellon")
            {
                MurderCards.murderWeapon = "2015 Suzuki Swift";
            }


            //error checking for establishing NPCS that are not the murderer
            Console.WriteLine("Murderer: " + MurderCards.murderer);
            for (int i = 0; i <= 5; i++)
            {
                bool loop = true;
                while (loop == true)
                {
                    if (suspectArray[i] != MurderCards.murderer)
                    {
                        InnocentCharacter[i] = suspectArray[i];
                        Console.WriteLine("Innocent: " + InnocentCharacter[i]);
                    }
                    loop = false;
                }
            }
            userInput(ref MurderCards, Inventory, InnocentCharacter);
        }

        //Constantly looping checking for users input
        public static void userInput(ref TrackedItems MurderCards, string[] Inventory, string[] InnocentCharacter)
        {
            Console.WriteLine(MurderCards.murderRoom);
            Console.WriteLine("Murder Room: " + MurderCards.murderRoom);
            Console.WriteLine(MurderCards.currentRoom);
            bool loop = true;
            while (loop == true)
            {
                Console.Write("Please enter what you want to do: ");
                Console.ForegroundColor = ConsoleColor.Green;
                string userInput = Console.ReadLine().ToLower();
                Console.ForegroundColor = ConsoleColor.White;
                string[] userArray = userInput.Split(' ');

                //GoTo Command
                if (userArray[0].ToLower() == "goto")
                {
                    MovingRoom(userArray, ref MurderCards, Inventory, InnocentCharacter);
                }

                //Inventory Command
                else if (userArray[0].ToLower() == "inventory")
                {
                    for (int i = 0; i < Inventory.Length; i++)
                    {
                        Console.WriteLine($"Slot {i + 1} : {Inventory[i]}");
                    }
                }

                //Exit Command
                else if (userArray[0].ToLower() == "exit")
                {
                    Console.WriteLine("Exiting");
                    Thread.Sleep(2000);
                    loop = false;
                }

                //Map command
                else if (userArray[0].ToLower() == "map")
                {
                    Map(ref MurderCards);
                }

                //Ask command
                else if (userArray[0].ToLower() == "ask")
                {
                    Asking(ref MurderCards, Inventory, InnocentCharacter, userArray);
                }

                //Examine command
                else if (userArray[0].ToLower() == "examine")
                {
                    Console.WriteLine(MurderCards.currentRoom);
                    if (MurderCards.currentRoom.ToLower() == "kitchen")
                    {
                        Console.WriteLine("There is a large cupboard on the far side of the kitchen. The bench tops are covered in half cleaned cuttlery and cookware.");
                    }
                }
                //Hint command
                else if (userArray[0].ToLower() == "hint")
                {
                    Hints(ref MurderCards, InnocentCharacter);
                }

                //Error Checking
                else
                {
                    Console.Write("Unknown command, please try again: ");
                }
            }
        }
        public static void Asking(ref TrackedItems MurderCards, string[] Inventory, string[] InnocentCharacter, string[] userArray)
        {
            if (MurderCards.currentRoom == "dining")
            {
                if (userArray.Contains("happened"))
                {
                    Console.WriteLine("I don't know what happened.");
                }
                else if ((userArray.Contains("ballroom")) && (userArray.Contains("key")))
                {
                    Console.WriteLine("I have the key to the ballroom, please take it.");
                }
                else
                {
                    Console.WriteLine($"{InnocentCharacter[0]} is confused by that sentence and asks you to repeat it.");
                }
            }
        }

        public static void Hints(ref TrackedItems MurderCards, string[] InnocentCharacter)
        {
            if(MurderCards.currentRoom.ToLower() == "dining")
            {  
                Console.WriteLine($"Try asking {InnocentCharacter[0]} what happened.");
            }
        }
        
        public static void Map(ref TrackedItems MurderCards)
        {
            StreamReader sr = new StreamReader(@"map.txt");
            while (!sr.EndOfStream)
            {
                Console.WriteLine(sr.ReadLine());
            }
            Console.WriteLine("Finished?");
            Console.ReadLine();
        }

        //Activates when the user is using the moving command.
        public static void MovingRoom(string[] userArray,ref TrackedItems MurderCards, string[] Inventory, string[] InnocentCharacter)
        {

            Console.WriteLine(MurderCards.currentRoom);
            if (userArray.Contains("kitchen"))
            {
                MurderCards.currentRoom = "kitchen";
                Console.WriteLine(MurderCards.currentRoom);
                Console.WriteLine("Inside the marble topped kitchen you find many stainless steel pots and pans. The sink is overflowing with dishes from last nights meal.");
            }
            //Entering ballroom - Not murder room
            else if ((userArray.Contains("ballroom")) && (Inventory[0] == "Ballroom Key"))
            {
                Console.WriteLine("You have the Ballroom key, collected from one of the suspects.");
                if (MurderCards.murderRoom == "Ballroom")
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
            else if (userArray.Contains("dining"))
            {
                MurderCards.currentRoom = "dining";
                if (MurderCards.murderRoom == "dining")
                {


                    Console.WriteLine("You enter the room where the murdered suspect lies");
                }
                else
                {
                    Console.WriteLine(MurderCards.currentRoom);
                    Console.WriteLine("You enter the Dining room, You are disappointed. No corpse can be seen");
                }
                //Console.WriteLine($"You enter the dining room, {InnocentCharacter[0]} is standing in the corner, distraught.");
                Inventory[0] = "Ballroom Key";
            }
        }

        //For when the user enters the cellar and is ready to guess
        public static void Guessing(ref TrackedItems MurderCards)
        {
            int points = 0;
            Console.Write("He asks who you think the murderer was, you reply: ");
            string userGuess = Console.ReadLine();
            if (MurderCards.murderer == userGuess)
            {
                points++;
            }
            Console.Write("He asks what weapon you think you think was used, you reply: ");
            userGuess = Console.ReadLine();
            if (MurderCards.murderWeapon == userGuess)
            {
                points++;
            }
            Console.Write("He asks what room you think you think the murder was commited in, you reply: ");
            userGuess = Console.ReadLine();
            if (MurderCards.murderRoom == userGuess)
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
                Console.ReadLine();
            }
        }
    }
}
