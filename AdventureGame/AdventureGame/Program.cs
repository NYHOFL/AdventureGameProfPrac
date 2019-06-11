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
            public string ballroomLock;
            public string studyLock;
        }

        public static void Main()
        {
            //Setting up variables and arrays
            string[] Inventory = { "Empty", "Empty", "Empty", "Empty", "Empty" };
            Random rand = new Random();
            TrackedItems Cards = new TrackedItems();
            string[] InnocentCharacter = new string[6];
            string[] suspectArray = { "Peter Plum", "Miss Scarlet", "Miss White", "Mr. Green", "Colonel Mustard", "Anthony Mellon" };
            string[] weaponArray = { "Candlestick", "Dagger", "Lead Pipe", "Revolver", "Spanner", "Poison" };
            string[] roomArray = { "Kitchen", "Ballroom", "Billiard", "Library", "Dining", "Hall", "Study","Lounge" };

            //Randomising the murderer, weapon and room
            Cards.murderer = suspectArray[rand.Next(0, 5)];
            Cards.murderWeapon = weaponArray[rand.Next(0, 5)];
            Cards.murderRoom = roomArray[rand.Next(0, 5)];
            Cards.currentRoom = "Outside";
            Cards.studyLock = "locked";
            Cards.ballroomLock = "locked";
            if (Cards.murderer == "Anthony Mellon")
            {
                Cards.murderWeapon = "2015 Suzuki Swift";
            }


            //error checking for establishing NPCS that are not the murderer
            Console.WriteLine("Murderer: " + Cards.murderer);
            for (int i = 0; i <= 5; i++)
            {
                bool loop = true;
                while (loop == true)
                {
                    if (suspectArray[i] != Cards.murderer)
                    {
                        if (suspectArray[i] != Cards.murderer) { 
                        InnocentCharacter[i] = suspectArray[i];
                        //Console.WriteLine("Innocent: " + InnocentCharacter[i]);
                        }
                    }
                    loop = false;
                }
            }
            userInput(ref Cards, Inventory, InnocentCharacter);
        }

        //Constantly looping checking for users input
        public static void userInput(ref TrackedItems Cards, string[] Inventory, string[] InnocentCharacter)
        {
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
                    MovingRoom(userArray, ref Cards, Inventory, InnocentCharacter);
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
                    Map(ref Cards);
                }

                //Ask command
                else if (userArray[0].ToLower() == "ask")
                {
                    Asking(ref Cards, Inventory, InnocentCharacter, userArray);
                }

                //Examine command
                else if (userArray[0].ToLower() == "examine")
                {
                    Console.WriteLine(Cards.currentRoom);
                    if (Cards.currentRoom.ToLower() == "kitchen")
                    {
                        Console.WriteLine("There is a large cupboard on the far side of the kitchen. The bench tops are covered in half cleaned cuttlery and cookware.");
                    }
                }
                //Hint command
                else if (userArray[0].ToLower() == "hint")
                {
                    Hints(ref Cards, InnocentCharacter);
                }

                //Error Checking
                else
                {
                    Console.Write("Unknown command, please try again: ");
                }
            }
        }
        public static void Asking(ref TrackedItems Cards, string[] Inventory, string[] InnocentCharacter, string[] userArray)
        {
            if (Cards.currentRoom == "dining")
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

        public static void Hints(ref TrackedItems Cards, string[] InnocentCharacter)
        {
            if(Cards.currentRoom.ToLower() == "dining")
            {  
                Console.WriteLine($"Try asking {InnocentCharacter[0]} what happened.");
            }
        }
        
        public static void Map(ref TrackedItems Cards)
        {
            StreamReader sr = new StreamReader(@"map.txt");
            while (!sr.EndOfStream)
            {
                Console.WriteLine(sr.ReadLine());
            }
            Console.WriteLine($"=       N: NPC             Current Room: {Cards.currentRoom.PadRight(7)}          =");
            Console.WriteLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = = = =");
            Console.WriteLine("Finished?");
            Console.ReadLine();
        }

        //Activates when the user is using the moving command.
        public static void MovingRoom(string[] userArray, ref TrackedItems Cards, string[] Inventory, string[] InnocentCharacter)
        {
            if (userArray.Contains("kitchen"))
            {
                Cards.currentRoom = "kitchen";
                Console.WriteLine(Cards.currentRoom);
                Console.WriteLine("Inside the marble topped kitchen you find many stainless steel pots and pans. The sink is overflowing with dishes from last nights meal.");
            }

            //Entering ballroom - Not murder room
            else if (userArray.Contains("ballroom"))
            {
                if (Cards.ballroomLock.ToLower() == "locked")
                { 
                    if (Inventory[0] == "Ballroom Key")
                    {
                        Cards.ballroomLock =  "unlocked";
                        Console.WriteLine("You have the Ballroom key, collected from one of the suspects.");
                        if (Cards.murderRoom.ToLower() == "ballroom")
                        {
                            Console.WriteLine("You enter the room where the murdered victim lies");
                        }
                        else
                        {
                            Console.WriteLine("You enter the ballroom, the room echos with the conversations of distraught guests. No body can be seen.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("You try to open the ballroom door, but the door is locked");
                    }
                }
                else
                {
                    Console.WriteLine("The door is still open from last time you entered.");
                    if (Cards.murderRoom.ToLower() == "ballroom")
                    {
                        Console.WriteLine("The victim's body still lies on the floor");
                    }
                    else
                    {
                        Console.WriteLine("The room continues to echo with the conversations of distraught guests.");
                    }
                }
            }

            else if (userArray.Contains("study"))
            {
                if (Cards.studyLock.ToLower() == "locked")
                {
                    if (Inventory[1] == "Study Key")
                    {
                        Cards.studyLock = "unlocked";
                        Console.WriteLine("You have the Study key");
                        if (Cards.murderRoom.ToLower() == "study")
                        {
                            Console.WriteLine("You enter the room where the murdered victim lies");
                        }
                        else
                        {
                            Console.WriteLine("You enter the study");
                        }
                    }
                    else
                    {
                        Console.WriteLine("You try to open the study door, but the door is locked");
                    }
                }
                else
                {
                    Console.WriteLine("The door is still open from last time you entered.");
                    if (Cards.murderRoom.ToLower() == "study")
                    {
                        Console.WriteLine("The victim's body still lies on the floor");
                    }
                    else
                    {
                        Console.WriteLine("The room continues to echo with the conversations of distraught guests.");
                    }
                }
            }


            //Entering Dining Room - Not murder room
            else if (userArray.Contains("dining"))
            {
                Cards.currentRoom = "dining";
                if (Cards.murderRoom == "dining")
                {
                    Console.WriteLine("You enter the room where the murdered victim lies");
                }
                else
                {
                    Console.WriteLine("You enter the Dining room, You are disappointed. No corpse can be seen");
                }
                //Console.WriteLine($"You enter the dining room, {InnocentCharacter[0]} is standing in the corner, distraught.");
                Inventory[0] = "Ballroom Key";
            }
        }

        //For when the user enters the cellar and is ready to guess
        public static void Guessing(ref TrackedItems Cards)
        {
            int points = 0;
            Console.Write("He asks who you think the murderer was, you reply: ");
            string userGuess = Console.ReadLine();
            if (Cards.murderer == userGuess)
            {
                points++;
            }
            Console.Write("He asks what weapon you think you think was used, you reply: ");
            userGuess = Console.ReadLine();
            if (Cards.murderWeapon == userGuess)
            {
                points++;
            }
            Console.Write("He asks what room you think you think the murder was commited in, you reply: ");
            userGuess = Console.ReadLine();
            if (Cards.murderRoom == userGuess)
            {
                points++;
            }
            End(points);
            
        }
        public static void End(int points)
        {
            if (points == 3)
            {
                Console.WriteLine("You win, you figured out who the murderer was!");

            }
            else
            {
                Console.WriteLine("You lost");
            }
        }
    }
}
