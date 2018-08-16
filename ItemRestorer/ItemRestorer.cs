using Rocket.Core.Plugins;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItemRestorer
{
    public class ItemRestorer : RocketPlugin<Config>
    {

        public static ItemRestorer insta;
        List<SaveType> Saves = new List<SaveType>();
        protected override void Load()
        {
            insta = this;
            base.Load();
            Rocket.Unturned.Events.UnturnedPlayerEvents.OnPlayerDeath += UnturnedPlayerEvents_OnPlayerDeath;
            Rocket.Unturned.Events.UnturnedPlayerEvents.OnPlayerRevive += UnturnedPlayerEvents_OnPlayerRevive;
        }

        private void UnturnedPlayerEvents_OnPlayerRevive(UnturnedPlayer player, UnityEngine.Vector3 position, byte angle)
        {
            if (insta.Configuration.Instance.DeleteItemsOnDeath)
            {
                ClearAllItems(player);
                ClearClothes(player);
            }
        }

        private void UnturnedPlayerEvents_OnPlayerDeath(UnturnedPlayer player, SDG.Unturned.EDeathCause cause, SDG.Unturned.ELimb limb, Steamworks.CSteamID murderer)
        {
            ClearList(player);
            GetData(player);
            ClearHands(player);
        }
        void GetData(UnturnedPlayer unturnedPlayer)
        {
            List<ushort> itemids = new List<ushort>();
            List<ushort> ClothesIDs = new List<ushort>();
            for (byte page = 0; page < 8; page++)
            {
                byte ItemCount = unturnedPlayer.Player.inventory.getItemCount(page);
                for (byte i = 0; i < ItemCount; i++)
                {

                    if (unturnedPlayer.Player.inventory.getItem(page, i) != null)
                    {
                        itemids.Add(unturnedPlayer.Player.inventory.getItem(page, i).item.id);

                    }
                }
            }
            var Clothes = unturnedPlayer.Player.clothing;
            if (Clothes.backpack != 0) { ClothesIDs.Add(Clothes.backpack); }
            if (Clothes.glasses != 0) { ClothesIDs.Add(Clothes.glasses); }
            if (Clothes.hat != 0) { ClothesIDs.Add(Clothes.hat); }
            if (Clothes.mask != 0) { ClothesIDs.Add(Clothes.mask); }
            if (Clothes.pants != 0) { ClothesIDs.Add(Clothes.pants); }
            if (Clothes.shirt != 0) { ClothesIDs.Add(Clothes.shirt); }
            if (Clothes.vest != 0) { ClothesIDs.Add(Clothes.vest); }

            StoreIntoList(itemids, ClothesIDs, unturnedPlayer);
        }
        void StoreIntoList(List<ushort> items, List<ushort> clothes, UnturnedPlayer player)
        {
            Saves.Add(new SaveType { cSteamID = player.CSteamID, ItemsIDS = items, ClothingIDS = clothes });
        }
        void ClearList(UnturnedPlayer player)
        {
            var Save = Saves.Find(c => c.cSteamID == player.CSteamID);
            if (Save != null)
            {
                Saves.Remove(Save);
            }

        }
        void ClearHands(UnturnedPlayer player)
        {
            player.Player.inventory.removeItem(0, 0);
            player.Player.inventory.removeItem(0, 1);
        }
        public void RestoreItems(UnturnedPlayer player)
        {
            var Save = Saves.Find(c => c.cSteamID == player.CSteamID);
            if (Save != null)
            {
                foreach (var item in Save.ClothingIDS)
                {
                    player.GiveItem(item, 1);
                }
                foreach (var item in Save.ItemsIDS)
                {
                    player.GiveItem(item, 1);
                }
                Saves.Remove(Save);
            }
        }

        void ClearClothes(UnturnedPlayer player)
        {
            player.Player.clothing.askWearBackpack(0, 0, new byte[0], true);
            ClearAllItems(player);

            player.Player.clothing.askWearGlasses(0, 0, new byte[0], true);
            ClearAllItems(player);

            player.Player.clothing.askWearHat(0, 0, new byte[0], true);
            ClearAllItems(player);

            player.Player.clothing.askWearMask(0, 0, new byte[0], true);
            ClearAllItems(player);

            player.Player.clothing.askWearPants(0, 0, new byte[0], true);
            ClearAllItems(player);

            player.Player.clothing.askWearShirt(0, 0, new byte[0], true);
            ClearAllItems(player);

            player.Player.clothing.askWearVest(0, 0, new byte[0], true);
            ClearAllItems(player);
        }

        private void ClearAllItems(UnturnedPlayer player)
        {
            for (byte page = 0; page < 8; page++)
            {
                byte itemCount = player.Player.inventory.getItemCount(page);

                for (byte index = 0; index < itemCount; index++)
                {

                    player.Player.inventory.removeItem(page, index);
                    index--;
                    itemCount--;
                }
            }
        }



        protected override void Unload()
        {
            Rocket.Unturned.Events.UnturnedPlayerEvents.OnPlayerDeath -= UnturnedPlayerEvents_OnPlayerDeath;
            insta = null;
            base.Unload();
        }
    }
}

