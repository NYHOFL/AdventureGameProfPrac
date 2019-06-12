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
            string[] InnocentCharacter = new string[4];
            string[] suspectArray = { "Peter Plum", "Miss Scarlet", "Miss White", "Mr. Green", "Colonel Mustard"};
            string[] weaponArray = { "Candlestick", "Dagger", "Cue", "Revolver", "Spanner", "Poison" };
            string[] roomArray = { "Kitchen", "Ballroom", "Billiard", "Library", "Dining", "Hall", "Study","Lounge" };

            //Randomising the murderer, weapon and room
            Cards.murderer = suspectArray[rand.Next(0, 5)];
            Cards.murderRoom = roomArray[rand.Next(0, 5)];
            Cards.currentRoom = "Outside";
            Cards.studyLock = "locked";
            Cards.ballroomLock = "locked";

            //error checking for establishing NPCS that are not the murderer
            Console.WriteLine("Murderer: " + Cards.murderer);
            for (int i = 0; i < 4; i++)
            {
                bool loop = true;
                while (loop == true)
                {
                    if (suspectArray[i] != Cards.murderer)
                    {
                        InnocentCharacter[i] = suspectArray[i];
                    }
                    else
                    {
                        InnocentCharacter[i] = "Frank West";
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
                    Examine(ref Cards, Inventory);
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
        //If there are NPCs in the current room the user can ask questions
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
            if (Cards.currentRoom == "lounge")
            {
                if (userArray.Contains("happened"))
                {
                    Console.WriteLine("We don't know, it all happened so fast. We were in the dining room getting food when it happened.");
                }
                else
                {
                    Console.WriteLine($"{InnocentCharacter[1]} and {InnocentCharacter[2]}  are confused by that sentence and ask you to repeat it.");
                }
            }
        }
        //Examing the body
        public static void ExamineBody(ref TrackedItems Cards)
        {
            if ((Cards.murderWeapon.ToLower() == "poison")&& (Cards.currentRoom == "kitchen"))
            {
                Console.WriteLine("The body is lying motionless on the floor, upon further inspection. A glass had been dropped as the victim tried to drink");
                Console.WriteLine("It was very similiar to a recent murder you solved, involving Arsenic. Arsenic is a substance found in rat poison and weed killer. You conclude that the victim must have been poisened.");
            }

        }
        //Examing the rooms method
        public static void Examine(ref TrackedItems Cards, string[] Inventory)
        {
            if(Cards.currentRoom.ToLower() == "kitchen")
            {
                Console.WriteLine("You examine the room, moving tables and searching cupboards. When it seems likely that the room is a dead end, you find a key.");
                Thread.Sleep(200);
                Console.WriteLine("The key is engraved with a giant 'S'. You conclude this can only mean this key is meant for the study room.");
                Inventory[1] = "Study Key";
            }
        }
        //Shows a hint for the current room if there is one
        public static void Hints(ref TrackedItems Cards, string[] InnocentCharacter)
        {
            if (Cards.currentRoom.ToLower() == "dining")
            {
                Console.WriteLine($"Try asking {InnocentCharacter[0]} what happened.");
            }
            if (Cards.currentRoom.ToLower() == "ballroom")
            {
                Console.WriteLine("You did overhear that a gentleman in the lounge has a set of keys for the mansion's many rooms.");
            }
        }
        //Displays the map when the command is triggered
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
                if (Cards.murderRoom.ToLower() == "kitchen")
                {
                    Cards.murderWeapon = "Poison";
                    Console.WriteLine("You enter the kitchen, in the corner is the victim, foam and bile spilling from their mouth, eyes bloodred, skin purple. It must've been a horrible way to die.");
                }
                else
                {
                Console.WriteLine("Inside the marble kitchen you find many stainless steel pots, pans, trays and other equipment. The sink is overflowing with cutlery from last nights meal.");
                }
            }

            //Entering ballroom
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
                            Cards.murderWeapon = "Lead Pipe";
                            Console.WriteLine("You enter the room where the murdered victim lies");
                        }
                        else
                        {
                            Console.WriteLine("You enter the ballroom, the room echos with the conversations of distraught guests. No body can be seen.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("You try to open the ballroom door, but the door is locked.");
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

            //Entering study
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
                            Cards.murderWeapon = "Revolver";
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

            //Entering Lounge
            else if (userArray.Contains("lounge"))
            {
                Cards.currentRoom = "lounge";
                if (Cards.murderRoom == "lounge")
                {
                    Cards.murderWeapon = "Dagger";
                    Console.WriteLine("You enter the room where the murdered victim lies");
                    Console.WriteLine($"You enter the lounge, {InnocentCharacter[2]} and {InnocentCharacter[3]} are sitting on the couch. Comforting each other.");
                }
                else
                {
                    Console.WriteLine($"You enter the lounge, {InnocentCharacter[2]} and {InnocentCharacter[3]} are sitting on the couch. Comforting each other.");
                }
            }

            //Entering Library
            else if (userArray.Contains("library"))
            {
                Cards.currentRoom = "library";
                if (Cards.murderRoom == "library")
                {
                    Cards.murderWeapon = "Candlestick";
                    Console.WriteLine("You enter the library and a body is lying down");
                }
                else
                {
                    Console.WriteLine("You enter the library, bookshelves as tall as the ceiling can be seen.");
                }
            }

            //Entering Dining Room 
            else if (userArray.Contains("dining"))
            {
                Cards.currentRoom = "dining";
                if (Cards.murderRoom == "dining")
                {
                    Cards.murderWeapon = "Fork";
                    Console.WriteLine("You enter the room where the murdered victim lies");
                }
                else
                {
                    Console.WriteLine("You enter the Dining room, You are disappointed. No corpse can be seen");
                    Console.WriteLine($"You enter the dining room, {InnocentCharacter[0]} is standing in the corner.");
                }
                //Console.WriteLine($"You enter the dining room, {InnocentCharacter[0]} is standing in the corner, distraught.");
                Inventory[0] = "Ballroom Key";
            }
            
            //Entering billiard room
            else if(userArray.Contains("billiard"))
            {
                Cards.currentRoom = "billiard";
                if (Cards.murderRoom == "billiard")
                {
                    Cards.murderWeapon = "Pool Cue";
                    Console.WriteLine("Upon entering the Billiard room the smell hits you immediatly, this was a bludgening. The vicim lies on top of the pool table");
                }
                else
                {
                    Console.WriteLine("You enter the billiard room, the room smells of recently lit cigars, and expensive whiskey.");
                }
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
