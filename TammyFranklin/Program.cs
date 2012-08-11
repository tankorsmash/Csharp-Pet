using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Lidgren.Network;


namespace PetR1
{
    class Meta
    {
        public static string ver = "0.0.2";
        public static string name_ver = String.Format("PetR1 {0}", ver);

    }


    class Program
    {

        //Default consts
        public const ConsoleColor defaultFG = ConsoleColor.White;
        public const ConsoleColor defaultBG = ConsoleColor.Black;


        static void Main(string[] args)
        {
            //debugging stuff


            ////checking how c# passes vars
            //int orig = 100;
            //int first = orig;
            //orig++;
            //Tools.Print("Orig {0} and first {1}\n", new object[]{orig, first});

            //seeing if you can convert chars to ints and back
            //char val = 'A'; // c=99 so a=97? B=66 so A=65?
            //val++;
            //int newVal = (int)val;
            //char newest = (char)newVal;
            //Tools.Print(newVal.ToString()) ;

            //testing if `c` and `C` are the same, they aren't
            //string val;
            //if ('c' == 'c') {val = "t"; }
            //else {  val = "f"; }
            //Tools.Print(val);

            /*char qwe = 'a';
            qwe++;
            Console.WriteLine(qwe);*/


            //end debugging


            //Intro to the app
            Console.Title = Meta.name_ver;


            Console.BackgroundColor = defaultBG;
            Console.ForegroundColor = defaultFG;


            //Print intro line
            string welcomeString = "Welcome to the PET GAME version: {0}\n\n";
            string welcomeArg = Meta.ver;
            Tools.Print(ConsoleColor.Black, ConsoleColor.White, welcomeString, welcomeArg);


            //Creates User with a default name
            User user = new User();
            user.inventory.AddItem(new FoodFruit());
            user.inventory.AddItem(new FoodApple());
            //user.inventory.AddItem(new FoodApple());
            //user.inventory.AddItem(new FoodMeat());

            //Gets the name of the pet.
            //string name = Tools.Prompt("What is the name of your new male Pet?");
            string name = "YourFirstPet";
            Pet yourPet = new Pet(user, name);

            //Intro message
            Tools.Print(newFG: ConsoleColor.DarkBlue,
                        newBG: ConsoleColor.Cyan,
                        text: "\nThis is the main loop for the game. \n" +
                            "It is here that you'll feed or fight your" +
                            "\npet in order to train the shit out of it" +
                            "\n\n\n\t\tBEGIN!\n");

            //main loop
            while (true)
            {
                //give the user a choice of actions.
                string choices = "\t[f]eed, [b]attle, [s]tats or [q]uit.";
                KeyValuePair<string, string> action = getUserAction(choices);

                //feed the pet
                if (action.Value == "feed")
                {
                    //this is  a real shitty way of doing this. There 
                    // shouldn't be a class all on its own... :(
                    FeedAction feeding = new FeedAction(user);

                    user.inventory.ShowInventory();

                    string choice = Tools.Prompt("Choose a key from your items to feed");
                    char charChoice = (char)choice[0];

                    feeding.FeedPet(yourPet, user.inventory.items[charChoice] as FoodType);
                    //feeding.FeedPet(yourPet, new FoodMeat());
                }
                //check stats of the current pet
                else if (action.Value == "stats")
                {
                    StatAction stats = new StatAction(user);
                    stats.CheckPetStats(yourPet);
                }

                else if (action.Value == "battle")
                {
                    BattleAction battle = new BattleAction(user);
                    yourPet.battle.TakeDamage(10);
                }

                //quit the main loop
                else if (action.Value == "quit")
                {
                    break;
                }

                //Tools.Print(newFG: ConsoleColor.Yellow, text:"This was the action chosen: {0}\n", vals:action);

            }

            //Wait for Key on exit
            Tools.Print(ConsoleColor.DarkRed,
                    ConsoleColor.DarkYellow,
                    "Press any key to exit {0}",
                    Meta.name_ver);
            Console.ReadKey();
        }


