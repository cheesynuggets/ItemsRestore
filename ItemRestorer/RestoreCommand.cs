using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using ULib;
using UnityEngine;

namespace ItemRestorer
{
    class RestoreCommand : ULib.Command
    {
        Raycast raycast;
        public RestoreCommand() : base(new CommandType { AllowedCaller = AllowedCaller.Both, Aliasses = "restoreinv", Help = "", Name = "RestoreInv", Permissions = "u.restoreinv", Syantax = "" })
        {
           raycast = new Raycast();
        }

        public override void Execute(IRocketPlayer caller, string[] command)
        {
            if(command.Length == 0)
            {
                var target = GetPlayerCsteamidRay(caller);
                if(target == null) { UnturnedChat.Say(caller, "Player Doesn't Exists", Color.red); return; }
                UnturnedChat.Say(caller, "Items Restored", Color.blue);
                ItemRestorer.insta.RestoreItems(target);
            } else if (command.Length == 1)
            {
                var Player = FindPlayer(command[0]);

                if(Player == null) { UnturnedChat.Say(caller, "Player Doesn't Exists", Color.red); ; return; }
                UnturnedChat.Say(caller, "Items Restored", Color.blue);
                ItemRestorer.insta.RestoreItems(Player);
            } else
            {
                UnturnedChat.Say(caller,"Player Doesn't Exists",Color.red);
            }
        }
        UnturnedPlayer FindPlayer(string input)
        {
            foreach (var sp in Provider.clients)
            {
                var Player = UnturnedPlayer.FromSteamPlayer(sp);
                if (Player.CharacterName.Contains(input)) { return Player; }
            }
            return null;
        }
        UnturnedPlayer GetPlayerCsteamidRay(IRocketPlayer caller)
        {
            var ray = raycast.RayForPlayer(caller);
            return ray;
        }
    }
}
