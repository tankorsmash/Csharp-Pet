using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetR1
{
    class Action
        {
            //The user who's action this is
            User master;

            public Action(User master)
            {
                this.master = master;
            }

        }

    class BattleAction : Action
    {
        public BattleAction(User master)
            : base(master)
        {

        }

        public void BattlePet(Pet pet, Creature enemy)
        {
            Tools.Print("RAWR LET'S GO FIGHTING\n");
        }

    }

    class FeedAction : Action
    {
        public FeedAction(User master)
            : base(master)
        {

            //nothing happens
        }

        //Feeds the pet a Foodtype
        public void FeedPet(Pet pet, FoodType foodToEat)
        {
            //feed the pet food
            int gainedNuts = pet.food.Eat(foodToEat);

            string text = "";

            if (gainedNuts > 0)
            {
                text = String.Format("{0} just ate {1}, ",
                                        pet.name,
                                        foodToEat.ToString().ToLower());
            }
            //print the 
            text = text + "{0} satiation level is: {1}\n";
            string formatted = String.Format(text,
                                            pet.pronoun,
                                            pet.food.satiation);
            Tools.Print(newFG: Program.defaultFG,
                        text: formatted,
                        newBG: ConsoleColor.DarkGreen);

            //vals: new object[] {pet.name, foodToEat, pet.food.satiation});
        }
    }

        //Printing out a screen of stats for the current pet
    class StatAction : Action
    {
        public User master;
        public StatAction(User master)
            : base(master)
        {
            this.master = master;
        }


        public void CheckPetStats(Pet pet)
        {
            Tools.Print(newFG: ConsoleColor.Gray,
            text: "Current Pet's name {0}\n" +
                         "Current Satiation {1}\n" +
                         "Current EXP {2}\n"+
                         "HP {3}/{4}\n",
              vals: new object[] {pet.name,
                             pet.food.satiation,
                             pet.exp,
                             pet.battle.currentHP,
                             pet.battle.maxHP});
        }
    }
}
