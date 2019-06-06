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
            string[] suspectArray = { "Peter Plum", "Miss Scarlet", "Miss White", "Mr. Green", "Colonel Mustard", "Alfred Gray", "Anthony Mellon" };
            string[] weaponArray = { "Candlestick", "Dagger", "Lead Pipe", "Revolver", "Spanner", "Poison" };
            string[] roomArray = { "Kitchen", "Ballroom", "Billiard", "Library", "Dining", "Hall", "Study" };

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
            userInput(MurderCards, Inventory, InnocentCharacter);
        }

        public static void userInput(TrackedItems MurderCards, string[] Inventory, string[] InnocentCharacter)
        {
            int temp;
            Console.WriteLine(MurderCards.murderRoom);
            bool loop = true;
            while (loop == true)
            {
                Console.Write("Please enter what you want to do: ");
                Console.ForegroundColor = ConsoleColor.Green;
                string userInput = Console.ReadLine().ToLower();
                Console.ForegroundColor = ConsoleColor.White;
                string[] userArray = userInput.Split(' ');

                //GoTo Command
                if (userArray[0] == "goto")
                {
                    MovingRoom(userArray, MurderCards, Inventory, InnocentCharacter);
                }

                //Inventory Command
                else if (userArray[0] == "inventory")
                {
                    for (int i = 0; i < Inventory.Length; i++)
                    {
                        Console.WriteLine($"Slot {i + 1} : {Inventory[i]}");
                    }
                    Console.WriteLine("What slot would you like to look at?");
                    temp = Convert.ToInt32(Console.ReadLine());

                    switch (temp)
                    {
                        case 1:
                            Console.WriteLine("Opening slot 1");
                            break;

                        case 2:
                            Console.WriteLine("Opening slot 2");
                            break;

                        case 3:
                            Console.WriteLine("Opening Slot 3");
                            break;

                        case 4:
                            Console.WriteLine("Opening Slot 4");
                            break;

                        case 5:
                            Console.WriteLine("Opening Slot 5");
                            break;
                    }

                    Console.ReadLine();

                }

                //Exit Command
                else if (userArray[0] == "exit")
                {
                    Console.WriteLine("You failed to solve the case. You disgrace your academy and are sent back in shame.");
                    Thread.Sleep(2000);
                    loop = false;
                }

                //Ask command
                else if (userArray[0] == "ask")
                {
                    Console.WriteLine(MurderCards.currentRoom);
                    Console.ReadLine();
                    Asking(MurderCards, Inventory, InnocentCharacter, userArray);
                }

                //Error Checking
                else
                {
                    Console.Write("Unknown command, please try again: ");
                }


            }
        }
        public static void Asking(TrackedItems MurderCards, string[] Inventory, string[] InnocentCharacter, string[] userArray)
        {
            Console.WriteLine(MurderCards.currentRoom);
            Console.ReadLine();
            if (MurderCards.currentRoom == "dining")
            {
                if (userArray.Contains("happened"))
                {
                    Console.WriteLine("I don't know what happened.");
                }
                else if ((userArray.Contains("ballroom")) && (userArray.Contains("key")))
                {
                    Console.WriteLine("Yes I do, please take it.");

                }
                else
                {    
                    Console.WriteLine($"{InnocentCharacter[0]} is confused by that sentence and asks you to repeat it.");
                }
            }
        }

        public static void MovingRoom(string[] userArray, TrackedItems MurderCards, string[] Inventory, string[] InnocentCharacter)
        {

            if (userArray.Contains("kitchen"))
            {
                Console.Clear();
                Console.WriteLine("Kitchen");
                Console.WriteLine("Inside the marble topped kitchen you find many stainless steel pots and pans. The sink is overflowing with dishes from last nights meal.");
                Console.WriteLine("There is a large cupboard on the far side of the kitchen. The bench tops are covered in half cleaned cuttlery and cookware.");
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("");
                //if examine, mention the cupboard again, cuttlery, sinks, bench tops. 
                //if goto, 
                    
            }
            //Entering ballroom - Not murder room
            if ((userArray.Contains("ballroom")) && (Inventory[0] == "Ballroom Key"))
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
                    Console.WriteLine("You enter the Dining room, You are dissappointed. No corpse can be seen");
                }
                Console.WriteLine($"You enter the dining room, {InnocentCharacter[0]} is standing in the corner, distraught.");
                Inventory[0] = "Ballroom Key";
            }
        }
    

        //This is for when the player is ready to guess.
        public static void Guessing(TrackedItems MurderCards)
        {
            int points = 0;
            Console.Write("He asks who you think the murderer was, you reply: ");
            string userGuess = Console.ReadLine();
            if (MurderCards.murderer == userGuess)
            {
                points++;
            }
            Console.Write("He asks what weapon you think you think was used: ");
            userGuess = Console.ReadLine();
            if (MurderCards.murderWeapon == userGuess)
            {
                points++;
            }
            Console.Write("He asks what room you think you think the murder was commited in: ");
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
