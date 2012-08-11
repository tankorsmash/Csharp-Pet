using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetR1
{
    class Creature
    {

        //pet details
        public string name;
        public int level = 0;
        public long exp = 0;

        public char gender = 'm';
        public string pronoun
        {
            get
            {
                if (gender == 'm')
                {
                    return "his";
                }
                else if (gender == 'f')
                {
                    return "her";
                }
                else
                {
                    return "it's";
                }
            }
        }

        public override string ToString()
        {
            return string.Format("A {0} pet named {1}", this.getGender(), this.name);
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
    }

    class Pet : Creature
    {


        public User master;

        public PetMoodComponent mood;
        public PetFoodComponent food;
        public PetBattleComponent battle;

        public Pet(User newMaster, string newName, char newGender = 'm')
        {
            this.master = newMaster;
            this.name = newName;
            this.gender = newGender;

            //assign components
            this.mood = new PetMoodComponent(this);
            this.food = new PetFoodComponent(this);
            this.battle = new PetBattleComponent(this);

            //Tools.Print("A new {1} lion was created, named {0}, belonging to {2}\n",
              //  this.name, this.getGender(), this.master.name);
            //constructor method
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
