using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItemRestorer
{

   public class SaveType
   {
        public CSteamID cSteamID;
        public List<ushort> ItemsIDS { get; set; }
        public List<ushort> ClothingIDS { get; set; }
    }

}
