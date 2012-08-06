using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetR1
{
    
    class  FoodType
    {
        public int nutrientsGiven = 15;
        public  FoodType()
        {
            
        }

    }

    class FoodMeat : FoodType
    {
        //access the nutrientsGiven here
        public FoodMeat()
        {
            this.nutrientsGiven = 30;
       }
    }
}
