using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TammyFranklin
{
    class Meta
    {
        public static string ver = "0.0.1";
    }


    class Program
    {

        //Default consts
        public const ConsoleColor defaultFG = ConsoleColor.Gray;
        public const ConsoleColor defaultBG = ConsoleColor.DarkMagenta;


        static void Main(string[] args)
        {
            //Intro to the app
            Console.Title = "sarah's game";

            
            Console.BackgroundColor = defaultBG;
            Console.ForegroundColor = defaultFG;
            
            //Print intro line
            string welcomeString = "Welcome to the PET GAME version: {0}\n\n";
            string welcomeArg =  Meta.ver;
            Tools.Print(ConsoleColor.Black, ConsoleColor.White, welcomeString, welcomeArg);


            //loops forever afaik
            //while (true)
            //{
                //Create new pet
             
                string name = Tools.Prompt(ConsoleColor.Red, ConsoleColor.Black, "What is your new pets name?");

                Pet userPet = new Pet(name);
                PetMoodComponent mood = new PetMoodComponent(userPet);
                userPet.mood = mood;
                //Tools.Prompt("asd", "a  ");
            //}


            //Wait for Key on exit
            Tools.Print(ConsoleColor.DarkRed, ConsoleColor.DarkYellow, "Press any key to exit");
            Console.ReadKey();
        }
    }

    static class Tools
    {

        /// <summary>
        /// Lazy and wrote a mock python Print function; just found out it's not helpful though.
        /// </summary>
        /// <param name="text"></param>
        /// <returns>the text that was printed to line</returns>
        public static string Print(string text, params Object[] vals)
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
        public static string Print(ConsoleColor newFG = Program.defaultFG , ConsoleColor newBG = Program.defaultBG,  string text = "None set" , params Object[] vals)
        {
            //Changes the Console colors to the new colors, then changes the console colors back.
            ConsoleColor curFG = Console.ForegroundColor;
            ConsoleColor curBG = Console.BackgroundColor;

            Console.ForegroundColor = newFG;
            Console.BackgroundColor = newBG;

            string toPrint = string.Format(text, vals);
            Console.WriteLine(toPrint);

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

    class Pet
    {
        //pet details
        public string name;
        public int level= 0;
        public long exp = 0;
        public char gender = 'm';

        public PetMoodComponent mood;
        
        public Pet(string newName, char newGender = 'm')
        {
            this.name = newName;
            this.gender = newGender;

            Console.WriteLine("A new {1} lion was created, named {0}", this.name, this.getGender());   
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
            else { 
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



        public void changeName(string  newName){
            this.name = newName;
            Console.WriteLine("Pets new name: {0}", name);
        }
    }
}
