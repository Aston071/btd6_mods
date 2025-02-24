﻿using MelonLoader;
using Harmony;
using Il2CppAssets.Scripts.Unity.UI_New.InGame.Races;
using Il2CppAssets.Scripts.Simulation.Towers.Weapons;
using Il2CppAssets.Scripts.Simulation;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using Il2CppAssets.Scripts.Unity.UI_New.Main;
using Il2CppAssets.Scripts.Simulation.Bloons;
using Il2CppAssets.Scripts.Models.Towers;

using Il2CppAssets.Scripts.Unity;



using Il2CppAssets.Scripts.Simulation.Towers;

using Il2CppAssets.Scripts.Utils;

using Il2CppSystem.Collections;
using Il2CppAssets.Scripts.Unity.UI_New.Popups;
using Il2CppAssets.Scripts.Models.Rounds;
using Il2CppInterop.Runtime; using Il2CppInterop.Runtime.InteropTypes; using Il2CppInterop.Runtime.InteropTypes.Arrays;
using System;
using Il2CppAssets.Scripts.Unity.Scenes;
using BTD_Mod_Helper.Api.Bloons;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper;

[assembly: MelonInfo(typeof(clumped_rounds.Main), clumped_rounds.ModHelperData.Name, clumped_rounds.ModHelperData.Version, clumped_rounds.ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]
namespace clumped_rounds
{
    public class Main : BloonsTD6Mod
    {



        public override void OnApplicationStart()
        {
            base.OnApplicationStart();
            Console.WriteLine("clumped_rounds loaded");
        }

        public class clumpedRounds : ModRoundSet
        {
            public override string BaseRoundSet => RoundSetType.Default;
            public override int DefinedRounds => BaseRounds.Count;
            public override string DisplayName => "Clumped Rounds";
            public override string Icon => VanillaSprites.LargerPotionsUpgradeIcon;

            public override void ModifyRoundModels(RoundModel roundModel, int round)
            {
                foreach (var group in roundModel.groups)
                {
                    group.start = 0;
                    group.end = 0;
                }
            }
        }


        //[HarmonyPatch(typeof(TitleScreen), "Start")]
        //public class Game_Patch
        //{
        //    [HarmonyPostfix]
        //    public static void Postfix()
        //    {
        //        for (int i = 0; i < Game.instance.model.roundSets.Length; i++)
        //        {
        //            RoundSetModel roundSet = Game.instance.model.roundSets[i];
        //            for (int j = 0; j < roundSet.rounds.Length; j++)
        //            {
        //                RoundModel round = roundSet.rounds[j];
        //                foreach (var group in round.groups)
        //                {
        //                    group.start = 0;
        //                    group.end = 0;
        //                }

        //            }
        //        }
        //    }
        //}



        public override void OnUpdate()
        {
            base.OnUpdate();
            bool inAGame = InGame.instance != null && InGame.instance.bridge != null;


            if (inAGame)
            {
            }
        }




    }

}