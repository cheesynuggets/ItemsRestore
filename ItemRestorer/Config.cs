using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItemRestorer
{
    public class Config : IRocketPluginConfiguration
    {
        public bool DeleteItemsOnDeath;
        public void LoadDefaults()
        {
            DeleteItemsOnDeath = true;
        }
    }
}
