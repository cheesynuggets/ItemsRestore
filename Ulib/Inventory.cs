using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ULib
{
   public class Inventory
    {
        
        
        public Item GetPrimary(UnturnedPlayer unturned) {
            
            return unturned.Inventory.getItem(0, 0).item;
        }
        public Item GetSecondary(UnturnedPlayer unturned)
        {

            return unturned.Inventory.getItem(0, 1).item;
        }
        public ushort GetEquiped(UnturnedPlayer player)
        {
            return player.Player.equipment.itemID;
        }
       
    }
}