        private static KeyValuePair<string, string> getUserAction(string choices)
        {
            //whether or not the userAction is valid
            bool actionIsValid = false;

            //From the choices string, parse out the optional actions, returned as a key:action
            Dictionary<string, string> validAnwers = FindActionKeys(choices);

            do
            {
                //Get the user's choice of action
                string action = Tools.Prompt("What do you want to do?\n{0}", choices);

                //Verify that it's in the keys of the validAnswers
                if (validAnwers.ContainsKey(action))
                {
                    Tools.Print("you have chosen wisely and a valid key.\n");
                    //Tools.Print(validAnwers[action]);
                    //return action;
                    return new KeyValuePair<string, string>(action, validAnwers[action]);
                }
                else
                {
                    Tools.Print(newFG: ConsoleColor.Red,
                                text: "Incorrect key, please type exactly the letter you're looking for.\n");
                    //return action;
                }
            } while (!actionIsValid);

            //since the loop is done, the action is considered valid, and returned
            //return action;
            return new KeyValuePair<string, string>("invalid", "invalid");
        }

        /// <summary>
        /// Builds a dictionary with letter:word pairs of the keys you want the user
        /// to be able to press in order to advance.
        /// </summary>
        /// <param name="toTest">The string you want to make get the letter:word legend from</param>
        /// <returns>A dict with letter:word pairs, both strings</returns>
        private static Dictionary<string, string> FindActionKeys(string toTest)
        {

            //Finds the bracketed letter along with the whole word it belongs to
            string toFind = @"(?<=\s|^)\[(.)\]\w+";
            //do the search
            MatchCollection matches = Regex.Matches(toTest, toFind);

            //create a dict with strings as key and values
            Dictionary<String, String> keyWordPairs = new Dictionary<string, string>();

            //loop over the matches
            foreach (Match result in matches)
            {
                //Console.WriteLine("next match");
                //add the key and words to the dict, uglily replace the brackets.
                keyWordPairs.Add(result.Groups[1].Value,  //key set to the letter
                    result.Groups[0].Value.Replace("[", "").Replace("]", "")); //value set to word without [ or ]
            }

            //loop over the dict and print em out
            foreach (KeyValuePair<string, string> pair in keyWordPairs)
            {

                //Console.WriteLine("Key: {0} to {1}", pair.Key, pair.Value);

            }

            return keyWordPairs;
        }


        
    }   


    class User
    {

        public Inventory inventory;
        private string _name;

        public string name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }

        public override string ToString()
        {
            return String.Format("User:{0}", this.name);
        }

