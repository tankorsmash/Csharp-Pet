using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetR1
{
    class Inventory
    {

        public List<Item> items;
        User owner;
        public Inventory(User owner)
        {
            Tools.Print("{0}'s Inventory was constructed\n", owner);
            
            //store the owner of the instance
            this.owner = owner;

            //list of all the things in the users inventory
            this.items = new List<Item>();

        }


        public void AddItem(Item item)
        {
            Tools.Print("Adding {0} to {1}'s inventory\n",
                        new object[] { item, this.owner });
            items.Add(item);
            Tools.Print("Total of {0} items in {1}'s inventory\n",
                        items.Count, owner.name);
        }

        //Prints the inventory to screen.
        //todo implement scrolling screen
        public void ShowInventory()
        {
            Tools.Print(newFG: ConsoleColor.DarkGray, text: "{0}'s inventory:",
            vals: owner.name);
            foreach (object item in items)
            {
                string text = String.Format("An item: {0}\n", item);
                Tools.Print(newBG:ConsoleColor.DarkGray, text:text);
            }

        }
    }
}
