using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetR1
{
    
    class  FoodType : Item
    {
        //It's the amount of satiation that will be added to the pet that eats it.
        public int nutrientsGiven = 15;
        //servings is the amount of times a normal sized pet can eat it without destroying
        //it entirely
        public int servings = 1;

        //public string type_of_food;

        public  FoodType()
        {
            //this.type_of_food = "General Food stuff";
            //this.type_of_food = this.ToString().Remove(0, 10);
        }

        public override string ToString()
        {
            return base.ToString().Remove(0, 10);
        }


        public bool isEdible
        {
            get
            {
                return this.servings > 0;
            }
        }
        public int EatThis()
        {
            //If there's servings left return nutrients, otherwise it's 0
            if (this.isEdible)
            {
                this.servings -= 1;
                Tools.Print("{0} was eaten.\n", this);
                return nutrientsGiven;
            }
            else 
            {
                Tools.Print("{0} is already eaten.\n", this);
                return 0;
            }
            
        }
    }

    
    class FoodMeat : FoodType
    {
        //access the nutrientsGiven here
        public FoodMeat()

        {
            this.nutrientsGiven = 30;
            //this.type_of_food = "Meat";
       }
    }

    class FoodFruit : FoodType
    {
        public FoodFruit()
        {
            this.nutrientsGiven = 15;
            //this.type_of_food = "Fruit";
        }
    }

    class FoodApple : FoodFruit
    {
        public FoodApple()
        {
            this.nutrientsGiven = base.nutrientsGiven - 5;
        }
    }
    
}
