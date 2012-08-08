using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetR1
{
    

    //Blank status to be inherited
    class Status
    {
        public Pet owner;

        public void Status(Pet owner)
        {
            this.owner = owner;

        }
    }

    /// <summary>
    /// Class for negative status ailments. Sad, Hungry, Starvin, Dying, Sick, Depressed, Sleepy
    /// </summary>
    class Debuff : Status
    {

    }
    

    /// <summary>
    /// Class for good status ailments. Happy, Satiated, Extra Healthy, Giddy, Well Rested
    /// </summary>
    class Buff : Status
    {

    }
}
