using System;
using System.Collections.Generic;
using System.Linq;
using BepInEx;
using Comfort.Common;
using EFT;
using UnityEngine;

namespace VisceralRagdolls
{
    [BepInPlugin("com.servph.visceralbodies", "Visceral Bodies", "1.2.0")]
    public class VisceralEntry : BaseUnityPlugin
    {
        public Player LocalPlayer { get; private set; }
        public Player HideoutPlayer { get; private set; }
        public Boolean SillyGooseBool { get; private set; }

        public static VisceralEntry Instance { get; private set; }

        public void Awake()
        {
            Instance = this;
            new BodyPatch().Enable();
            EFTHardSettings.Instance.DEBUG_CORPSE_PHYSICS = true;
        }
        
		public void Update()
        {
            if (!Singleton<GameWorld>.Instantiated)
            {
                this.LocalPlayer = null;
                return;
            }

            GameWorld gameWorld = Singleton<GameWorld>.Instance;

            if (this.LocalPlayer == null && gameWorld.RegisteredPlayers.Count > 0)
            {
                this.LocalPlayer = gameWorld.RegisteredPlayers[0];
                return;
            }

            if (this.HideoutPlayer == null && gameWorld.RegisteredPlayers.Count > 0)
            {
                this.HideoutPlayer = gameWorld.RegisteredPlayers[0];
                return;
            }




            // the part that actually fixes floating bodies. no clue on how bad fps is tho.
            if (Instance.LocalPlayer != null && SillyGooseBool == false)
            {
                var terrains = GameObject.Find("TerrainsAI");

                if (terrains == null)
                {
                    SillyGooseBool = true;
                }

                if (terrains != null)
                {
                    if (terrains.activeSelf)
                    {
                        terrains.SetActive(false);
                        SillyGooseBool = true;
                    }
                }

                if (terrains == null)
                {
                    SillyGooseBool = true;
                }
            }

            if (Instance.LocalPlayer == null)
            {
                SillyGooseBool = false;
            }


        }
    }
}