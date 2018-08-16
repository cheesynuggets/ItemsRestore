using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ULib
{
    public class Raycast
    {
        public Transform Ray(IRocketPlayer caller)
        {
            RaycastHit hit;
            UnturnedPlayer player = (UnturnedPlayer)caller;
            if (Physics.Raycast(player.Player.look.aim.position, player.Player.look.aim.forward, out hit, 10, RayMasks.PLAYER_INTERACT))
            {


                Transform transform = hit.transform;
                InteractableVehicle vehicle = transform.gameObject.GetComponent<InteractableVehicle>();

                if (transform.GetComponent<InteractableDoorHinge>() != null)
                {
                    transform = transform.parent.parent;
                }
                return transform;
            }
            return null;
        }
        public UnturnedPlayer RayForPlayer(IRocketPlayer caller)
        {
            RaycastHit hit;
            UnturnedPlayer player = (UnturnedPlayer)caller;
            if (Physics.Raycast(player.Player.look.aim.position, player.Player.look.aim.forward, out hit, 10, RayMasks.PLAYER))
            {
                if(hit.transform.GetComponent<Player>() == null) { return null; }
                var Player = hit.transform.GetComponent<Player>();

                return UnturnedPlayer.FromPlayer(Player);
                
            }
            return null;
        }
        public BarricadeData TryGetBarricade(Transform transform)
        {
            byte x;
            byte y;

            ushort plant;
            ushort index;

            BarricadeRegion r;

            if (BarricadeManager.tryGetInfo(transform, out x, out y, out plant, out index, out r))
            {

                return r.barricades[index];
            }
            return null;
        }
        public StructureData TryGetStructure(Transform transform)
        {
            byte x;
            byte y;

            ushort index;

            StructureRegion s;

            if (StructureManager.tryGetInfo(transform, out x, out y, out index, out s))
            {
                return s.structures[index];
            }
            return null;
        }
        public InteractableVehicle TryGetVehicle(Transform transform)
        {
            InteractableVehicle vehicle = transform.gameObject.GetComponent<InteractableVehicle>();
            
            if (vehicle != null)
            {
                return vehicle;   
            }
            return null;
        }
        public InteractableSign TryGetSign(Transform transform)
        {

           if (transform.GetComponent<InteractableSign>() != null)
           {
                return transform.GetComponent<InteractableSign>();
           }
           return null;

        }
    } 
}
