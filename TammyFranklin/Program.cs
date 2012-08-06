using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Text.RegularExpressions;



namespace PetR1
{
    class Meta
    {
        public static string ver = "0.0.1";
    }


    class Program
    {

        //Default consts
        public const ConsoleColor defaultFG = ConsoleColor.White;
        public const ConsoleColor defaultBG = ConsoleColor.Black;


        static void Main(string[] args)                                        
        {
            //Intro to the app
            Console.Title = "sarah's game";


            Console.BackgroundColor = defaultBG;
            Console.ForegroundColor = defaultFG;

            //Print intro line
            string welcomeString = "Welcome to the PET GAME version: {0}\n\n";
            string welcomeArg = Meta.ver;
            Tools.Print(ConsoleColor.Black, ConsoleColor.White, welcomeString, welcomeArg);


            //Creates User with a default name
            User user = new User();

            //Gets the name of the pet.
            string name = Tools.Prompt("What is the name of your new male Pet?");
            Pet yourPet = new Pet(user, name);

            //Intro message
            Tools.Print(newFG: ConsoleColor.DarkBlue,
                        newBG: ConsoleColor.Cyan,
                        text: "\t\nThis is the main loop for the game. \n" +
                            "It is here that you'll feed or fight your" +
                            "\npet in order to train the shit out of it" +
                            "\n\n\n\t\tBEGIN!");

            //main loop
            while (true)
            {
                //give the user a choice of actions.
                string choices = "\t[f]eed, [b]attle or [q]uit.";
                KeyValuePair<string,string> action = getUserAction(choices);

                if (action.Value == "feed")
                {
                    FeedAction feeding = new FeedAction(user);
                    feeding.FeedPet(yourPet, new FoodMeat());


                    //yourPet.food.Eat(new FoodMeat());
                }

                Tools.Print(newFG: ConsoleColor.Yellow, text:"This was the action chosen: {0}", vals:action);

                //Tools.Prompt("asd", "a  ");
            }


            //Wait for Key on exit
            Tools.Print(ConsoleColor.DarkRed, ConsoleColor.DarkYellow, "Press any key to exit");
            Console.ReadKey();
        }


        private static KeyValuePair<string,string> getUserAction(string choices)
        {
            //whether or not the userAction is valid
            bool actionIsValid = false;

            //From the choices string, parse out the optional actions, returned as a key:action
            Dictionary<string,string> validAnwers = FindActionKeys(choices);

            do
            {
                //Get the user's choice of action
                string action = Tools.Prompt("What do you want to do?\n{0}", choices);

                //Verify that it's in the keys of the validAnswers
                if (validAnwers.ContainsKey(action))
                {
                    Tools.Print("you have chosen wisely");
                    //Tools.Print(validAnwers[action]);
                    //return action;
                    return new KeyValuePair<string,string>(action, validAnwers[action]);
                }
                else
                {
                    Tools.Print(newFG:ConsoleColor.Red, 
                                text:"Incorrect key, please type exactly the letter you're looking for.");
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
        private static Dictionary<string,string> FindActionKeys(string toTest)
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


        class Action
        {
            //The user who's action this is
            User master;

            public  Action(User master)
            {
                this.master = master;
            }
           
        }

        class FeedAction : Action
        {
            public  FeedAction(User master) : base(master)
            {
                
                //nothing happens
            }

            //Feeds the pet a Foodtype
            public void FeedPet(Pet pet, FoodType foodToEat)
            {
                //feed the pet food
                pet.food.Eat(foodToEat);
                
                //print the 
                string text = "{0} just ate {1}, sat. lvl is: {2}";
                string formatted = String.Format(text, pet.name, foodToEat, pet.food.satiation);
                Tools.Print( defaultFG,
                            ConsoleColor.Green,
                            text,
                            pet.name, foodToEat, pet.food.satiation);
            }
        }



        class User
        {
            


            private string _name;

            public string name
            {
                get {
                    return this._name;
                }
                set { 
                    this._name = value; 
                }
            }

            public override string ToString()
            {
                return String.Format("A User named {0}", this.name);
            }

            public  User(string newName = "USERjosh")
            {

                this.name = newName;

                string text = String.Format("{0} was just created as an instance of {1}", this.name, this);
                Tools.Print(newFG:ConsoleColor.Green, text:text);
                
                
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
                Console.WriteLine(toPrint);
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

                //try
                //{
                    string toPrint = string.Format(text, vals);
                    Console.WriteLine(toPrint);

                //}
                //catch (FormatException ex)
                //{
                  //Console.WriteLine(vals);
                  //Console.WriteLine(ex);
                //}
                

                //Change the colors back
                Console.ForegroundColor = curFG;
                Console.BackgroundColor = curBG;

                return text;
            }

            public static string Prompt(string promptText, params Object[] formatting)
            {
                //writes a string to buffer, then returns the string of the response
                Print(promptText, formatting);

                string response = Console.ReadLine();

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
                Tools.Print(ConsoleColor.Green, ConsoleColor.DarkYellow, "This general mood belongs to: {0}", this.getMaster().ToString());
            }

            public Pet getMaster()
            {
                return this.master;
            }
        }

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

            public void Eat(FoodType toBeEaten)
            {

            }

        }

        class Pet
        {
            //pet details
            public string name;
            public int level = 0;
            public long exp = 0;
            public char gender = 'm';
            public User master;

            public PetMoodComponent mood;
            public PetFoodComponent food;

            public Pet(User newMaster, string newName, char newGender = 'm')
            {
                this.master = newMaster;
                this.name = newName;
                this.gender = newGender;

                //assign components
                this.mood = new PetMoodComponent(this);
                this.food = new PetFoodComponent(this);

                Console.WriteLine("A new {1} lion was created, named {0}, belonging to {2}",
                    this.name, this.getGender(), this.master.name);
                //constructor method
            }


            public override string ToString()
            {
                return string.Format("A {0} pet name {1}", this.getGender(), this.name);
            }


            /// <summary>
            /// Returns the gender of the Pet, either full word "male" or a single letter "m"
            /// </summary>
            /// <param name="fullWord"></param>
            /// <returns>"male" or "m"</returns>
            public string getGender(bool fullWord = true)
            {
                if (fullWord)
                {
                    if (this.gender == 'm')
                    {
                        return "male";
                    }
                    else
                    {
                        return "female";
                    }
                }
                else
                {
                    return this.gender.ToString();
                }
            }

            /// <summary>
            /// Adds exp to the pet's total, and changes the level if needed
            /// </summary>
            /// <param name="exp"></param>
            /// <returns>a long with the total exp</returns>
            public long raiseExp(long newExp)
            {
                this.exp += newExp;
                long printedExp;
                printedExp = printExp();
                return printedExp;
            }

            //Writes the current exp to the console and returns a long of the current exp
            public long printExp()
            {
                Console.WriteLine("This is {0}'s total exp : {1}", this.name, this.exp);
                return this.exp;
            }



            public void changeName(string newName)
            {
                this.name = newName;
                Console.WriteLine("Pets new name: {0}", name);
            }
        }
    }
}