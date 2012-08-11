using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetR1
{
    class Inventory
    {

        public Dictionary<char,Item> items;
        User owner;
        public Inventory(User owner)
        {
            //Tools.Print("{0}'s Inventory was constructed\n", owner);
            
            //store the owner of the instance
            this.owner = owner;

            //list of all the things in the users inventory
            this.items = new Dictionary<char, Item>();

        }


        public void AddItem(Item item)
        {
           
            //want to add an item, but need to find a key that hasn't been used yet.
            char newKey = 'a';
            while ( items.ContainsKey(newKey))
            {
                newKey++;
            }

            //add the item to newKey
            items.Add(newKey, item);

            Tools.Print("Adding {0} to {1}'s inventory at '{2}'\n",
                       new object[] { item, this.owner, newKey });

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