        public User(string newName = "DefaultName")
        {

            this.name = newName;
            this.inventory = new Inventory(this);
            
            string text = String.Format("{0} was just created as an instance of {1}\n", this.name, this);
            //Tools.Print(newFG: ConsoleColor.Green, text: text);


        }
    }


    static class Tools
    {

        /// <summary>
        /// Lazy and wrote a mock python Print function; just found out it's not helpful though.
        /// </summary>
        /// <param name="text"></param>
        /// <returns>the text that was printed to line</returns>
        public static string Print(string text, params object[] vals)
        {
            string toPrint = string.Format(text, vals);
            Console.Write(toPrint);
            //if (! toPrint.EndsWith("\n"))
            //{
            //    Console.Write(toPrint);
            //}
            //else if (toPrint.EndsWith("\n"))
            //{
            //    Console.WriteLine(toPrint);
            //}
            return text;
        }

        /// <summary>
        /// Print overload that allows to change the console color along with printint text
        /// </summary>
        /// <param name="newFG">The new foreground color </param>
        /// <param name="newBG">The new background color</param>
        /// <param name="text">The text to print</param>
        /// <param name="vals">The strings to format into the text</param>
        /// <returns></returns>
        public static string Print(ConsoleColor newFG = Program.defaultFG,
                                    ConsoleColor newBG = Program.defaultBG,
                                    string text = "None set",
                                    params object[] vals)
        {
            //Changes the Console colors to the new colors,  then changes the console colors back.
            ConsoleColor curFG = Console.ForegroundColor;
            ConsoleColor curBG = Console.BackgroundColor;

            Console.ForegroundColor = newFG;
            Console.BackgroundColor = newBG;

            string toPrint = string.Format(text, vals);
            Console.Write(toPrint);
            //Print(toPrint);

            //Change the Console colors back to what they were
            Console.ForegroundColor = curFG;
            Console.BackgroundColor = curBG;

            return text;
        }

        public static string Prompt(string promptText, params Object[] formatting)
        {
            //writes a string to buffer, then returns the string of the response
            promptText = promptText + "\n>>> ";
            Print(promptText, formatting);

            string response = Console.ReadLine();

            //add a newline
            Console.WriteLine();

            return response;
        }

        public static string Prompt(ConsoleColor fg, ConsoleColor bg, string promptText, params Object[] formatting)
        {
            //writes a string to buffer, then returns the string of the response
            Print(fg, bg, promptText, formatting);

            string response = Console.ReadLine();

            return response;
        }
    }


    /// <summary>
    /// Handles all the pets moods. Starting with Happy, Sad, Tired, Sleeping, Hungry
    /// </summary>
    class PetMoodComponent
    {
        private Pet master;

        public PetMoodComponent(Pet master)
        {
            //define the pet which this mood belongs too
            this.master = master;

            //print confirmation.
            //Tools.Print(ConsoleColor.Green, ConsoleColor.DarkYellow, "This general mood belongs to: {0}\n", this.getMaster().ToString());
        }

        public Pet getMaster()
        {
            return this.master;
        }
    }

    /// <summary>
    /// Any pet related status, such as being full or sick or anything
    /// will be passed to this component to be shown to the user
    /// </summary>
    class PetStatusComponent
    {

        public List<Debuff> debuffs = new List<Debuff>();
        public List<Buff> buffs = new List<Buff>();

        public User owner;
        /// <summary>
        /// Constructor, assigns a User to owner
        /// </summary>
        /// <param name="owner">the user who owns this pet</param>
        public  PetStatusComponent(User owner)
        {
            this.owner = owner;
        }

        /// <summary>
        /// Used every loop to check whether the pet is sick, hungry, full, tired etc.
        /// If status > 1, then a string will be shown before you prompt
        /// </summary>
        public void CheckPetStatus()
        {
            
        }


    }

    class PetBattleComponent
    {
        public Pet master;

        public int maxHP = 100;
        public int currentHP;
        

        public PetBattleComponent(Pet master) 
        {
            this.master = master;
            this.currentHP = this.maxHP;
        }

        public int TakeDamage(int damage)
        {
            this.currentHP -= damage;

            Tools.Print(text:"{0} took {1} with {2} remaining HP\n",
                        vals:new object[] { this.master, damage, this.currentHP },
                        newBG: ConsoleColor.Magenta);

            if (currentHP < 0)
            {
                Tools.Print("{0} is on {1} deathbed\n", new object[]{this.master,
                                                    this.master.pronoun});
            }

            return this.currentHP;
        }
    }

    /// <summary>
    /// Handles all the eating related stuff for the pet
    /// </summary>
    class PetFoodComponent
    {
        //who owns the instance of PFC
        Pet master;
        //closer the satiation is to zero, the hungrier 
        //the pet is. At less than zero, it starts losing
        //health.
        public int satiation;

        public PetFoodComponent(Pet master)
        {
            this.master = master;
        }

        public int Eat(FoodType toBeEaten)
        {
            int gainedNutrients= toBeEaten.EatThis();
            this.satiation += gainedNutrients;

            if (satiation >= 150)
            {

                Tools.Print(newBG: ConsoleColor.Red, text: "Mother fucker is full yo, stop feeding it.\n");

            }

            return gainedNutrients;
        }

    }

}

