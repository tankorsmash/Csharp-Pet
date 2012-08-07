using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetR1
{
    
    class  FoodType
    {
        public int nutrientsGiven = 15;

        public string type_of_food;

        public  FoodType()
        {
            this.type_of_food = "General Food stuff";
            this.type_of_food = this.ToString().Remove(0, 10);
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
